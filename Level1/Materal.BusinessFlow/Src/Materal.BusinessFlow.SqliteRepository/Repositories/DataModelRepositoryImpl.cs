using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class DataModelRepositoryImpl : SqliteBaseRepositoryImpl<DataModel>, IDataModelRepository
    {
        public DataModelRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
