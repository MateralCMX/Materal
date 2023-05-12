using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models.Step;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface IStepService : IBaseService<Step, Step, IStepRepository, AddStepModel, EditStepModel, QueryStepModel>
    {

    }
}
