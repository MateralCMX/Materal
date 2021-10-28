using Authority.Common;
using Authority.SqliteEFRepository;
using Materal.APP.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Authority.DependencyInjection
{
    public static class AuthorityDIExtension
    {
        public static void AddAuthorityServices(this IServiceCollection services)
        {
            services.AddDbContext<AuthorityDBContext>(options =>
            {
                options.UseSqlite(AuthorityConfig.SqliteConfig.ConnectionString);
            }, ServiceLifetime.Transient);
            services.AddTransient<DBContextHelper<AuthorityDBContext>>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Authority.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Authority.SqliteEFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.AddTransient<IAuthoritySqliteEFUnitOfWork, AuthoritySqliteEFUnitOfWorkImpl>();
        }
    }
}
