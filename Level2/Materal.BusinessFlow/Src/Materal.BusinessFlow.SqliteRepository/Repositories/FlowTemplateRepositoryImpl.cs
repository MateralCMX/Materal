using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class FlowTemplateRepositoryImpl : SqliteBaseRepositoryImpl<FlowTemplate>, IFlowTemplateRepository
    {
        public FlowTemplateRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
