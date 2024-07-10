namespace Materal.Tools.Core.Logger
{
    /// <summary>
    /// 日志监听器
    /// </summary>
    public interface ILoggerObserver
    {
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="log"></param>
        void OnLog(Log log);
    }
}
