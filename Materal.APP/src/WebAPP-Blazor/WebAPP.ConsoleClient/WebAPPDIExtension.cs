using Materal.Common;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;
using Materal.APP.HttpClient;
using WebAPP.Common;

namespace WebAPP.ConsoleClient
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class WebAPPDIExtension
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddWebAPPServices(this IServiceCollection services)
        {
            MateralConfig.PageStartNumber = 1;
            services.AddSingleton<IAuthorityManage, DefaultAuthorityManageImpl>();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("WebAPP.HttpClientImpl"))
                .Where(c => c.Name.EndsWith("HttpClientImpl"))
                .AsPublicImplementedInterfaces();
        }
    }
}
