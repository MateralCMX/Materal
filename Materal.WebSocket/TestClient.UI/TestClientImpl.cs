using Materal.WebSocket.Model;
using MateralTools.Base.Model;
using MateralTools.MConvert.Manager;
using MateralTools.MResult.Model;
using System;
using System.Text;
using TestClient.Commands;
using TestClient.Common;
using TestClient.Events;
using TestClient.WebStockClient;

namespace TestClient.UI
{
    public class TestClientImpl : ITestClient
    {
        public bool IsAutoReload { get; set; }
        private readonly ITestClientClient _testClientClient;

        public TestClientImpl(ITestClientClient testClientClient)
        {
            _testClientClient = testClientClient;
        }
        public void Dispose()
        {
            Stop();
        }
        public void Init()
        {
            var testClientConfigModel = new ClientConfigModel
            {
                Url = "ws://127.0.0.1:10001",
                EncodingType = Encoding.UTF8,
                ServerMessageMaxLength = 102400
            };
            _testClientClient.OnConfigChange += TestClientClientOnConfigChange;
            _testClientClient.OnSendCommand += TestClientClientOnSendCommand;
            _testClientClient.OnReceiveEvent += TestClientClientOnReceiveEvent;
            _testClientClient.OnStateChange += TestClientClientOnStateChange;
            _testClientClient.SetConfig(testClientConfigModel);
        }

        public void Start()
        {
            StartExternalWebStockClient();
        }
        public void Stop()
        {
            _testClientClient?.Dispose();
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="args"></param>
        private void TestClientClientOnSendCommand(SendCommandEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine(args.Command.StringData, "发送");
        }
        /// <summary>
        /// 状态更改事件
        /// </summary>
        /// <param name="args"></param>
        private void TestClientClientOnStateChange(ConnectServerEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine(args.Message);
            switch (args.State)
            {
                case ClientStateEnum.NotConfigured:
                    break;
                case ClientStateEnum.Ready:
                    break;
                case ClientStateEnum.Runing:
                    var result = new DataResult
                    {
                        ResultType = MResultType.Success,
                        Message = "Test"
                    };
                    _testClientClient.SendCommandAsync(new Command("TestCommandHandler")
                        {
                            StringData = result.MToJson(),
                            Message = "qwer"
                        });
                    _testClientClient.StartListeningEventAsync().Wait();
                    break;
                case ClientStateEnum.ConnectionFailed:
                    if (IsAutoReload)
                    {
                        ConsoleHelper.TestClientWriteLine("重新连接");
                        _testClientClient.ReloadAsync().Wait();
                        _testClientClient.StartListeningEventAsync().Wait();
                    }
                    break;
                case ClientStateEnum.Stop:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 接收事件
        /// </summary>
        /// <param name="args"></param>
        private void TestClientClientOnReceiveEvent(ReceiveEventEventArgs args)
        {
            try
            {
                string message = _testClientClient.Config.EncodingType.GetString(args.ByteArrayData);
                ConsoleHelper.TestClientWriteLine(message, "接收");
                var @event = new Event("TestEventHandler")
                {
                    Message = "测试命令",
                    ByteArrayData = args.ByteArrayData,
                    StringData = message
                };
                _testClientClient.HandleEventAsync(@event);
            }
            catch (MException ex)
            {
                ConsoleHelper.TestClientWriteLine(ex.Message, "解析错误");
            }
        }
        /// <summary>
        /// 配置更改事件
        /// </summary>
        /// <param name="args"></param>
        private void TestClientClientOnConfigChange(ConfigEventArgs args)
        {
            ConsoleHelper.TestClientWriteLine(args.Message);
        }
        /// <summary>
        /// 启动外接WebStock客户端
        /// </summary>
        private async void StartExternalWebStockClient()
        {
            await _testClientClient.StartAsync();
        }
    }
}
