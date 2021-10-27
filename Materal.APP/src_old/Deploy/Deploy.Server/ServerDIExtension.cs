using Deploy.DependencyInjection;
using Materal.APP.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Deploy.Server
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
        public static void AddDeployServerServices(this IServiceCollection services)
        {
            services.AddDeployServices();
            services.AddAutoMapperService(Assembly.Load("Deploy.ServiceImpl"), Assembly.Load("Deploy.Server"));
        }
    }
}
