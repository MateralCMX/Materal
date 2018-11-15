using System;
using System.Threading.Tasks;
using Materal.WebSocket.Client;
using Materal.WebSocket.Events;
using Materal.WebSocket.Model;
using Microsoft.Extensions.DependencyInjection;
using TestClient.Common;
using TestClient.Events;

namespace TestClient.WebSocketClient.WebStock
{
    public class TestWebSocketClientImpl : WebSocketClientImpl, ITestWebSocketClient
    {
        private readonly IServiceProvider _serviceProvider;
        public TestWebSocketClientImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task HandleEventAsync(Event eventM)
         {
            try
            {
                var commandBus = (IEventBus)_serviceProvider.GetRequiredService(typeof(IEventBus));
                await commandBus.SendAsync(eventM);
            }
            catch (MateralWebSocketException ex)
            {
                ConsoleHelper.TestClientWriteLine(ex.Message, "未能解析事件");
            }
        }
    }
}
