using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;

namespace Materal.BusinessFlow.Services
{
    public class FlowTemplateServiceImpl : BaseServiceImpl<FlowTemplate, FlowTemplate, IFlowTemplateRepository, QueryFlowTemplateModel>, IFlowTemplateService
    {
        public FlowTemplateServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
