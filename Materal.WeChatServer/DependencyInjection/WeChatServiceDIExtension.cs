using WeChatService.EFRepository;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace DependencyInjection
{
    /// <summary>
    /// 微信服务依赖注入扩展类
    /// </summary>
    public static class WeChatServiceDIExtension
    {
        /// <summary>
        /// 添加用户服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddWeChatServiceServices(this IServiceCollection services)
        {
            services.AddDbContextPool<WeChatServiceDbContext>(options => options.UseSqlServer(ApplicationConfig.WeChatServiceDB.ConnectionString, m =>
            {
                m.UseRowNumberForPaging();
                m.EnableRetryOnFailure();
            }));
            services.AddTransient(typeof(IWeChatServiceUnitOfWork), typeof(WeChatServiceUnitOfWorkImpl));
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("WeChatService.EFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("WeChatService.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
        }
    }
}
