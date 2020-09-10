using Authority.Common;
using Authority.Server.Services;
using Authority.SqliteEFRepository;
using Materal.APP.Common;
using Materal.APP.EFRepository;
using Materal.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace Authority.Server
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServerDIExtension
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddServerServices(this IServiceCollection services)
        {
            MateralConfig.PageStartNumber = 1;
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
            services.AddAutoMapperService(Assembly.Load("Authority.ServiceImpl"), Assembly.Load("Authority.Server"));
        }

        public static void AddGrpcRoute(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGrpcService<UserService>();
        }
    }
}
