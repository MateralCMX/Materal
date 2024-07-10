namespace Materal.Tools.Core.Logger
{
    /// <summary>
    /// 默认日志监听器
    /// </summary>
    /// <param name="onLog"></param>
    public class DefaultLoggerObserver(Action<Log> onLog) : ILoggerObserver
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        public virtual void OnLog(Log log) => onLog.Invoke(log);
    }
}
