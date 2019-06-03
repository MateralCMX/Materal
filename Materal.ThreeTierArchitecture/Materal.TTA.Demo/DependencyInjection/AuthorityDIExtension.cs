using Authority.EFRepository;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace DependencyInjection
{
    /// <summary>
    /// 用户依赖注入扩展类
    /// </summary>
    public static class AuthorityDIExtension
    {
        /// <summary>
        /// 添加用户服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorityServices(this IServiceCollection services)
        {
            services.AddDbContextPool<AuthorityDbContext>(options => options.UseSqlServer(ApplicationConfig.AuthorityDB.ConnectionString, m =>
            {
                m.EnableRetryOnFailure();
            }));
            services.AddTransient(typeof(IAuthorityUnitOfWork), typeof(AuthorityUnitOfWorkImpl));
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Authority.EFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Authority.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
        }
    }
}
