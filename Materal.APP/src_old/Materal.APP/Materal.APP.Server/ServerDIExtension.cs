using Materal.APP.Core;
using Materal.APP.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.APP.Server
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
        public static void AddMateralAPPServerServices(this IServiceCollection services)
        {
            services.AddMateralAPPServices();
            services.AddAutoMapperService(Assembly.Load("Materal.APP.Server"));
        }
    }
}
