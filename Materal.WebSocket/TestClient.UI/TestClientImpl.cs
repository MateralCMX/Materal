using Materal.WebSocket.Model;
using System;
using System.Text;
using System.Threading.Tasks;
using Materal.Common;
using Materal.ConvertHelper;
using TestClient.Commands;
using TestClient.Common;
using TestClient.Events;
using TestClient.WebSocketClient;

namespace TestClient.UI
{
    public class TestClientImpl : ITestClient
    {
        public bool IsAutoReload { get; set; }
        private readonly ITestWebSocketClient _testWebSocketClient;
        public TestClientImpl(ITestWebSocketClient testWebSocketClient)
        {
            _testWebSocketClient = testWebSocketClient;
        }
        public void Dispose()
        {
            Stop();
        }
        public void Init()
        {
            var testClientConfigModel = new WebSocketClientConfigModel
            {
                Url = "ws://127.0.0.1:10001",
                EncodingType = Encoding.UTF8,
                ServerMessageMaxLength = 102400
            };
            _testWebSocketClient.OnConfigChange += TestWebSocketClientOnConfigChange;
            _testWebSocketClient.OnSendCommand += TestWebSocketClientOnSendCommand;
            _testWebSocketClient.OnReceiveEvent += TestWebSocketClientOnReceiveEvent;
            _testWebSocketClient.OnStateChange += TestWebSocketClientOnStateChange;
            _testWebSocketClient.SetConfig(testClientConfigModel);
        }
        public void Start()
        {
            StartExternalWebSocketClient();
        }
        public void Stop()
        {
            _testWebSocketClient?.Dispose();
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="args"></param>
        private void TestWebSocketClientOnSendCommand(SendCommandEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine(args.Command.StringData, "发送");
        }
        /// <summary>
        /// 状态更改事件
        /// </summary>
        /// <param name="args"></param>
        private void TestWebSocketClientOnStateChange(ConnectServerEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine(args.Message);
            switch (args.State)
            {
                case WebSocketClientStateEnum.NotConfigured:
                    break;
                case WebSocketClientStateEnum.Ready:
                    break;
                case WebSocketClientStateEnum.Runing:
                    var result = new ResultModel
                    {
                        ResultType = ResultTypeEnum.Success,
                        Message = "Test"
                    };
                    _testWebSocketClient.SendCommandAsync(new Command("TestCommandHandler")
                        {
                            StringData = result.ToJson(),
                            Message = "qwer"
                        });
                    _testWebSocketClient.StartListeningEventAsync().Start();
                    break;
                case WebSocketClientStateEnum.ConnectionFailed:
                    if (IsAutoReload)
                    {
                        ConsoleHelper.TestClientWriteLine("重新连接");
                        _testWebSocketClient.ReloadAsync().Wait();
                        _testWebSocketClient.StartListeningEventAsync().Wait();
                    }
                    break;
                case WebSocketClientStateEnum.Stop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 接收事件
        /// </summary>
        /// <param name="args"></param>
        private void TestWebSocketClientOnReceiveEvent(ReceiveEventEventArgs args)
        {
            HandleEventAsync(args).Start();
        }
        /// <summary>
        /// 初始事件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task HandleEventAsync(ReceiveEventEventArgs args)
        {
            string message = _testWebSocketClient.Config.EncodingType.GetString(args.ByteArrayData);
            ConsoleHelper.TestClientWriteLine(message, "接收");
            var @event = new Event("TestEventHandler")
            {
                Message = "测试命令",
                ByteArrayData = args.ByteArrayData,
                StringData = message
            };
            await _testWebSocketClient.HandleEventAsync(@event);
        }
        /// <summary>
        /// 配置更改事件
        /// </summary>
        /// <param name="args"></param>
        private void TestWebSocketClientOnConfigChange(ConfigEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine(args.Message);
        }
        /// <summary>
        /// 启动外接WebSocket客户端
        /// </summary>
        private async void StartExternalWebSocketClient()
        {
            await _testWebSocketClient.StartAsync();
        }
    }
}
