using Materal.DotNetty.Client;
using Materal.WebSocket.Events;
using Materal.WebSocket.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestWebSocket.Common;

namespace TestClient.WebSocketClient.DotNetty
{
    public class DotNettyTestWebStockClientImpl : DotNettyClientImpl, ITestWebSocketClient
    {
        private readonly IServiceProvider _serviceProvider;

        public DotNettyTestWebStockClientImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task HandleEventAsync(IEvent eventM)
        {
            try
            {
                var eventBus = (IEventBus)_serviceProvider.GetRequiredService(typeof(IEventBus));
                await eventBus.SendAsync(eventM);
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
                var eventBus = (IEventBus)_serviceProvider.GetRequiredService(typeof(IEventBus));
                Task.Run(() => eventBus.SendAsync(eventM));
            }
            catch (MateralWebSocketException ex)
            {
                ConsoleHelper.TestWriteLine(ex.Message, "未能解析事件");
            }
        }
    }
}
