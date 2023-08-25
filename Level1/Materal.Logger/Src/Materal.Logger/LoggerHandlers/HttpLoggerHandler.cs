using Materal.Logger.LoggerHandlers.Models;
using Materal.Utils.Http;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// Http日志处理器
    /// </summary>
    public class HttpLoggerHandler : BufferLoggerHandler<HttpLoggerHandlerModel>
    {
        private readonly IHttpHelper _httpHelper;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpHelper"></param>
        public HttpLoggerHandler(IHttpHelper httpHelper) : base()
        {
            _httpHelper = httpHelper;
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
                    string data = $"[{string.Join(",", item.Select(m => m.Data))}]";
                    HttpLoggerHandlerModel model = item.First();
                    _httpHelper.SendAsync(model.Url, model.HttpMethod, null, data).Wait();
                }
                catch (MateralHttpException exception)
                {
                    LoggerLog.LogWarning($"日志Http发送[{item.Key}]失败：{exception.GetExceptionMessage()}");
                }
                catch (Exception exception)
                {
                    LoggerLog.LogWarning($"日志Http发送[{item.Key}]失败：{exception.GetErrorMessage()}");
                }
            });
        }
    }
}
