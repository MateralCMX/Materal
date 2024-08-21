namespace Materal.Logger.Extensions
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 开始作用域
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="scopeName"></param>
        /// <param name="scopeData"></param>
        /// <returns></returns>
        public static IDisposable? BeginScope(this ILogger logger, string scopeName, Dictionary<string, object?> scopeData)
            => logger.BeginScope(new LoggerScope(scopeName, scopeData));
    }
}
