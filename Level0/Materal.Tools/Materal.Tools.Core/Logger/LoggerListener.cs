using System.Diagnostics;

namespace Materal.Tools.Core.Logger
{
    /// <summary>
    /// 日志监听器
    /// </summary>
    public class LoggerListener : ILoggerListener
    {
        private readonly DiagnosticListener diagnosticListener = new("Logger");
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="log"></param>
        public void Log(Log log) => diagnosticListener.Write("OnLog", log);
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="loggerObserver"></param>
        public IDisposable Subscribe(ILoggerObserver loggerObserver)
        {
            LoggerObserver observer = new(loggerObserver);
            return diagnosticListener.Subscribe(observer);
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="onLog"></param>
        /// <returns></returns>
        public IDisposable Subscribe(Action<Log> onLog)
        {
            DefaultLoggerObserver loggerObserver = new(onLog);
            return Subscribe(loggerObserver);
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() => diagnosticListener.Dispose();
    }
}
