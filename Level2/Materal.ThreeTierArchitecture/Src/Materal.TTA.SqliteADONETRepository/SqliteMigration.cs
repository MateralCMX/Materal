using Materal.TTA.ADONETRepository;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// Sqlite迁移类
    /// </summary>
    public abstract class SqliteMigration : Migration
    {
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected override string GetTableExistsTSQL(string tableName) => SqliteRepositoryHelper.GetTableExistsTSQL(tableName);
    }
}
