using System.CommandLine;
using System.Net.WebSockets;
using System.Text;

namespace Materal.Logger.LoggerTrace
{
    /// <summary>
    /// 程序
    /// </summary>
    public class Program
    {
        private static ClientWebSocket? _webSocket;
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            Option<string> urlOption = new("--Url", "指定服务地址");
            urlOption.AddAlias("-u");
            urlOption.IsRequired = true;
            urlOption.SetDefaultValue("127.0.0.1:5002");
            Option<string> targetOption = new("--Target", "指定目标日志等级[Trace,Debug,Information,Warning,Error,Critical]");
            targetOption.AddAlias("-t");
            targetOption.IsRequired = false;
            Option<string> ignoreOption = new("--Ignore", "指定忽略日志等级[Trace,Debug,Information,Warning,Error,Critical]");
            ignoreOption.AddAlias("-i");
            ignoreOption.IsRequired = false;
            RootCommand rootCommand = new("MateralLogger远程追踪器");
            rootCommand.AddOption(urlOption);
            rootCommand.AddOption(targetOption);
            rootCommand.AddOption(ignoreOption);
            rootCommand.SetHandler(ExcuteListenerAsync, urlOption, targetOption, ignoreOption);
            return await rootCommand.InvokeAsync(args);
        }
        /// <summary>
        /// 执行监听
        /// </summary>
        /// <param name="url"></param>
        /// <param name="targets"></param>
        /// <param name="ignores"></param>
        /// <returns></returns>
        private static async Task ExcuteListenerAsync(string url, string? targets, string? ignores)
        {
            Uri serverUri = new($"ws://{url}");
            List<LogLevel> targetLogLevels = [];
            List<LogLevel> ignoreLogLevels = [];
            if (targets is not null && !string.IsNullOrWhiteSpace(targets))
            {
                string[] targetLevels = targets.Split(",");
                targetLogLevels.AddRange(targetLevels.Select(m => (LogLevel)Enum.Parse(typeof(LogLevel), m)));
            }
            if (ignores is not null && !string.IsNullOrWhiteSpace(ignores))
            {
                string[] ignoreLevels = ignores.Split(",");
                ignoreLogLevels.AddRange(ignoreLevels.Select(m => (LogLevel)Enum.Parse(typeof(LogLevel), m)));
            }
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
                    if (message.IsJson())
                    {
                        LogMessageModel model = message.JsonToObject<LogMessageModel>();
                        if (ignoreLogLevels.Count > 0 && ignoreLogLevels.Contains(model.Level)) continue;
                        if (targetLogLevels.Count > 0 && !targetLogLevels.Contains(model.Level)) continue;
                        ConsoleQueue.WriteLine(model.GetWriteMessage(), model.GetWriteMessageColor());
                    }
                    else
                    {
                        ConsoleQueue.WriteLine(message);
                    }
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
            if (_webSocket is null || _webSocket.CloseStatus is not null || _webSocket.State != WebSocketState.Open) return;
            _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None).Wait();
        }
    }
}