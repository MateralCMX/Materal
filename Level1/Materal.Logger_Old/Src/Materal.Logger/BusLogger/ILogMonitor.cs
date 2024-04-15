namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 日志监控器
    /// </summary>
    public interface ILogMonitor
    {
        /// <summary>
        /// 处理新日志信息
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns></returns>
        Task HandlerNewLogInfoAsync(LogModel logModel);
    }
}
