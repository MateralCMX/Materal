using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.TTA.ADONETRepository;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class FlowUserRepositoryImpl : BusinessFlowRepositoryImpl<FlowUser>, IFlowUserRepository
    {
        public FlowUserRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
