using Materal.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TestClient.WebSocketClient;
using TestClient.WebSocketClient.DotNetty;
using TestClient.WebSocketClient.WebStock;

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
            Services.AddSingleton<ITestWebSocketClient, TestWebSocketClientImpl>();
            Services.AddSingleton<ITestWebSocketClientConfig, WebSocketTestWebSocketClientConfig>();
            //Services.AddSingleton<ITestWebSocketClient, DotNettyTestWebStockClientImpl>();
            //Services.AddSingleton<ITestWebSocketClientConfig, DotNettyTestWebSocketClientConfig>();
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
