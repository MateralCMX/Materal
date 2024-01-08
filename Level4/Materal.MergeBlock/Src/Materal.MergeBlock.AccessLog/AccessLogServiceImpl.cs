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
        private readonly IOptionsMonitor<AccessLogConfig> _config;
        private readonly ActionBlock<WriteLoggerModel> _writeLogBlock;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public AccessLogServiceImpl(ILogger<AccessLogServiceImpl> logger, IOptionsMonitor<AccessLogConfig> config)
        {
            _logger = logger;
            _config = config;
            _writeLogBlock = new(WriteAccessLog);
        }
        /// <summary>
        /// 写入访问日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteAccessLog(LogLevel logLevel, RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
        {
            WriteLoggerModel model = new(logLevel, request, response, elapsedMilliseconds, exception);
            _writeLogBlock.Post(model);
        }
        /// <summary>
        /// 写入严重日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteCriticalLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(LogLevel.Critical, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteDebugLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(LogLevel.Debug, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteErrorLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(LogLevel.Error, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入信息日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteInformationLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(LogLevel.Information, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入跟踪日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteTraceLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(LogLevel.Trace, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入警告日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="elapsedMilliseconds"></param>
        /// <param name="exception"></param>
        public void WriteWarningLog(RequestModel request, ResponseModel response, long elapsedMilliseconds, Exception? exception = null)
            => WriteAccessLog(LogLevel.Warning, request, response, elapsedMilliseconds, exception);
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        private void WriteAccessLog(WriteLoggerModel model)
        {
            if (model.Request.Body?.Length > _config.CurrentValue.MaxBodySize)
            {
                model.Request.Body = model.Request.Body[.._config.CurrentValue.MaxBodySize];
            }
            if (model.Response.Body?.Length > _config.CurrentValue.MaxBodySize)
            {
                model.Response.Body = model.Response.Body[.._config.CurrentValue.MaxBodySize];
            }
            AdvancedScope advancedScope = new(ConstData.AccessLogScopeName, new Dictionary<string, object?>
            {
                [nameof(model.Request)] = model.Request,
                [nameof(model.Response)] = model.Response,
                [nameof(model.ElapsedMilliseconds)] = model.ElapsedMilliseconds
            });
            using IDisposable? scope = _logger.BeginScope(advancedScope);
            _logger.Log(model.Level, model.Exception, model.LogMessage);
        }
    }
}
