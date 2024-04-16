namespace Materal.Logger
{
    /// <summary>
    /// 日志监控器
    /// </summary>
    public interface ILogMonitor
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Task OnLogAsync(Log log);
    }
}
