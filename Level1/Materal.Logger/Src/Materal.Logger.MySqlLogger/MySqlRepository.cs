using MySqlConnector;
using System.Data;

namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// Sqlserver基础仓储
    /// </summary>
    public class MySqlRepository(string connectionString) : BaseRepository<MySqlLoggerWriterModel>(() =>
    {
        MySqlConnectionStringBuilder builder = new(connectionString);
        return new MySqlConnection(builder.ConnectionString);
    })
    {
        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="models"></param>
        /// <exception cref="LoggerException"></exception>
        public override void Inserts(MySqlLoggerWriterModel[] models)
        {
            DBConnection ??= GetDBConnection();
            OpenDBConnection();
            try
            {
                if (DBConnection is not MySqlConnection sqlConnection) throw new LoggerException("连接对象不是MySql连接对象");
                foreach (IGrouping<string, MySqlLoggerWriterModel> item in models.GroupBy(m => m.TableName))
                {
                    List<MySqlDBFiled> firstFileds = models.First().Fileds;
                    CreateTable(item.Key, firstFileds);
                    DataTable dt = new();
                    foreach (MySqlDBFiled filed in firstFileds)
                    {
                        dt.Columns.Add(filed.Name, filed.CSharpType);
                    }
                    MySqlBulkCopy sqlBulkCopy = new(sqlConnection)
                    {
                        DestinationTableName = item.Key
                    };
                    FillDataTable(dt, item);
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
        private static DataTable FillDataTable(DataTable dt, IEnumerable<MySqlLoggerWriterModel> models)
        {
            foreach (MySqlLoggerWriterModel model in models)
            {
                DataRow dr = dt.NewRow();
                foreach (MySqlDBFiled filed in model.Fileds)
                {
                    if (filed.Value is not null)
                    {
                        Type? targetType = dt.Columns[filed.Name]?.DataType;
                        if (targetType == null) continue;
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
        private void CreateTable(string tableName, List<MySqlDBFiled> fileds, bool closeDB = false)
        {
            DBConnection ??= GetDBConnection();
            try
            {
                IDbCommand cmd = DBConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SHOW TABLES LIKE '{tableName}';";
                bool tableExists = cmd.ExecuteScalar() is not null;
                cmd.Dispose();
                if (tableExists) return;
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
        private string GetCreateTableTSQL(string tableName, List<MySqlDBFiled> fileds)
        {
            StringBuilder setPrimaryKeyTSQL = new();
            StringBuilder createTableTSQL = new();
            createTableTSQL.AppendLine($"CREATE TABLE `{tableName}`(");
            List<string> indexs = [];
            List<string> columns = [];
            foreach (MySqlDBFiled filed in fileds)
            {
                columns.Add(filed.GetCreateTableFiledSQL());
                if (filed.PK)
                {
                    setPrimaryKeyTSQL.AppendLine($"PRIMARY KEY (`{filed.Name}`)");
                }
                if (filed.Index is not null)
                {
                    indexs.Add($"INDEX `{filed.Name}Index`(`{filed.Name}` {filed.Index})");
                }
            }
            createTableTSQL.Append(string.Join(",", columns));
            if (setPrimaryKeyTSQL is not null && setPrimaryKeyTSQL.Length > 0)
            {
                createTableTSQL.AppendLine(",");
                createTableTSQL.Append(setPrimaryKeyTSQL);
            }
            if (indexs is not null && indexs.Count > 0)
            {
                createTableTSQL.AppendLine(",");
                createTableTSQL.Append(string.Join(",", indexs));
            }
            createTableTSQL.Append(')');
            string result = createTableTSQL.ToString();
            return result;
        }
    }
}
