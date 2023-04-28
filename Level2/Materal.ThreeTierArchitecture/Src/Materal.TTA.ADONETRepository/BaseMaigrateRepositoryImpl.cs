using System.Data;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 迁移仓储
    /// </summary>
    public abstract class BaseMaigrateRepositoryImpl : IMaigrateRepository
    {
        /// <summary>
        /// 迁移表名称
        /// </summary>
        protected const string MigrateTableName = "__TTAMigrationsHistory";
        /// <summary>
        /// 获得已存在的迁移数据
        /// </summary>
        /// <param name="dbOption"></param>
        /// <returns></returns>
        public List<string> GetExistingData(DBOption dbOption)
        {
            using IDbConnection dbConnection = dbOption.GetConnection();
            dbConnection.Open();
            InitMigrateTable(dbConnection);
            List<string> result = GetExistingData(dbConnection);
            return result;
        }
        /// <summary>
        /// 获得已存在的迁移数据
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        private List<string> GetExistingData(IDbConnection dbConnection)
        {
            List<string> result = new();
            IDbCommand command = dbConnection.CreateCommand();
            command.CommandText = GetQueryMigrateTableTSQL();
            using IDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                result.Add(dataReader.GetString(0));
            }
            return result;
        }
        /// <summary>
        /// 初始化迁移表
        /// </summary>
        /// <param name="dbConnection"></param>
        private void InitMigrateTable(IDbConnection dbConnection)
        {
            if (ExistsMigrateTable(dbConnection)) return;
            CreateMigrateTable(dbConnection);
        }
        /// <summary>
        /// 创建迁移表
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        private void CreateMigrateTable(IDbConnection dbConnection)
        {
            IDbCommand command = dbConnection.CreateCommand();
            command.CommandText = GetCreateMigrateTableTSQL();
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// 检查迁移表是否存在
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        private bool ExistsMigrateTable(IDbConnection dbConnection)
        {
            IDbCommand command = dbConnection.CreateCommand();
            command.CommandText = GetTableExistsTSQL(MigrateTableName);
            using IDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.GetInt32(0) > 0) return true;
            }
            return false;
        }
        #region 获取TSQL相关
        /// <summary>
        /// 获得查询迁移表TSQL
        /// </summary>
        /// <returns></returns>
        protected abstract string GetQueryMigrateTableTSQL();
        /// <summary>
        /// 获得创建迁移表TSQL
        /// </summary>
        /// <returns></returns>
        protected abstract string GetCreateMigrateTableTSQL();
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected abstract string GetTableExistsTSQL(string tableName);
        #endregion
    }
}
