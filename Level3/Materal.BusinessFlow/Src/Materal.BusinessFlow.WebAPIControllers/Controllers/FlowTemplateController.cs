using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.WebAPIControllers.Models.FlowTemplate;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class FlowTemplateController : BusinessFlowServiceControllerBase<FlowTemplate, FlowTemplateDTO, IFlowTemplateService, QueryFlowTemplateModel, AddFlowTemplateModel, EditFlowTemplateModel>
    {
        public FlowTemplateController(IServiceProvider service) : base(service)
        {
        }
    }
}