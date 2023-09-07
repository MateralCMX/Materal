using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Utils.Http;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// Http日志处理器
    /// </summary>
    public class HttpLoggerHandler : BufferLoggerHandler<HttpLoggerHandlerModel, HttpLoggerTargetConfigModel>
    {
        private readonly static IHttpHelper _httpHelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        static HttpLoggerHandler()
        {
            _httpHelper = new HttpHelper();
        }
        /// <summary>
        /// 处理合格的数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void HandlerOKData(HttpLoggerHandlerModel[] datas)
        {
            IGrouping<string, HttpLoggerHandlerModel>[] models = datas.GroupBy(m => m.Url).ToArray();
            Parallel.ForEach(models, item =>
            {
                try
                {
                    HttpLoggerHandlerModel model = item.First();
                    switch (model.HttpMethod.Method)
                    {
                        case "GET":
                        case "DELETE":
                            LoggerLog.LogWarning("Http日志处理器不支持GET和DELETE方法");
                            //_httpHelper.SendAsync($"{model.Url}?{data}", model.HttpMethod).Wait();
                            break;
                        default:
                            string data = $"[{string.Join(",", item.Select(m => m.Data))}]";
                            _httpHelper.SendAsync(model.Url, model.HttpMethod, null, data).Wait();
                            break;
                    }
                }
                catch (MateralHttpException exception)
                {
                    LoggerLog.LogWarning($"日志Http发送[{item.Key}]失败", exception);
                }
                catch (Exception exception)
                {
                    LoggerLog.LogWarning($"日志Http发送[{item.Key}]失败", exception);
                }
            });
        }
    }
}
