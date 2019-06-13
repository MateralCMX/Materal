using System.Reflection;
using DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace WeChatService.WebAPI
{
    /// <summary>
    /// WebAPI依赖注入扩展
    /// </summary>
    public static class WeChatServiceWebAPIDIExtension
    {
        /// <summary>
        /// 添加WebAPI服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddWeChatServiceWebAPIServices(this IServiceCollection services)
        {
            services.AddBaseServices();
            services.AddAuthorityServices();
            services.AddWeChatServiceServices();
            services.AddAutoMapperService(Assembly.Load("WeChatService.ServiceImpl"), Assembly.Load("WeChatService.PresentationModel"));
        }
    }
}
