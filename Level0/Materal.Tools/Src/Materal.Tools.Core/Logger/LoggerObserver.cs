namespace Materal.Tools.Core.Logger
{
    internal class LoggerObserver(ILoggerObserver loggerObserver) : IObserver<KeyValuePair<string, object?>>
    {
        /// <summary>
        /// 完成
        /// </summary>
        public virtual void OnCompleted() { }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="error"></param>
        public virtual void OnError(Exception error) { }
        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="value"></param>
        public virtual void OnNext(KeyValuePair<string, object?> value)
        {
            if (value.Key != "OnLog" || value.Value is not Log log) return;
            loggerObserver.OnLog(log);
        }
    }
}
