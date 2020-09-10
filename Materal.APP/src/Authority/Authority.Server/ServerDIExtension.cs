using Authority.DependencyInjection;
using Materal.APP.Core;
using Microsoft.Extensions.DependencyInjection;
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
        public static void AddAuthorityServerServices(this IServiceCollection services)
        {
            services.AddAuthorityServices();
            services.AddAutoMapperService(Assembly.Load("Authority.ServiceImpl"), Assembly.Load("Authority.Server"));
        }
    }
}
