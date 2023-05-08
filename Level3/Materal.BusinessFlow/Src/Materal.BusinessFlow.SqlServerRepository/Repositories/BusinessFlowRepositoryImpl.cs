using Materal.BusinessFlow.Abstractions.Domain;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.SqlServerADONETRepository;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class BusinessFlowRepositoryImpl<T> : SqlServerADONETRepositoryImpl<T, Guid>
        where T : class, IDomain, IEntity<Guid>, new()
    {
        public BusinessFlowRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
