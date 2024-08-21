using HttpMethodEnum = System.Net.Http.HttpMethod;

namespace Materal.Logger.HttpLogger
{
    /// <summary>
    /// Http日志目标配置
    /// </summary>
    public class HttpLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; } = "http://127.0.0.1/api/Logger/WriteLog";
        /// <summary>
        /// Http方法类型
        /// </summary>
        private HttpMethodEnum _httpMethod = HttpMethodEnum.Post;
        /// <summary>
        /// Http方法
        /// </summary>
        public string HttpMethod
        {
            get => _httpMethod.Method;
            set => _httpMethod = value.ToUpper() switch
            {
                "POST" => HttpMethodEnum.Post,
                "PUT" => HttpMethodEnum.Put,
                _ => throw new LoggerException("Http方法不正确,仅支持[POST|PUT]"),
            };
        }
        /// <summary>
        /// 获得HttpMethod
        /// </summary>
        /// <returns></returns>
        public HttpMethodEnum GetHttpMethod() => _httpMethod;
    }
}
