using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.SqlServerADONETRepository;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    /// <summary>
    /// SqlServer仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public class SqlServerRepositoryImpl<T, TPrimaryKeyType> : ADONETRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SqlServerRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// 获得创建表SQL
        /// </summary>
        /// <returns></returns>
        protected override string GetCreateTableTSQL() => SqlServerRepositoryHelper<T, TPrimaryKeyType>.GetCreateTableTSQL(UnitOfWork, TableName);
        /// <summary>
        /// 获得表是否存在TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected override string GetTableExistsTSQL(string tableName) => SqlServerRepositoryHelper.GetTableExistsTSQL(UnitOfWork, tableName);
    }
}
