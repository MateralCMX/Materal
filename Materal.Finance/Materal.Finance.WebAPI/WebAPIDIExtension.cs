using Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.Finance.WebAPI
{
    /// <summary>
    /// WebAPI依赖注入扩展
    /// </summary>
    public static class WebAPIDIExtension
    {
        /// <summary>
        /// 添加WebAPI服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddWebAPIServices(this IServiceCollection services)
        {
            services.AddBaseServices();
            services.AddAutoMapperService(Assembly.Load("Authority.ServiceImpl"), Assembly.Load("Authority.PresentationModel"),
                Assembly.Load("TaskManager.ServiceImpl"), Assembly.Load("TaskManager.PresentationModel"));
        }
    }
}
