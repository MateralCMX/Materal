using Materal.TTA.ADONETRepository;
using Materal.TTA.ADONETRepository.Extensions;
using System.Data;

namespace Materal.TTA.SqlServerADONETRepository
{
    /// <summary>
    /// Sqlite迁移类
    /// </summary>
    public abstract class SqlServerMigration : Migration
    {        /// <summary>
             /// 设置添加记录命令
             /// </summary>
             /// <param name="dbCommand"></param>
        protected override void SetInsertHistoryCommand(IDbCommand dbCommand)
        {
            dbCommand.CommandText = $@"INSERT INTO [{MigrateTableName}]([ID],[MigrationID],[CreateTime])
VALUES(@ID,@MigrationID,@CreateTime)";
            dbCommand.AddParameter("@ID", Guid.NewGuid());
            dbCommand.AddParameter("@MigrationID", MigrationID);
            dbCommand.AddParameter("@CreateTime", DateTime.Now);
        }
        /// <summary>
        /// 设置检查是否迁移命令
        /// </summary>
        /// <param name="dbCommand"></param>
        protected override void SetMigrateExistsCommand(IDbCommand dbCommand)
        {
            dbCommand.CommandText = $@"SELECT COUNT([MigrationID]) FROM [{MigrateTableName}] WHERE [MigrationID] = @MigrationID";
            dbCommand.AddParameter("@MigrationID", MigrationID);
        }
        /// <summary>
        /// 获得创建迁移表TSQL
        /// </summary>
        /// <returns></returns>
        protected override string GetCreateMigrateTableTSQL() => $@"CREATE TABLE [{MigrateTableName}](
	[ID] [uniqueidentifier] NOT NULL,
	[MigrationID] [varchar](max) NOT NULL,
    [CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_{MigrateTableName}] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected override string GetTableExistsTSQL(string tableName) => SqlServerRepositoryHelper.GetTableExistsTSQL(tableName);
    }
}
