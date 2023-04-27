using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    /// <summary>
    /// Sqlite仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public class SqliteRepositoryImpl<T, TPrimaryKeyType> : ADONETRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SqliteRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// 获得创建表SQL
        /// </summary>
        /// <returns></returns>
        protected override string GetCreateTableTSQL() => SqliteRepositoryHelper<T, TPrimaryKeyType>.GetCreateTableTSQL(UnitOfWork, TableName);
        /// <summary>
        /// 获得表是否存在TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected override string GetTableExistsTSQL(string tableName) => SqliteRepositoryHelper.GetTableExistsTSQL(UnitOfWork, tableName);
    }
}
