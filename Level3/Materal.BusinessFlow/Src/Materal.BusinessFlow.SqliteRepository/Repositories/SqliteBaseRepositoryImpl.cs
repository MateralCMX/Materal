using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.ADONETRepository.Repositories;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class SqliteBaseRepositoryImpl<T> : BaseRepositoryImpl<T>
        where T : class, IBaseDomain, new()
    {
        public SqliteBaseRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected override string GetCreateTableTSQL() => SqliteRepositoryHelper<T>.GetCreateTableTSQL(UnitOfWork, TableName);

        protected override string GetTableExistsTSQL(string tableName) => SqliteRepositoryHelper.GetTableExistsTSQL(UnitOfWork, tableName);
    }
}
