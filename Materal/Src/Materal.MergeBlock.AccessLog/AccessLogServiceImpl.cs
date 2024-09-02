using Materal.Logger.Abstractions;
using Materal.MergeBlock.AccessLog.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks.Dataflow;

namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 访问日志服务
    /// </summary>
    public class AccessLogServiceImpl : IAccessLogService
    {
        private readonly ILogger<AccessLogServiceImpl> _logger;
        private readonly IOptionsMonitor<AccessLogOptions> _config;
        private readonly ActionBlock<WriteLoggerModel> _writeLogBlock;
        private readonly IBigAccessLogInterceptor _bigAccessLogInterceptor;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        /// <param name="bigAccessLogInterceptor"></param>
        public AccessLogServiceImpl(ILogger<AccessLogServiceImpl> logger, IOptionsMonitor<AccessLogOptions> config, IBigAccessLogInterceptor? bigAccessLogInterceptor = null)
        {
            _logger = logger;
            _config = config;
            _bigAccessLogInterceptor = bigAccessLogInterceptor ?? new TruncatedBigAccessLogInterceptor(config);
            _writeLogBlock = new(WriteAccessLog);
        }
        /// <summary>
        /// 写入访问日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="logLevel"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteAccessLog(DateTime startTime, DateTime? endTime, LogLevel logLevel, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
        {
            WriteLoggerModel model = new(startTime, endTime, logLevel, request, response, elapsedMilliseconds, exception);
            _writeLogBlock.Post(model);
        }
        /// <summary>
        /// 写入严重日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteCriticalLog(DateTime startTime, DateTime? endTime, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(startTime, endTime, LogLevel.Critical, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteDebugLog(DateTime startTime, DateTime? endTime, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(startTime, endTime, LogLevel.Debug, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteErrorLog(DateTime startTime, DateTime? endTime, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(startTime, endTime, LogLevel.Error, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入信息日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteInformationLog(DateTime startTime, DateTime? endTime, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(startTime, endTime, LogLevel.Information, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入跟踪日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteTraceLog(DateTime startTime, DateTime? endTime, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(startTime, endTime, LogLevel.Trace, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入警告日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteWarningLog(DateTime startTime, DateTime? endTime, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(startTime, endTime, LogLevel.Warning, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        private void WriteAccessLog(WriteLoggerModel model)
        {
            if (model.Request.Body?.Length > _config.CurrentValue.MaxBodySize)
            {
                model.Request.Body = _bigAccessLogInterceptor.Handler(model.Request.Body);
            }
            if (model.Response.Body?.Length > _config.CurrentValue.MaxBodySize)
            {
                model.Response.Body = _bigAccessLogInterceptor.Handler(model.Response.Body);
            }
            Dictionary<string, object?> loggerScopeData = new()
            {
                [nameof(model.Request)] = model.Request,
                [nameof(model.Response)] = model.Response,
                [nameof(model.ElapsedMilliseconds)] = model.ElapsedMilliseconds,
                [nameof(model.StartTime)] = model.StartTime,
                [nameof(model.EndTime)] = model.EndTime,
            };
            LoggerScope loggerScope = new(ConstData.AccessLogScopeName, loggerScopeData);
            using IDisposable? scope = _logger.BeginScope(loggerScope);
            _logger.Log(model.Level, model.Exception, model.LogMessage);
        }
    }
}
