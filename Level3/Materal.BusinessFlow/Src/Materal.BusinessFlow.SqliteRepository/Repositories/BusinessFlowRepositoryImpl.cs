using Materal.BusinessFlow.Abstractions.Domain;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class BusinessFlowRepositoryImpl<T> : SqliteADONETRepositoryImpl<T, Guid>
        where T : class, IDomain, IEntity<Guid>, new()
    {
        public BusinessFlowRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
