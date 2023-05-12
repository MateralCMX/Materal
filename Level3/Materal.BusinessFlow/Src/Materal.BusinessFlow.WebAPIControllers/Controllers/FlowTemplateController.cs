using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.FlowTemplate;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class FlowTemplateController : BusinessFlowServiceControllerBase<FlowTemplate, FlowTemplateDTO, IFlowTemplateService, AddFlowTemplateModel, EditFlowTemplateModel, QueryFlowTemplateModel>
    {
        public FlowTemplateController(IServiceProvider service) : base(service)
        {
        }
    }
}