using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class FlowTemplateRepositoryImpl : SqlServerBaseRepositoryImpl<FlowTemplate>, IFlowTemplateRepository
    {
        public FlowTemplateRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
