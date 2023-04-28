using Materal.TTA.ADONETRepository;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// 迁移仓储
    /// </summary>
    public class SqliteMaigrateRepositoryImpl : BaseMaigrateRepositoryImpl, IMaigrateRepository
    {
        /// <summary>
        /// 获得查询迁移表TSQL
        /// </summary>
        /// <returns></returns>
        protected override string GetQueryMigrateTableTSQL() => $@"SELECT MigrationID FROM ""{MigrateTableName}""";
        /// <summary>
        /// 获得创建迁移表TSQL
        /// </summary>
        /// <returns></returns>
        protected override string GetCreateMigrateTableTSQL() => $@"CREATE TABLE ""{MigrateTableName}"" (
  ""ID"" TEXT NOT NULL,
  ""MigrationID"" TEXT NOT NULL,
  ""CreateTime"" DATETIME NOT NULL,
  PRIMARY KEY (""ID"")
);";
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected override string GetTableExistsTSQL(string tableName) => SqliteRepositoryHelper.GetTableExistsTSQL(tableName);
    }
}
