using Materal.Logger.BatchLogger;
using Materal.Utils.Http;

namespace Materal.Logger.HttpLogger
{
    /// <summary>
    /// Http日志写入器
    /// </summary>
    public class HttpLoggerWriter(IOptionsMonitor<LoggerOptions> options, IHostLogger hostLogger) : BatchLoggerWriter<HttpLoggerTargetOptions>(options)
    {
        private readonly HttpHelper _httpHelper = new();
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="batchLogs"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task LogAsync(BatchLog<HttpLoggerTargetOptions>[] batchLogs)
        {
            HttpLog[] httpLogs = batchLogs.Select(m => new HttpLog(m, Options.CurrentValue)).ToArray();
            IGrouping<string, HttpLog>[] httpModels = httpLogs.GroupBy(m => m.Url).ToArray();
            Parallel.ForEach(httpModels, item =>
            {
                try
                {
                    HttpLog model = item.First();
                    string data = item.Select(m => m.Log).ToJson();
                    _httpHelper.SendAsync(model.Url, model.HttpMethod, null, data).Wait();
                }
                catch (Exception exception)
                {
                    hostLogger.LogError($"发送Http日志[{item.Key}]失败", exception);
                }
            });
            await Task.CompletedTask;
        }
        private class HttpLog(BatchLog<HttpLoggerTargetOptions> batchLog, LoggerOptions options)
        {
            /// <summary>
            /// 日志
            /// </summary>
            public Log Log { get; } = batchLog.Log;
            /// <summary>
            /// 地址
            /// </summary>
            public string Url { get; } = batchLog.Log.ApplyText(batchLog.TargetOptions.Url, options);
            /// <summary>
            /// Http方法
            /// </summary>
            public HttpMethod HttpMethod { get; } = batchLog.TargetOptions.GetHttpMethod();
        }
    }
}
