using Materal.BaseCore.Common;
using Materal.BaseCore.HttpClient.Extensions;
using Materal.BaseCore.WebAPI.Common;
using MBC.Core.WebAPI;
using MBC.Demo.Common;
using MBC.Demo.EFRepository;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace MBC.Demo.WebAPI
{
    /// <summary>
    /// Authority依赖注入扩展
    /// </summary>
    public static class DemoDIExtension
    {
        /// <summary>
        /// 添加Authority服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDemoService(this IServiceCollection services)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}MBC.Demo.WebAPI.xml",
                $"{basePath}MBC.Demo.DataTransmitModel.xml",
                $"{basePath}MBC.Demo.PresentationModel.xml",
            };
            services.AddSignalR();
            services.AddMBCService<DemoDBContext>(DemoConfig.DBConfig, config =>
            {
                config.AddSignalRSwaggerGen();
            }, swaggerXmlPaths, Assembly.Load("MBC.Demo.EFRepository"));
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("MBC.Demo.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.AddHttpClientService(WebAPIConfig.AppName, Assembly.Load("MBC.Demo.HttpClient"));
            return services;
        }
    }
}
