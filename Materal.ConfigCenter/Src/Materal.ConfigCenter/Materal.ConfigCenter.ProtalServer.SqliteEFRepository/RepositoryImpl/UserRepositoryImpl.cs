using Materal.ConfigCenter.ProtalServer.Domain;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository.RepositoryImpl
{
    public class UserRepositoryImpl : ProtalServerSqliteEFRepositoryImpl<User>, IUserRepository
    {
        public UserRepositoryImpl(ProtalServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
