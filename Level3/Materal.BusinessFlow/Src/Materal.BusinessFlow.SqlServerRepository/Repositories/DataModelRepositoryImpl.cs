using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.TTA.ADONETRepository;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class DataModelRepositoryImpl : BusinessFlowRepositoryImpl<DataModel>, IDataModelRepository
    {
        public DataModelRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
