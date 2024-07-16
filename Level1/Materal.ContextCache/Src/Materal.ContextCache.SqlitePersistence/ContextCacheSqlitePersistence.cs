using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace Materal.ContextCache.SqlitePersistence
{
    /// <summary>
    /// 上下文缓存文件持久化
    /// </summary>
    public class ContextCacheSqlitePersistence : IContextCachePersistence
    {
        private const string _dbFilePath = @"Temp\ContextCache.db";
        private const string ContextCacheGroupTableName = "ContextCacheGroup";
        private const string ContextCacheTableName = "ContextCache";
        private bool _dbInitialized = false;
        private static readonly object _dbInitLock = new();
        private SqliteConnection? _dbConnection;
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="groupModel"></param>
        /// <param name="model"></param>
        public void Save(ContextCacheGroupModel groupModel, ContextCacheModel model)
        {
            SqliteConnection dbConnection = OpenDBConnection();
            SqliteTransaction transaction = dbConnection.BeginTransaction();
            try
            {
                SqliteCommand cmd = dbConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new("@ID", model.ID));
                cmd.CommandText = "SELECT COUNT(*) AS \"COUNT\" FROM \"ContextCacheGroup\" WHERE \"ID\" = @ID";
                object? groupCountResultObj = cmd.ExecuteScalar();
                cmd.Dispose();
                int groupCount = 0;
                if (groupCountResultObj is not null and long tableCountResult)
                {
                    groupCount = Convert.ToInt32(tableCountResult);
                }
                if (groupCount <= 0)
                {
                    cmd = dbConnection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new($"@{nameof(groupModel.ID)}", groupModel.ID));
                    cmd.Parameters.Add(new($"@{nameof(groupModel.RestorerTypeName)}", groupModel.RestorerTypeName));
                    cmd.CommandText = $"INSERT INTO \"ContextCacheGroup\"(\"{nameof(groupModel.ID)}\", \"{nameof(groupModel.RestorerTypeName)}\") VALUES(@{nameof(groupModel.ID)}, @{nameof(groupModel.RestorerTypeName)})";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                cmd = GetSaveCommad(dbConnection, model);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="groupModel"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveAsync(ContextCacheGroupModel groupModel, ContextCacheModel model)
        {
            SqliteConnection dbConnection = OpenDBConnection();
            SqliteTransaction transaction = dbConnection.BeginTransaction();
            try
            {
                SqliteCommand cmd = dbConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new("@ID", model.ID));
                cmd.CommandText = "SELECT COUNT(*) AS \"COUNT\" FROM \"ContextCacheGroup\" WHERE \"ID\" = @ID";
                object? groupCountResultObj = await cmd.ExecuteScalarAsync();
                cmd.Dispose();
                int groupCount = 0;
                if (groupCountResultObj is not null and long tableCountResult)
                {
                    groupCount = Convert.ToInt32(tableCountResult);
                }
                if (groupCount <= 0)
                {
                    cmd = dbConnection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new($"@{nameof(groupModel.ID)}", groupModel.ID));
                    cmd.Parameters.Add(new($"@{nameof(groupModel.RestorerTypeName)}", groupModel.RestorerTypeName));
                    cmd.CommandText = $"INSERT INTO \"ContextCacheGroup\"(\"{nameof(groupModel.ID)}\", \"{nameof(groupModel.RestorerTypeName)}\") VALUES(@{nameof(groupModel.ID)}, @{nameof(groupModel.RestorerTypeName)})";
                    await cmd.ExecuteNonQueryAsync();
                    cmd.Dispose();
                }
                cmd = GetSaveCommad(dbConnection, model);
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        public void Save(ContextCacheModel model)
        {
            SqliteConnection dbConnection = OpenDBConnection();
            try
            {
                SqliteCommand cmd = GetSaveCommad(dbConnection, model);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveAsync(ContextCacheModel model)
        {
            SqliteConnection dbConnection = OpenDBConnection();
            try
            {
                SqliteCommand cmd = GetSaveCommad(dbConnection, model);
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="groupID"></param>
        public void Remove(Guid groupID)
        {
            SqliteConnection dbConnection = OpenDBConnection();
            try
            {
                SqliteCommand cmd = GetRemoveCommad(dbConnection, groupID);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public async Task RemoveAsync(Guid groupID)
        {
            SqliteConnection dbConnection = OpenDBConnection();
            try
            {
                SqliteCommand cmd = GetRemoveCommad(dbConnection, groupID);
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 获得所有分组信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ContextCacheGroupModel> GetAllGroupInfo()
        {
            List<ContextCacheGroupModel> result = [];
            List<ContextCacheModel> contextCaches = [];
            SqliteConnection dbConnection = OpenDBConnection();
            try
            {
                SqliteCommand cmd = GetSelectGroupCommad(dbConnection);
                SqliteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ContextCacheGroupModel groupModel = new()
                    {
                        ID = dr.GetGuid(0),
                        RestorerTypeName = dr.GetString(1),
                        ContextCacheData = []
                    };
                    result.Add(groupModel);
                }
                cmd.Dispose();
                cmd = GetSelectCommad(dbConnection);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ContextCacheModel model = new()
                    {
                        ID = dr.GetGuid(0),
                        GroupID = dr.GetGuid(1),
                        UpID = dr.IsDBNull(2) ? null : dr.GetGuid(2),
                        ContextType = dr.GetString(3),
                        ContextData = dr.IsDBNull(4) ? null : dr.GetString(4)
                    };
                    contextCaches.Add(model);
                }
                cmd.Dispose();
            }
            finally
            {
                CloseDBConnection();
            }
            foreach (ContextCacheGroupModel groupModel in result)
            {
                ContextCacheModel[] nowContextCaches = contextCaches.Where(m => m.GroupID == groupModel.ID).ToArray();
                if (nowContextCaches.Length <= 0) yield return groupModel;
                ContextCacheModel? nowContextCache = nowContextCaches.FirstOrDefault(m => m.UpID is null);
                while (nowContextCache is not null)
                {
                    groupModel.ContextCacheData.Add(nowContextCache);
                    nowContextCache = contextCaches.FirstOrDefault(m => m.UpID == nowContextCache.ID);
                }
                yield return groupModel;
            }
        }
        /// <summary>
        /// 获得保存命令
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static SqliteCommand GetSaveCommad(SqliteConnection dbConnection, ContextCacheModel model)
        {
            SqliteCommand cmd = dbConnection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new($"@{nameof(model.ID)}", model.ID));
            cmd.Parameters.Add(new($"@{nameof(model.GroupID)}", model.GroupID));
            cmd.Parameters.Add(new($"@{nameof(model.UpID)}", model.UpID is null ? DBNull.Value : model.UpID.Value));
            cmd.Parameters.Add(new($"@{nameof(model.ContextType)}", model.ContextType));
            cmd.Parameters.Add(new($"@{nameof(model.ContextData)}", model.ContextData is null ? DBNull.Value : model.ContextData));
            cmd.CommandText = $"INSERT INTO \"ContextCache\"(\"{nameof(model.ID)}\", \"{nameof(model.GroupID)}\", \"{nameof(model.UpID)}\", \"{nameof(model.ContextType)}\", \"{nameof(model.ContextData)}\") VALUES(@{nameof(model.ID)}, @{nameof(model.GroupID)}, @{nameof(model.UpID)}, @{nameof(model.ContextType)}, @{nameof(model.ContextData)});";
            return cmd;
        }
        /// <summary>
        /// 获得移除命令
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private static SqliteCommand GetRemoveCommad(SqliteConnection dbConnection, Guid groupID)
        {
            SqliteCommand cmd = dbConnection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new("@GroupID", groupID));
            StringBuilder sqlBuilder = new();
            sqlBuilder.AppendLine("DELETE FROM \"ContextCache\" WHERE \"GroupID\" = @GroupID;");
            sqlBuilder.AppendLine("DELETE FROM \"ContextCacheGroup\" WHERE \"ID\" = @GroupID;");
            cmd.CommandText = sqlBuilder.ToString();
            return cmd;
        }
        /// <summary>
        /// 获得查询组命令
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        private static SqliteCommand GetSelectGroupCommad(SqliteConnection dbConnection)
        {
            SqliteCommand cmd = dbConnection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sqlBuilder = new();
            sqlBuilder.AppendLine("SELECT * FROM \"ContextCacheGroup\"");
            cmd.CommandText = sqlBuilder.ToString();
            return cmd;
        }
        /// <summary>
        /// 获得查询命令
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        private static SqliteCommand GetSelectCommad(SqliteConnection dbConnection)
        {
            SqliteCommand cmd = dbConnection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            StringBuilder sqlBuilder = new();
            sqlBuilder.AppendLine("SELECT * FROM \"ContextCache\"");
            cmd.CommandText = sqlBuilder.ToString();
            return cmd;
        }
        /// <summary>
        /// 打开数据库链接
        /// </summary>
        private SqliteConnection OpenDBConnection()
        {
            _dbConnection ??= GetDBConnection();
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            InitDB();
            return _dbConnection;
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void InitDB()
        {
            if (_dbInitialized || _dbConnection is null) return;
            lock (_dbInitLock)
            {
                if (_dbInitialized) return;
                TryCreateTable(_dbConnection, ContextCacheGroupTableName, "  \"RestorerTypeName\" TEXT NOT NULL,");
                TryCreateTable(_dbConnection, ContextCacheTableName,
                    "  \"GroupID\" TEXT(36) NOT NULL,",
                    "  \"UpID\" TEXT(36) NULL,",
                    "  \"ContextType\" TEXT NOT NULL,",
                    "  \"ContextData\" TEXT NULL,");
                _dbInitialized = true;
            }
        }
        /// <summary>
        /// 尝试创建表
        /// </summary>
        /// <param name="sqliteConnection"></param>
        /// <param name="tableName"></param>
        /// <param name="filedsSQLs"></param>
        private static void TryCreateTable(SqliteConnection sqliteConnection, string tableName, params string[] filedsSQLs)
        {
            SqliteCommand cmd = sqliteConnection.CreateCommand();
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
                cmd = sqliteConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                StringBuilder createTableTSQL = new();
                createTableTSQL.AppendLine($"CREATE TABLE \"{tableName}\" (");
                createTableTSQL.AppendLine("  \"ID\" TEXT(36) NOT NULL,");
                foreach (string filedsSQL in filedsSQLs)
                {
                    createTableTSQL.AppendLine(filedsSQL);
                }
                createTableTSQL.AppendLine("  PRIMARY KEY (\"ID\")");
                createTableTSQL.AppendLine(");");
                cmd.CommandText = createTableTSQL.ToString();
                cmd.ExecuteScalar();
                cmd.Dispose();
            }
        }
        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        private void CloseDBConnection()
        {
            if (_dbConnection is null || _dbConnection.State != ConnectionState.Open) return;
            _dbConnection.Close();
            _dbConnection.Dispose();
            _dbConnection = null;
        }
        /// <summary>
        /// 获得数据库链接
        /// </summary>
        /// <returns></returns>
        private SqliteConnection GetDBConnection()
        {
            string connectionString = Path.Combine(GetType().Assembly.GetDirectoryPath(), _dbFilePath);
            FileInfo fileInfo = new(connectionString);
            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            connectionString = $"Data Source={connectionString}";
            return new SqliteConnection(connectionString);
        }
    }
}
