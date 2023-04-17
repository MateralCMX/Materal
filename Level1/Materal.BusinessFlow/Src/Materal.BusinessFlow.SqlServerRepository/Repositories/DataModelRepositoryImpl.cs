using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class DataModelRepositoryImpl : SqlServerBaseRepositoryImpl<DataModel>, IDataModelRepository
    {
        public DataModelRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
