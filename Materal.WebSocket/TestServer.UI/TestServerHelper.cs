using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Materal.WebSocket;

namespace TestServer.UI
{
    public class TestServerHelper
    {
        public static IServiceCollection Services;
        public static IServiceProvider ServiceProvider;
        static TestServerHelper()
        {
            RegisterServices();
            BuildServices();
        }
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterServices()
        {
            Services = new ServiceCollection();
            Services.AddCommandBus(Assembly.Load("TestWebSocket.CommandHandlers"));
            Services.AddSingleton<ITestServer, TestServerImpl>();
        }

        /// <summary>
        /// Build服务
        /// </summary>
        public static void BuildServices()
        {
            ServiceProvider = Services.BuildServiceProvider();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
