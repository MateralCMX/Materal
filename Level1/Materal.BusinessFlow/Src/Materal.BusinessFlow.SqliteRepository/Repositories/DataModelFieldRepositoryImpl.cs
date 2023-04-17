using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class DataModelFieldRepositoryImpl : SqliteBaseRepositoryImpl<DataModelField>, IDataModelFieldRepository
    {
        public DataModelFieldRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
