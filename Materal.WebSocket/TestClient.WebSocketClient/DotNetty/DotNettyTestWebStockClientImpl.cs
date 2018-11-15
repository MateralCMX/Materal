using Materal.DotNetty.Client;
using Materal.WebSocket.Events;
using Materal.WebSocket.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestClient.Common;

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
                var commandBus = (IEventBus)_serviceProvider.GetRequiredService(typeof(IEventBus));
                await commandBus.SendAsync(eventM);
            }
            catch (MateralWebSocketException ex)
            {
                ConsoleHelper.TestClientWriteLine(ex.Message, "未能解析事件");
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
                ConsoleHelper.TestClientWriteLine(ex.Message, "未能解析事件");
            }
        }
    }
}
