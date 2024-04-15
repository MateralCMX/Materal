namespace Materal.Logger
{
    /// <summary>
    /// 日志消息模型
    /// </summary>
    public partial class LogMessageModel
    {
        /// <summary>
        /// 获取写入消息
        /// </summary>
        /// <returns></returns>
        public string GetWriteMessage() => $"{CreateTime}|{Application}|{Level}|{Scope}|{CategoryName}|[{MachineName},{ProgressID},{ThreadID}]\r\n{Message}\r\n{Exception}";
        /// <summary>
        /// 获取写入消息颜色
        /// </summary>
        /// <returns></returns>
        public ConsoleColor GetWriteMessageColor() => Level switch
        {
            LogLevel.Trace => ConsoleColor.DarkGray,
            LogLevel.Debug => ConsoleColor.DarkGreen,
            LogLevel.Information => ConsoleColor.Gray,
            LogLevel.Warning => ConsoleColor.DarkYellow,
            LogLevel.Error => ConsoleColor.DarkRed,
            LogLevel.Critical => ConsoleColor.Red,
            _ => ConsoleColor.Gray,
        };
    }
}
