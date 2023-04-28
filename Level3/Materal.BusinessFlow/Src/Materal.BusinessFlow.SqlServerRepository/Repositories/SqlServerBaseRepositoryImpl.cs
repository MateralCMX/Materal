using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.ADONETRepository.Repositories;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class SqlServerBaseRepositoryImpl<T> : BaseRepositoryImpl<T>
        where T : class, IBaseDomain, new()
    {
        public SqlServerBaseRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected override string GetCreateTableTSQL() => SqlServerRepositoryHelper<T>.GetCreateTableTSQL(UnitOfWork, TableName);

        protected override string GetTableExistsTSQL(string tableName) => SqlServerRepositoryHelper.GetTableExistsTSQL(UnitOfWork, tableName);
    }
}
