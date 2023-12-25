using HttpMethodEnum = System.Net.Http.HttpMethod;

namespace Materal.Logger.Models
{
    /// <summary>
    /// Http日志目标配置模型
    /// </summary>
    public class HttpLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "Http";
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        /// <paramref name="loggerRuntime"></paramref>
        public override ILoggerHandler GetLoggerHandler(LoggerRuntime loggerRuntime)
        {
            IHttpHelper? httpHelper = MateralServices.GetService<IHttpHelper>();
            return new HttpLoggerHandler(loggerRuntime, httpHelper);
        }
        private string _url = "http://127.0.0.1/api/Logger/WriteLog";
        /// <summary>
        /// 地址
        /// </summary>
        public string Url
        {
            get => _url;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new LoggerException("地址不能为空");
                if (!value.VerifyRegex("(https|http)://[\\S]+", true)) throw new LoggerException("Url格式错误");
                _url = value;
            }
        }
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
                _ => throw new LoggerException("Http方法不正确,支持[POST|PUT]"),
            };
        }
        /// <summary>
        /// 获得HttpMethod
        /// </summary>
        /// <returns></returns>
        public HttpMethodEnum GetHttpMethod() => _httpMethod;
    }
}
