namespace Materal.Logger
{
    /// <summary>
    /// 日志域
    /// </summary>
    public class LoggerScope : IDisposable
    {
        private readonly Logger _logger;
        /// <summary>
        /// 域
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="logger"></param>
        public LoggerScope(string scope, Logger logger)
        {
            Scope = scope;
            _logger = logger;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _logger.ExitScope();
            GC.SuppressFinalize(this);
        }
    }
}
