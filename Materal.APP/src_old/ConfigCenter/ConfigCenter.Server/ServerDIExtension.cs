using ConfigCenter.DependencyInjection;
using Materal.APP.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConfigCenter.Server
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
        public static void AddConfigCenterServerServices(this IServiceCollection services)
        {
            services.AddConfigCenterServices();
            services.AddAutoMapperService(Assembly.Load("ConfigCenter.ServiceImpl"), Assembly.Load("ConfigCenter.Server"));
        }
    }
}
