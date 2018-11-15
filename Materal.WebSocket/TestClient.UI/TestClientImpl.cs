using TestClient.Commands;
using TestClient.WebSocketClient;
using TestClient.WebSocketClient.DotNetty;
using TestClient.WebSocketClient.WebStock;

namespace TestClient.UI
{
    public class TestClientImpl : ITestClient
    {
        public bool IsAutoReload { get; set; }
        private readonly ITestWebSocketClient _testWebSocketClient;
        private readonly ITestWebSocketClientConfig _testWebSocketClientConfig;
        public TestClientImpl(ITestWebSocketClient testWebSocketClient, ITestWebSocketClientConfig testWebSocketClientConfig)
        {
            _testWebSocketClient = testWebSocketClient;
            _testWebSocketClientConfig = testWebSocketClientConfig;
        }
        public void Dispose()
        {
            Stop();
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
            _testWebSocketClient.SendCommandAsync(command);
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
