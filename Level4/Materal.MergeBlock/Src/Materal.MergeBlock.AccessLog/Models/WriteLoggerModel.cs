namespace Materal.MergeBlock.AccessLog.Models
{
    /// <summary>
    /// 写入日志模型
    /// </summary>
    public class WriteLoggerModel(LogLevel level, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel Level { get; set; } = level;
        /// <summary>
        /// 请求
        /// </summary>
        public RequestModel Request { get; set; } = request;
        /// <summary>
        /// 响应
        /// </summary>
        public ResponseModel Response { get; set; } = response;
        /// <summary>
        /// 耗时
        /// </summary>
        public long ElapsedMilliseconds { get; } = elapsedMilliseconds;
        /// <summary>
        /// 异常
        /// </summary>
        public Exception? Exception { get; set; } = exception;
        /// <summary>
        /// 日志消息
        /// </summary>
        public string LogMessage=> $"[{Request.Method}] {Request.Scheme}://{Request.Host}:{Request.Host}{Request.Path} [{Response.StatusCode}] [{ElapsedMilliseconds}ms]";
    }
}
