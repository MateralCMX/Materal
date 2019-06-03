using System.Reflection;
using DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Authority.WebAPI
{
    /// <summary>
    /// WebAPI依赖注入扩展
    /// </summary>
    public static class AuthorityWebAPIDIExtension
    {
        /// <summary>
        /// 添加WebAPI服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorityWebAPIServices(this IServiceCollection services)
        {
            services.AddBaseServices();
            services.AddAuthorityServices();
            services.AddAutoMapperService(Assembly.Load("Authority.ServiceImpl"), Assembly.Load("Authority.PresentationModel"));
        }
    }
}
