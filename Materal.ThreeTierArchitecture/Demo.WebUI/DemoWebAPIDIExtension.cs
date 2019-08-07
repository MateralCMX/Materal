using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Demo.DependencyInjection;
using TTA.Core.DependencyInjection;

namespace Demo.WebUI
{
    /// <summary>
    /// WebUI依赖注入扩展
    /// </summary>
    public static class DemoWebUIDIExtension
    {
        /// <summary>
        /// 添加WebAPI服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddDemoWebUIServices(this IServiceCollection services)
        {
            services.AddDemoServices();
            services.AddAutoMapperService(Assembly.Load("Demo.ServiceImpl"), Assembly.Load("Demo.PresentationModel"));
        }
    }
}
