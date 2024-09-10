namespace Materal.MergeBlock.AccessLog.Models
{
    /// <summary>
    /// 写入日志模型
    /// </summary>
    public class WriteLoggerModel(DateTime startTime, DateTime? endTime, LogLevel level, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = startTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; } = endTime;
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
        public string LogMessage => $"[{Response.StatusCode}|{ElapsedMilliseconds}ms|{Request.Method}]{Request.Scheme}://{Request.Host}:{Request.Port}{Request.Path}";
    }
}
