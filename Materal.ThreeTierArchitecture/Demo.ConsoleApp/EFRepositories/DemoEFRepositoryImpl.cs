using Materal.TTA.Common;
using Materal.TTA.SqlServerRepository;
using Microsoft.EntityFrameworkCore;

namespace Demo.ConsoleApp.EFRepositories
{
    public class DemoEFRepositoryImpl<T, TPrimaryKeyType> : EFSubordinateRepositoryImpl<T, TPrimaryKeyType, UserDbContext> where T : class, IEntity<TPrimaryKeyType>
    {
        public DemoEFRepositoryImpl(UserDbContext dbContext)
            : base(dbContext, ApplicationConfig.UserDB.SubordinateConfigs,
                (options, connectionString) =>
                {
                    options.UseSqlServer(connectionString, m =>
                    {
                        m.EnableRetryOnFailure();
                    });
                })
        {
        }
    }
}
