using Materal.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TestClient.WebStockClient;

namespace TestClient.UI
{
    public class TestClientHelper
    {
        public static IServiceCollection Services;
        public static IServiceProvider ServiceProvider;
        /// <summary>
        /// 注册依赖注入
        /// </summary>
        public static void RegisterCustomerService()
        {
            Services = new ServiceCollection();
            Services.AddCommandBus(Assembly.Load("TestClient.CommandHandlers"));
            Services.AddEventBus(Assembly.Load("TestClient.EventHandlers"));
            Services.AddSingleton<ITestClientClient, TestClientClientImpl>();
        }
        /// <summary>
        /// Bulid服务
        /// </summary>
        public static void BulidService()
        {
            ServiceProvider = Services.BuildServiceProvider();
        }
    }
}
