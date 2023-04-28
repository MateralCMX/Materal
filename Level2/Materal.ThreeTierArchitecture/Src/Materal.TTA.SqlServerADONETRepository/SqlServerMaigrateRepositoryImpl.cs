using Materal.TTA.ADONETRepository;

namespace Materal.TTA.SqlServerADONETRepository
{
    /// <summary>
    /// 迁移仓储
    /// </summary>
    public class SqlServerMaigrateRepositoryImpl : BaseMaigrateRepositoryImpl, IMaigrateRepository
    {
        /// <summary>
        /// 获得查询迁移表TSQL
        /// </summary>
        /// <returns></returns>
        protected override string GetQueryMigrateTableTSQL() => $@"SELECT MigrationID FROM [{MigrateTableName}]";
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
