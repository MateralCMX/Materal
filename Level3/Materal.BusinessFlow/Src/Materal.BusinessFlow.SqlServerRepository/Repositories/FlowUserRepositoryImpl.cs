using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.SqlServerRepository.Repositories;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class FlowUserRepositoryImpl : SqlServerBaseRepositoryImpl<FlowUser>, IFlowUserRepository
    {
        public FlowUserRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
