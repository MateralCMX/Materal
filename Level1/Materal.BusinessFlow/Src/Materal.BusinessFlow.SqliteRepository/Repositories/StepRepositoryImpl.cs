using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class StepRepositoryImpl : SqliteBaseRepositoryImpl<Step>, IStepRepository
    {
        public StepRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
