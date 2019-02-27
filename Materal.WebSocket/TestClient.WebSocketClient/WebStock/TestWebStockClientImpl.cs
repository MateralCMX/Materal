using Materal.WebSocket.Client;
using Materal.WebSocket.Events;
using Materal.WebSocket.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestWebSocket.Common;

namespace TestClient.WebSocketClient.WebStock
{
    public class TestWebSocketClientImpl : WebSocketClientImpl, ITestWebSocketClient
    {
        private readonly IServiceProvider _serviceProvider;
        public TestWebSocketClientImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task HandleEventAsync(IEvent eventM)
         {
            try
            {
                var commandBus = (IEventBus)_serviceProvider.GetRequiredService(typeof(IEventBus));
                await commandBus.SendAsync(eventM);
            }
            catch (MateralWebSocketException ex)
            {
                ConsoleHelper.TestWriteLine(ex.Message, "未能解析事件");
            }
        }

        public override void HandleEvent(IEvent eventM)
        {
            try
            {
                var commandBus = (IEventBus)_serviceProvider.GetRequiredService(typeof(IEventBus));
                Task.Run(() => commandBus.SendAsync(eventM));
            }
            catch (MateralWebSocketException ex)
            {
                ConsoleHelper.TestWriteLine(ex.Message, "未能解析事件");
            }
        }
    }
}
