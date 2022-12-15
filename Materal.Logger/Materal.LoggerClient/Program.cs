using Materal.Common;
using Materal.Logger.Message;
using Materal.LoggerClient.EventHandlers;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text;

namespace Materal.LoggerClient
{
    public class Program
    {
        private static Uri serverUri = new("ws://127.0.0.1:8800");
        private static ClientWebSocket? _webSocket;
        private static EventBus? _eventBus;
        public static async Task Main(string[] args)
        {
            if (!HandlerArgs(args))
            {
                return;
            }
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            await ExcuteListenerAsync();
        }
        private static bool HandlerArgs(string[] args)
        {
            if (args.Length == 0)
            {
                InitByConsoleCommand();
            }
            else
            {
                foreach (string arg in args)
                {
                    string[] temps = arg.Split("=");
                    if(temps.Length == 2)
                    {
                        temps[0] = temps[0].ToLower();
                        switch (temps[0])
                        {
                            case "-url":
                            case "-u":
                            case "-host":
                                serverUri = new Uri($"ws://{temps[1]}");
                                break;
                            case "-t":
                            case "-target":
                                string[] targetLevels = temps[1].Split(",");
                                LogMessageEventHandler.TargetLogLevels = targetLevels.Select(m => (LogLevel)Enum.Parse(typeof(LogLevel), m)).ToList();
                                break;
                            case "-i":
                            case "-ignore":
                                string[] ignoreLevels = temps[1].Split(",");
                                LogMessageEventHandler.IgnoreLogLevels = ignoreLevels.Select(m => (LogLevel)Enum.Parse(typeof(LogLevel), m)).ToList();
                                break;
                            default:
                                StringBuilder helperString = new();
                                helperString.AppendLine($"未识别命令{temps[0]}");
                                helperString.AppendLine("-url -u -host:指定目标地址 -host=127.0.0.1:8800");
                                helperString.AppendLine("-target -t:指定查看的等级 可通过,指定多个 -target=Warning,Error");
                                helperString.AppendLine("-ignore -i:指定忽略的等级 可通过,指定多个 -ignore=Trace,Debug");
                                ConsoleQueue.WriteLine(helperString.ToString());
                                return false;
                        }
                    }
                    else if(temps.Length == 1)
                    {
                        serverUri = new Uri($"ws://{temps[0]}");
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 通过控制台命令初始化
        /// </summary>
        private static void InitByConsoleCommand()
        {
            ConsoleQueue.WriteLine("请输入Host[回车使用127.0.0.1:8800]");
            string? hostValue = Console.ReadLine();
            if (!string.IsNullOrEmpty(hostValue))
            {
                hostValue = $"ws://{hostValue}";
                serverUri = new Uri(hostValue);
            }
        }
        /// <summary>
        /// 执行监听
        /// </summary>
        /// <returns></returns>
        private static async Task ExcuteListenerAsync()
        {
            _eventBus = new EventBus();
            _eventBus.Subscribe<LogMessageEvent, LogMessageEventHandler>();
            _webSocket = new();
            _webSocket.Options.KeepAliveInterval = TimeSpan.FromHours(1);
            try
            {
                await _webSocket.ConnectAsync(serverUri, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            ConsoleQueue.WriteLine("连接成功，开始监听日志信息");
            while (_webSocket.State == WebSocketState.Open)
            {
                try
                {
                    byte[] buffer = new byte[4096];
                    WebSocketReceiveResult result = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    _eventBus.Handler(message);
                }
                catch (Exception)
                {
                    if (_webSocket.State != WebSocketState.Open) return;
                }
            }
        }
        /// <summary>
        /// 程序退出时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            if (_webSocket == null || _webSocket.CloseStatus != null || _webSocket.State != WebSocketState.Open) return;
            _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None).Wait();
        }
    }
}