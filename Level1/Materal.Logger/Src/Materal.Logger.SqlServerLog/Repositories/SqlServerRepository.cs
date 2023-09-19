using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace Materal.Logger.Repositories
{
    /// <summary>
    /// Sqlserver基础仓储
    /// </summary>
    public class SqlServerRepository : BaseRepository<SqlServerLoggerHandlerModel>
    {
        /// <summary>
        /// Sqlserver基础仓储
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlServerRepository(string connectionString) : base(() =>
        {
            SqlConnectionStringBuilder builder = new(connectionString);
            return new SqlConnection(builder.ConnectionString);
        })
        {
        }
        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="models"></param>
        /// <exception cref="LoggerException"></exception>
        public override void Inserts(SqlServerLoggerHandlerModel[] models)
        {
            DBConnection ??= GetDBConnection();
            OpenDBConnection();
            try
            {
                if (DBConnection is not SqlConnection sqlConnection) throw new LoggerException("连接对象不是SqlServer连接对象");
                foreach (IGrouping<string, SqlServerLoggerHandlerModel> item in models.GroupBy(m => m.TableName))
                {
                    List<SqlServerDBFiled> firstFileds = models.First().Fileds;
                    CreateTable(item.Key, firstFileds);
                    DataTable dt = new();
                    foreach (SqlServerDBFiled filed in firstFileds)
                    {
                        dt.Columns.Add(filed.Name, filed.CSharpType);
                    }
                    using SqlBulkCopy sqlBulkCopy = new(sqlConnection);
                    sqlBulkCopy.DestinationTableName = item.Key;
                    FillDataTable(dt, item);
                    sqlBulkCopy.BatchSize = dt.Rows.Count;
                    foreach (DataColumn column in dt.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
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
        private DataTable FillDataTable(DataTable dt, IEnumerable<SqlServerLoggerHandlerModel> models)
        {
            foreach (SqlServerLoggerHandlerModel model in models)
            {
                DataRow dr = dt.NewRow();
                foreach (SqlServerDBFiled filed in model.Fileds)
                {
                    if (filed.Value is not null)
                    {
                        Type targetType = dt.Columns[filed.Name].DataType;
                        if (filed.Value.CanConvertTo(targetType))
                        {
                            dr[filed.Name] = filed.Value.ConvertTo(targetType);
                        }
                        else
                        {
                            dr[filed.Name] = filed.Value;
                        }
                    }
                    else
                    {
                        dr[filed.Name] = DBNull.Value;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        /// <param name="closeDB"></param>
        private void CreateTable(string tableName, List<SqlServerDBFiled> fileds, bool closeDB = false)
        {
            DBConnection ??= GetDBConnection();
            try
            {
                IDbCommand cmd = DBConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT COUNT([name]) as [TableCount] FROM [SysObjects] WHERE [name] = '{tableName}'";
                object? tableCountResultObj = cmd.ExecuteScalar();
                cmd.Dispose();
                int tableCount = 0;
                if (tableCountResultObj is not null and int tableCountResult)
                {
                    tableCount = Convert.ToInt32(tableCountResult);
                }
                if (tableCount <= 0)
                {
                    IDbTransaction dbTransaction;
                    #region 创建表
                    dbTransaction = DBConnection.BeginTransaction();
                    cmd = DBConnection.CreateCommand();
                    cmd.Transaction = dbTransaction;
                    try
                    {
                        cmd.CommandText = GetCreateTableTSQL(tableName, fileds);
                        object createTableResult = cmd.ExecuteScalar();
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
                            object createTableResult = cmd.ExecuteScalar();
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
        private string GetCreateTableTSQL(string tableName, List<SqlServerDBFiled> fileds)
        {
            StringBuilder setPrimaryKeyTSQL = new();
            StringBuilder createTableTSQL = new();
            createTableTSQL.AppendLine($"CREATE TABLE [{tableName}](");
            List<string> columns = new();
            foreach (SqlServerDBFiled filed in fileds)
            {
                columns.Add(filed.GetCreateTableFiledSQL());
                if (filed.PK)
                {
                    setPrimaryKeyTSQL.AppendLine($"CONSTRAINT [PK_{tableName}s] PRIMARY KEY CLUSTERED (");
                    setPrimaryKeyTSQL.AppendLine($"[{filed.Name}] ASC");
                    setPrimaryKeyTSQL.AppendLine($")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                    setPrimaryKeyTSQL.AppendLine($") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");
                }
            }
            createTableTSQL.Append(string.Join(",", columns));
            if (setPrimaryKeyTSQL is not null && setPrimaryKeyTSQL.Length > 0)
            {
                createTableTSQL.AppendLine(",");
                createTableTSQL.Append(setPrimaryKeyTSQL);
            }
            string result = createTableTSQL.ToString();
            return result;
        }
        /// <summary>
        /// 获得创建表语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">字段</param>
        /// <returns></returns>
        private string? GetCreateIndexTSQL(string tableName, List<SqlServerDBFiled> fileds)
        {
            List<string> indexColumns = new();
            foreach (SqlServerDBFiled filed in fileds)
            {
                if (filed.Index)
                {
                    indexColumns.Add($"[{filed.Name}]");
                }
            }
            if (indexColumns.Count <= 0) return null;
            StringBuilder createIndexTSQL = new();
            createIndexTSQL.AppendLine($"CREATE NONCLUSTERED INDEX [IX_{tableName}s] ON [{tableName}] (");
            createIndexTSQL.Append(string.Join(" DESC,", indexColumns));
            createIndexTSQL.AppendLine($")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
            string result = createIndexTSQL.ToString();
            return result;
        }
    }
}
