﻿using Materal.TTA.ADONETRepository.Extensions;
using System.Data;
using System.Data.Common;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 迁移文件
    /// </summary>
    public abstract class Migration
    {
        private const string _migrateTableName = "__TTAMigrationsHistory";
        /// <summary>
        /// 迁移唯一标识
        /// </summary>
        protected virtual string MigrationID => GetType().Name;
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
        public virtual void Migrate(DBOption dbOption)
        {
            using IDbConnection dbConnection = dbOption.GetConnection();
            dbConnection.Open();
            InitMigrateTable(dbConnection);
            ExcuteUp(dbConnection);
        }
        /// <summary>
        /// 执行升级
        /// </summary>
        /// <param name="dbConnection"></param>
        private void ExcuteUp(IDbConnection dbConnection)
        {
            if (!CanMigrate(dbConnection)) return;
            List<string> upTSQLs = GetUpTSQL();
            foreach (string upTSQL in upTSQLs)
            {
                ExcuteUp(dbConnection, upTSQL);
            }
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
        /// <summary>
        /// 是否需要迁移
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        private bool CanMigrate(IDbConnection dbConnection)
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            SetMigrateExistsCommand(dbCommand);
            using IDataReader dataReader = dbCommand.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.GetInt32(0) > 0) return false;
            }
            return true;
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
            command.CommandText = GetTableExistsTSQL(_migrateTableName);
            using IDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.GetInt32(0) > 0) return true;
            }
            return false;
        }
        #region 获取TSQL相关
        /// <summary>
        /// 设置检查是否迁移命令
        /// </summary>
        /// <param name="dbCommand"></param>
        private void SetMigrateExistsCommand(IDbCommand dbCommand)
        {
            dbCommand.CommandText = $@"SELECT COUNT(""MigrationID"") FROM ""__TTAMigrationsHistory"" WHERE ""MigrationID"" = @MigrationID";
            dbCommand.AddParameter("@MigrationID", MigrationID);
        }
        /// <summary>
        /// 获得创建迁移表TSQL
        /// </summary>
        /// <returns></returns>
        private string GetCreateMigrateTableTSQL() => $@"CREATE TABLE ""{_migrateTableName}"" (
  ""MigrationID"" text NOT NULL,
  PRIMARY KEY (""MigrationID"")
);";
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected abstract string GetTableExistsTSQL(string tableName);
        #endregion
    }
}
