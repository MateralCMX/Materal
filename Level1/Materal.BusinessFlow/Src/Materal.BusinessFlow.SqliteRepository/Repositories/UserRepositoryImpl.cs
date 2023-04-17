using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class UserRepositoryImpl : SqliteBaseRepositoryImpl<User>, IUserRepository
    {
        public UserRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
