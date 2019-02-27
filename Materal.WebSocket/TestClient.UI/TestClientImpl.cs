using TestClient.WebSocketClient;
using TestClient.WebSocketClient.WebStock;
using TestWebSocket.Commands;

namespace TestClient.UI
{
    public class TestClientImpl : ITestClient
    {
        private readonly ITestWebSocketClient _testWebSocketClient;
        private readonly ITestWebSocketClientConfig _testWebSocketClientConfig;
        public TestClientImpl(ITestWebSocketClient testWebSocketClient, ITestWebSocketClientConfig testWebSocketClientConfig)
        {
            _testWebSocketClient = testWebSocketClient;
            _testWebSocketClientConfig = testWebSocketClientConfig;
        }
        public void Init()
        {
            _testWebSocketClient.SetConfig(_testWebSocketClientConfig);
        }
        public void Start()
        {
            StartTestWebSocketClient();
        }
        public void Stop()
        {
            _testWebSocketClient?.StopAsync();
        }

        public void SendMessage(string message)
        {
            var command = new TestCommand
            {
                StringData = message
            };
            _testWebSocketClient.SendCommandByStringAsync(command);
        }

        /// <summary>
        /// 启动测试WebSocket客户端
        /// </summary>
        private void StartTestWebSocketClient()
        {
            //_testWebSocketClient.StartAsync<DotNettyTestWebStockHandler>();
            _testWebSocketClient.StartAsync<TestWebStockHandler>();
        }
    }
}
