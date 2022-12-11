using Materal.TTA.Demo.Domain;
using Materal.TTA.SqliteRepository;

namespace Materal.TTA.Demo.Sqlite
{
    public class UserSqliteRepository : SqliteEFRepositoryImpl<User, Guid>, IUserRepository
    {
        public UserSqliteRepository(TTADemoDBContext dbContext) : base(dbContext)
        {
        }
    }
}
