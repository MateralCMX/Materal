using Materal.BusinessFlow.Abstractions.Domain;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class BusinessFlowRepositoryImpl<T> : SqlServerADONETRepositoryImpl<T, Guid>
        where T : class, IBaseDomain, IEntity<Guid>, new()
    {
        public BusinessFlowRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
