using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;

namespace Materal.BusinessFlow.Services
{
    public class StepServiceImpl : BaseServiceImpl<Step, IStepRepository, QueryStepModel>, IStepService
    {
        public StepServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
