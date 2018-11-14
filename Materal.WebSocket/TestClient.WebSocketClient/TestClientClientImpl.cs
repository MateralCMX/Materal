using Materal.WebSocket;
using Materal.WebSocket.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TestClient.Common;
using TestClient.Events;

namespace TestClient.WebStockClient
{
    public class TestClientClientImpl : ClientImpl, ITestClientClient
    {
        private readonly IServiceProvider _serviceProvider;
        public TestClientClientImpl(IServiceProvider serviceProvider)
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
            catch (Exception ex)
            {
                ConsoleHelper.TestClientWriteLine(ex.Message, "未能解析事件");
            }
        }
    }
}
