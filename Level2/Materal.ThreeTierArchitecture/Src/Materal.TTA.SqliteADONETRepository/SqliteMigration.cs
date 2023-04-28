using Materal.TTA.ADONETRepository;
using Materal.TTA.ADONETRepository.Extensions;
using System.Data;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// Sqlite迁移类
    /// </summary>
    public abstract class SqliteMigration : Migration
    {
        /// <summary>
        /// 设置添加记录命令
        /// </summary>
        /// <param name="dbCommand"></param>
        protected override void SetInsertHistoryCommand(IDbCommand dbCommand)
        {
            dbCommand.CommandText = $@"INSERT INTO ""{MigrateTableName}""(""ID"",""MigrationID"",""CreateTime"")
VALUES(@ID,@MigrationID,@CreateTime)";
            dbCommand.AddParameter("@ID", Guid.NewGuid());
            dbCommand.AddParameter("@MigrationID", MigrationID);
            dbCommand.AddParameter("@CreateTime", DateTime.Now);
        }
    }
}
