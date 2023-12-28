using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite基础仓储
    /// </summary>
    public class SqliteRepository : BaseRepository<SqliteLoggerWriterModel>
    {
        private readonly string _dbPath;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="path"></param>
        public SqliteRepository(string path) : base(() => new SqliteConnection($"Data Source={path};"))
        {
            _dbPath = path;
            if (string.IsNullOrEmpty(_dbPath)) throw new LoggerException("未指定Sqlite数据库位置");
            FileInfo fileInfo = new(_dbPath);
            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="models"></param>
        public override void Inserts(SqliteLoggerWriterModel[] models)
        {
            DBConnection ??= GetDBConnection();
            OpenDBConnection();
            try
            {
                foreach (IGrouping<string, SqliteLoggerWriterModel> item in models.GroupBy(m => m.TableName))
                {
                    List<SqliteDBFiled> firstFileds = item.First().Fileds;
                    CreateTable(item.Key, firstFileds);
                    IDbTransaction dbTransaction = DBConnection.BeginTransaction();
                    try
                    {
                        IDbCommand cmd = DBConnection.CreateCommand();
                        if (cmd is not SqliteCommand sqliteCmd) throw new LoggerException("sqlcmd类型错误");
                        sqliteCmd.CommandType = CommandType.Text;
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.CommandText = GetInsertTSQL(item.Key, firstFileds);
                        foreach (SqliteLoggerWriterModel model in item)
                        {
                            Insert(sqliteCmd, model.Fileds);
                        }
                        dbTransaction.Commit();
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
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="fileds"></param>
        private static void Insert(SqliteCommand cmd, List<SqliteDBFiled> fileds)
        {
            cmd.Parameters.Clear();
            foreach (SqliteDBFiled filed in fileds)
            {
                object? value = filed.Value;
                if (filed.Value is null)
                {
                    value = DBNull.Value;
                }
                cmd.Parameters.AddWithValue($"@{filed.Name}", value);
            }
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 获得添加语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        private string GetInsertTSQL(string tableName, List<SqliteDBFiled> fileds)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"Insert into \"{tableName}\"(");
            List<string> columns = [];
            List<string> args = [];
            foreach (SqliteDBFiled filed in fileds)
            {
                columns.Add($"\"{filed.Name}\"");
                args.Add($"@{filed.Name}");
            }
            stringBuilder.AppendLine($"{string.Join(",", columns)})");
            stringBuilder.AppendLine($"Values({string.Join(",", args)})");
            string result = stringBuilder.ToString();
            return result;
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        /// <param name="closeDB"></param>
        private void CreateTable(string tableName, List<SqliteDBFiled> fileds, bool closeDB = false)
        {
            DBConnection ??= GetDBConnection();
            try
            {
                IDbCommand cmd = DBConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"select Count(*) as \"Count\" from sqlite_master where type = 'table' and name = '{tableName}'";
                object? tableCountResultObj = cmd.ExecuteScalar();
                cmd.Dispose();
                int tableCount = 0;
                if (tableCountResultObj is not null and long tableCountResult)
                {
                    tableCount = Convert.ToInt32(tableCountResult);
                }
                if (tableCount <= 0)
                {
                    IDbTransaction dbTransaction = DBConnection.BeginTransaction();
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
        private string GetCreateTableTSQL(string tableName, List<SqliteDBFiled> fileds)
        {
            string? setPrimaryKeyTSQL = null;
            List<string> indexColumns = [];
            StringBuilder createTableTSQL = new();
            createTableTSQL.AppendLine("PRAGMA foreign_keys = false;");
            createTableTSQL.AppendLine($"CREATE TABLE \"{tableName}\" (");
            List<string> columns = [];
            foreach (SqliteDBFiled filed in fileds)
            {
                columns.Add(filed.GetCreateTableFiledSQL());
                if (filed.PK)
                {
                    setPrimaryKeyTSQL = $", PRIMARY KEY (\"{filed.Name}\")";
                }
                if (filed.Index is not null)
                {
                    indexColumns.Add($"\"{filed.Name}\" {filed.Index}");
                }
            }
            createTableTSQL.Append(string.Join(",", columns));
            if (setPrimaryKeyTSQL is not null && !string.IsNullOrWhiteSpace(setPrimaryKeyTSQL))
            {
                createTableTSQL.Append(setPrimaryKeyTSQL);
            }
            createTableTSQL.AppendLine(");");
            if (indexColumns.Count > 0)
            {
                createTableTSQL.AppendLine($"CREATE INDEX \"{tableName}Index\" ON \"{tableName}\" (");
                createTableTSQL.Append(string.Join(",", indexColumns));
                createTableTSQL.AppendLine(");");
            }
            createTableTSQL.AppendLine("PRAGMA foreign_keys = true;");
            string result = createTableTSQL.ToString();
            return result;
        }
    }
}
