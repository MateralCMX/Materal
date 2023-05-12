using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.Step;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class StepController : BusinessFlowServiceControllerBase<Step, Step, IStepService, AddStepModel, EditStepModel, QueryStepModel>
    {
        public StepController(IServiceProvider service) : base(service)
        {
        }
    }
}