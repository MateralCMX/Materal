using System.Data;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 迁移文件
    /// </summary>
    public abstract class Migration
    {
        /// <summary>
        /// 迁移表名称
        /// </summary>
        protected const string MigrateTableName = "__TTAMigrationsHistory";
        /// <summary>
        /// 迁移唯一标识
        /// </summary>
        public virtual string MigrationID => GetType().Name;
        /// <summary>
        /// 位序
        /// </summary>
        public abstract int Index { get; }
        /// <summary>
        /// 获取升级TSQL
        /// </summary>
        /// <returns></returns>
        public abstract List<string> GetUpTSQL();
        /// <summary>
        /// 迁移
        /// </summary>
        public virtual void Migrate(IDbConnection dbConnection)
        {
            List<string> upTSQLs = GetUpTSQL();
            foreach (string upTSQL in upTSQLs)
            {
                ExcuteUp(dbConnection, upTSQL);
                AddHistory(dbConnection);
            }
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="dbConnection"></param>
        private void AddHistory(IDbConnection dbConnection)
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            SetInsertHistoryCommand(dbCommand);
            dbCommand.ExecuteNonQuery();
        }
        /// <summary>
        /// 执行升级
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="upTSQL"></param>
        private void ExcuteUp(IDbConnection dbConnection, string upTSQL)
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = upTSQL;
            dbCommand.ExecuteNonQuery();
        }
        #region 获取TSQL相关
        /// <summary>
        /// 设置添加记录命令
        /// </summary>
        /// <param name="dbCommand"></param>
        protected abstract void SetInsertHistoryCommand(IDbCommand dbCommand);
        #endregion
    }
}
