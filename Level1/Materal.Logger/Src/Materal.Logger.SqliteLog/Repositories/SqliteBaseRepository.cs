using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;
using System.Text;

namespace Materal.Logger.Repositories
{
    /// <summary>
    /// Sqlite基础仓储
    /// </summary>
    public abstract class SqliteBaseRepository<T> : BaseRepository<T>
    {
        private readonly static object _createTableLockObj = new();
        private static bool _canCreateTable = false;
        private readonly string _dbPath;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="path"></param>
        protected SqliteBaseRepository(string path) : base(() =>
        {
            SqliteConnectionStringBuilder builder = path.StartsWith("Data Source=") ? new(path) : new($"Data Source={path};");
            return new SqliteConnection(builder.ConnectionString);
        })
        {
            if(path.StartsWith("Data Source="))
            {
                _dbPath = path[12..];
            }
            else
            {
                _dbPath = path;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <exception cref="LoggerException"></exception>
        public override void Init()
        {
            if (string.IsNullOrEmpty(_dbPath)) throw new LoggerException("未指定Sqlite数据库位置");
            FileInfo fileInfo = new(_dbPath);
            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            if (!fileInfo.Exists)
            {
                _canCreateTable = true;
            }
            CreateTable();
        }
        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <exception cref="LoggerException"></exception>
        protected virtual void CreateTable()
        {
            if (!_canCreateTable) return;
            DBConnection ??= GetDBConnection();
            lock (_createTableLockObj)
            {
                if (!_canCreateTable) return;
                OpenDBConnection();
                IDbCommand cmd = DBConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"select Count(*) as \"Count\" from sqlite_master where type = 'table' and name = '{TableName}'";
                object? dataResult = cmd.ExecuteScalar();
                cmd.Dispose();
                int tableCount = 0;
                if (dataResult != null && dataResult is long tableCountResult)
                {
                    tableCount = Convert.ToInt32(tableCountResult);
                }
                if (tableCount > 0)
                {
                    CloseDBConnection();
                    return;
                }
                IDbTransaction dbTransaction = DBConnection.BeginTransaction();
                cmd = DBConnection.CreateCommand();
                cmd.Transaction = dbTransaction;
                try
                {
                    cmd.CommandText = $@"PRAGMA foreign_keys = false;
{GetCreateTableTSQL()}
PRAGMA foreign_keys = true;";
                    var result = cmd.ExecuteScalar();
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
                    CloseDBConnection();
                    _canCreateTable = false;
                }
            }
        }
        /// <summary>
        /// 获取创建表sql文本
        /// </summary>
        /// <returns></returns>
        protected abstract string GetCreateTableTSQL();
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="domains"></param>
        /// <exception cref="LoggerException"></exception>
        public override void Inserts(T[] domains)
        {
            DBConnection ??= GetDBConnection();
            OpenDBConnection();
            IDbTransaction dbTransaction = DBConnection.BeginTransaction();
            try
            {
                IDbCommand cmd = DBConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandText = GetInsertCommandText(typeof(T));
                try
                {
                    if (cmd is not SqliteCommand sqliteCmd) throw new LoggerException("sqlcmd类型错误");
                    foreach (var domain in domains)
                    {
                        FillParams(sqliteCmd, domain);
                        int result = sqliteCmd.ExecuteNonQuery();
                        if (result <= 0) throw new LoggerException("添加失败");
                    }
                    dbTransaction.Commit();
                }
                finally
                {
                    cmd.Dispose();
                }
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
                throw;
            }
            finally
            {
                dbTransaction.Dispose();
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 获得添加tsql命令
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual string GetInsertCommandText(Type type)
        {
            Type tType = typeof(T);
            StringBuilder properties = new();
            StringBuilder values = new();
            foreach (PropertyInfo propertyInfo in tType.GetProperties())
            {
                properties.Append($"\"{propertyInfo.Name}\",");
                string parameterName = $"{ParameterPrefix}{propertyInfo.Name}";
                values.Append($"{parameterName},");
            }
            string propertiesString = properties.ToString()[0..^1];
            string valuesString = values.ToString()[0..^1];
            string result = $"Insert into \"{TableName}\"({propertiesString}) Values({valuesString});";
            return result;
        }
        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="domain"></param>
        protected virtual void FillParams(SqliteCommand cmd, T domain)
        {
            Type tType = typeof(T);
            foreach (PropertyInfo propertyInfo in tType.GetProperties())
            {
                string parameterName = $"{ParameterPrefix}{propertyInfo.Name}";
                object? value = propertyInfo.GetValue(domain);
                if (value is Guid guidValue)
                {
                    value = guidValue.ToString();
                }
                cmd.Parameters.AddWithValue(parameterName, value ?? DBNull.Value);
            }
        }
    }
}
