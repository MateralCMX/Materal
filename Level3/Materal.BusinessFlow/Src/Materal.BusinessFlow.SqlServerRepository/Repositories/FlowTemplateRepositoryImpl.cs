using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.TTA.ADONETRepository;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class FlowTemplateRepositoryImpl : BusinessFlowRepositoryImpl<FlowTemplate>, IFlowTemplateRepository
    {
        public FlowTemplateRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
