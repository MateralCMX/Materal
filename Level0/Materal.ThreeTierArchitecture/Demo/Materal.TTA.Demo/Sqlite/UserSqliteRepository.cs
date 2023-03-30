using Materal.TTA.Demo.Domain;
using Materal.TTA.SqliteRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.Demo.Sqlite
{
    public class UserSqliteRepository : SqliteEFRepositoryImpl<User, Guid, TTADemoDBContext>, IUserRepository
    {
        public UserSqliteRepository(TTADemoDBContext? dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<User> DBSet => DBContext.User.AsNoTracking();
    }
}
