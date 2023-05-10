using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.WebAPIControllers.Models.Step;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class StepController : BusinessFlowServiceControllerBase<Step, IStepService, QueryStepModel, AddStepModel, EditStepModel>
    {
        public StepController(IServiceProvider service) : base(service)
        {
        }
    }
}