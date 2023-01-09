//using NetCore.AutoRegisterDi;
//using RC.Core;
//using RC.Core.WebAPI;
//using RC.Demo.Common;
//using System.Reflection;
//using RC.Demo.RepositoryImpl;

//namespace RC.Demo
//{
//    /// <summary>
//    /// Demo依赖注入扩展
//    /// </summary>
//    public static class DemoDIExtension
//    {
//        /// <summary>
//        /// 添加Demo服务
//        /// </summary>
//        /// <param name="services"></param>
//        /// <returns></returns>
//        public static IServiceCollection AddDemoService(this IServiceCollection services)
//        {
//            Assembly currentAssembly = Assembly.GetExecutingAssembly();
//            string basePath = AppDomain.CurrentDomain.BaseDirectory;
//            string[] swaggerXmlPaths = new[]
//            {
//                $"{basePath}{currentAssembly.GetName().Name}.xml",
//            };
//            services.AddWebAPIServices<DemoDBContext>(swaggerXmlPaths, ApplicationConfig.DBConfig);
//            services.AddRCServices(currentAssembly);
//            services.RegisterAssemblyPublicNonGenericClasses(currentAssembly)
//                .Where(m => !m.IsAbstract && (m.Name.EndsWith("RepositoryImpl") || m.Name.EndsWith("ServiceImpl")))
//                .AsPublicImplementedInterfaces();
//            services.AddIntegrationEventBus("RCDemoQueue");
//            services.AddIntegrationEventHandlers(currentAssembly);
//            return services;
//        }
//    }
//}
