using Materal.ConDep.Center.Domain;
using Materal.ConDep.Center.Domain.Repositories;

namespace Materal.ConDep.Center.SqliteEFRepository.RepositoryImpl
{
    public class UserRepositoryImpl : CenterSqliteEFRepositoryImpl<User>, IUserRepository
    {
        public UserRepositoryImpl(CenterDBContext dbContext) : base(dbContext)
        {
        }
    }
}
