using DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Log.WebAPI
{    /// <summary>
     /// WebAPI依赖注入扩展
     /// </summary>
    public static class LogWebAPIDIExtension
    {
        /// <summary>
        /// 添加WebAPI服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddLogWebAPIServices(this IServiceCollection services)
        {
            services.AddBaseServices();
            services.AddAuthorityServices();
            services.AddAutoMapperService(Assembly.Load("Authority.ServiceImpl"),
                Assembly.Load("Log.ServiceImpl"), Assembly.Load("Log.PresentationModel"));
        }
    }
}
