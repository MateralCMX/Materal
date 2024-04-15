namespace Materal.Logger
{
    /// <summary>
    /// 日志主机控制台日志记录器
    /// </summary>
    public class ConsoleHostLogger : IHostLogger
    {
        private LoggerOptions? _options;
        /// <summary>
        /// 配置
        /// </summary>
        public LoggerOptions Options
        {
            get => _options ?? throw new LoggerException("获取日志选项失败");
            set => _options = value;
        }
        /// <summary>
        /// 最小等级
        /// </summary>
        public LogLevel MinLogLevel => Options.MinLogHostLogLevel;
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevel MaxLogLevel => Options.MaxLogHostLogLevel;
        /// <summary>
        /// 启动
        /// </summary>
        public async Task StartAsync()
        {
            ConsoleQueue.Start();
            await Task.CompletedTask;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync() => await ConsoleQueue.ShutdownAsync();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="consoleColor"></param>
        public void Log(string message, LogLevel logLevel, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            if (logLevel < MinLogLevel || logLevel > MaxLogLevel) return;
            ConsoleQueue.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|MateralLogger|{logLevel}|{message}", consoleColor);
        }
        /// <summary>
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message) => Log(message, LogLevel.Debug, ConsoleColor.DarkGreen);
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        public void LogInfomation(string message) => Log(message, LogLevel.Information, ConsoleColor.Gray);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message) => Log(message, LogLevel.Warning, ConsoleColor.DarkYellow);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void LogWarning(string message, Exception? exception) => LogWarning(GetErrorMessage(message, exception));
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message) => Log(message, LogLevel.Error, ConsoleColor.DarkRed);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void LogError(string message, Exception? exception) => LogError(GetErrorMessage(message, exception));
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static string GetErrorMessage(string message, Exception? exception)
        {
            StringBuilder messageBuild = new();
            messageBuild.Append(message);
            if (exception is not null)
            {
                messageBuild.AppendLine();
                messageBuild.Append(exception.GetErrorMessage());
            }
            return messageBuild.ToString();
        }
    }
}
