﻿using Materal.Logger.DBLogger.Repositories;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle基础仓储
    /// </summary>
    public class OracleRepository(string connectionString) : BaseRepository<OracleLog, OracleLoggerTargetOptions, OracleFiled>(() =>
    {
        OracleConnectionStringBuilder builder = new(connectionString);
        return new OracleConnection(builder.ConnectionString);
    })
    {
        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="logs"></param>
        /// <exception cref="LoggerException"></exception>
        public override void Inserts(OracleLog[] logs)
        {
            DBConnection ??= GetDBConnection();
            OpenDBConnection();
            try
            {
                if (DBConnection is not OracleConnection sqlConnection) throw new LoggerException("连接对象不是Oracle连接对象");
                foreach (IGrouping<string, OracleLog> item in logs.GroupBy(m => m.TableName))
                {
                    List<IDBFiled> firstFileds = logs.First().Fileds;
                    CreateTable(item.Key, firstFileds);
                    DataTable dt = firstFileds.CreateDataTable();
                    using OracleBulkCopy sqlBulkCopy = new(sqlConnection);
                    sqlBulkCopy.DestinationTableName = $"\"{item.Key}\"";
                    FillDataTable(dt, item);
                    sqlBulkCopy.BatchSize = dt.Rows.Count;
                    foreach (DataColumn column in dt.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(column.ColumnName, $"\"{column.ColumnName}\"");
                    }
                    sqlBulkCopy.WriteToServer(dt);
                }
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        private static DataTable FillDataTable(DataTable dt, IEnumerable<OracleLog> models)
        {
            foreach (OracleLog model in models)
            {
                model.Fileds.AddNewRow(dt);
            }
            return dt;
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        /// <param name="closeDB"></param>
        private void CreateTable(string tableName, List<IDBFiled> fileds, bool closeDB = false)
        {
            DBConnection ??= GetDBConnection();
            try
            {
                IDbCommand cmd = DBConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT COUNT(*) FROM all_tables WHERE table_name = '{tableName}'";
                object? tableCountResultObj = cmd.ExecuteScalar();
                cmd.Dispose();
                int tableCount = 0;
                if (tableCountResultObj is not null and decimal tableCountResult)
                {
                    tableCount = Convert.ToInt32(tableCountResult);
                }
                if (tableCount > 0) return;
                IDbTransaction dbTransaction;
                #region 创建表
                dbTransaction = DBConnection.BeginTransaction();
                cmd = DBConnection.CreateCommand();
                cmd.Transaction = dbTransaction;
                try
                {
                    cmd.CommandText = GetCreateTableTSQL(tableName, fileds);
                    object? createTableResult = cmd.ExecuteScalar();
                    dbTransaction.Commit();
                    cmd.Dispose();
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    throw;
                }
                finally
                {
                    dbTransaction.Dispose();
                }
                #endregion
                #region 创建索引
                string? creteIndexTSQL = GetCreateIndexTSQL(tableName, fileds);
                if (creteIndexTSQL is not null && !string.IsNullOrWhiteSpace(creteIndexTSQL))
                {
                    dbTransaction = DBConnection.BeginTransaction();
                    cmd = DBConnection.CreateCommand();
                    cmd.Transaction = dbTransaction;
                    try
                    {
                        cmd.CommandText = creteIndexTSQL;
                        object? createTableResult = cmd.ExecuteScalar();
                        dbTransaction.Commit();
                        cmd.Dispose();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        dbTransaction.Dispose();
                    }
                }
                #endregion
            }
            finally
            {
                if (closeDB)
                {
                    CloseDBConnection();
                }
            }
        }
        /// <summary>
        /// 获得创建表语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">字段</param>
        /// <returns></returns>
        private static string GetCreateTableTSQL(string tableName, List<IDBFiled> fileds)
        {
            StringBuilder setPrimaryKeyTSQL = new();
            StringBuilder createTableTSQL = new();
            createTableTSQL.AppendLine($"CREATE TABLE \"{tableName}\"(");
            List<string> columns = [];
            foreach (IDBFiled filed in fileds)
            {
                columns.Add(filed.GetCreateTableFiledSQL());
                if (filed.PK)
                {
                    setPrimaryKeyTSQL.AppendLine($"PRIMARY KEY (\"{filed.Name}\")");
                }
            }
            createTableTSQL.Append(string.Join(",", columns));
            if (setPrimaryKeyTSQL is not null && setPrimaryKeyTSQL.Length > 0)
            {
                createTableTSQL.AppendLine(",");
                createTableTSQL.Append(setPrimaryKeyTSQL);
            }
            createTableTSQL.Append(')');
            string result = createTableTSQL.ToString();
            return result;
        }
        /// <summary>
        /// 获得创建索引语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">字段</param>
        /// <returns></returns>
        private static string? GetCreateIndexTSQL(string tableName, List<IDBFiled> fileds)
        {
            List<string> indexColumns = [];
            foreach (IDBFiled filed in fileds)
            {
                if (filed.Index is not null)
                {
                    indexColumns.Add($"CREATE INDEX \"{filed.Name}Index\" ON \"{tableName}\" (\"{filed.Name}\" {filed.Index})");
                }
            }
            if (indexColumns.Count <= 0) return null;
            StringBuilder createIndexTSQL = new();
            createIndexTSQL.Append(string.Join(";", indexColumns));
            string result = createIndexTSQL.ToString();
            return result;
        }
    }
}
