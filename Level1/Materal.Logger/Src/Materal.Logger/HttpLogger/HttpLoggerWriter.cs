using Materal.Utils.Http;

namespace Materal.Logger.HttpLogger
{
    /// <summary>
    /// Http日志写入器
    /// </summary>
    public class HttpLoggerWriter(HttpLoggerTargetConfig targetConfig) : BatchLoggerWriter<HttpLoggerWriterModel, HttpLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        private readonly HttpHelper _httpHelper = new();
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(HttpLoggerWriterModel[] models)
        {
            IGrouping<string, HttpLoggerWriterModel>[] httpModels = models.GroupBy(m => m.Url).ToArray();
            Parallel.ForEach(httpModels, item =>
            {
                try
                {
                    HttpLoggerWriterModel model = item.First();
                    switch (model.HttpMethod.Method)
                    {
                        case "POST":
                        case "PATCH":
                        case "PUT":
                            string data = item.Select(m => LogMessageModel.Pass(m)).ToJson();
                            _httpHelper.SendAsync(model.Url, model.HttpMethod, null, data).Wait();
                            break;
                        default:
                            LoggerHost.LoggerLog?.LogWarning($"Http日志处理器不支持{model.HttpMethod.Method}方法");
                            //_httpHelper.SendAsync($"{model.Url}?{data}", model.HttpMethod).Wait();
                            break;
                    }
                }
                catch (Exception exception)
                {
                    LoggerHost.LoggerLog?.LogError($"发送Http日志[{item.Key}]失败", exception);
                }
            });
            await Task.CompletedTask;
        }
    }
}
