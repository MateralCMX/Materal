using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class StepRepositoryImpl : SqlServerBaseRepositoryImpl<Step>, IStepRepository
    {
        public StepRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
