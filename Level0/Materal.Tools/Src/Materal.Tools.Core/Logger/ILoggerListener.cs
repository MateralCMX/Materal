namespace Materal.Tools.Core.Logger
{
    /// <summary>
    /// 日志监听器
    /// </summary>
    public interface ILoggerListener : IDisposable
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        void Log(Log log);
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="loggerObserver"></param>
        IDisposable Subscribe(ILoggerObserver loggerObserver);
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="onLog"></param>
        IDisposable Subscribe(Action<Log> onLog);
    }
}
