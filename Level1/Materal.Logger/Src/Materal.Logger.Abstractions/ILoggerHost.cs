namespace Materal.Logger.Abstractions
{
    /// <summary>
    /// 日志主机
    /// </summary>
    public interface ILoggerHost
    {
        /// <summary>
        /// 配置
        /// </summary>
        LoggerOptions Options { get; set; }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        void Log(Log log);
    }
}
