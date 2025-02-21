namespace Materal.Logger.Abstractions
{
    /// <summary>
    /// 日志拦截器
    /// </summary>
    public interface ILoggerInterceptor
    {
        /// <summary>
        /// 写日志时
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool OnLog(Log log);
    }
}
