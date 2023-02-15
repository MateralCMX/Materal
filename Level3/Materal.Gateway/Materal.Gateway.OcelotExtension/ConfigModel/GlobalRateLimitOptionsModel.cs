using System.Net;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 全局限流配置
    /// </summary>
    public class GlobalRateLimitOptionsModel
    {
        /// <summary>
        /// 限流消息
        /// </summary>
        public string QuotaExceededMessage { get; set; } = "访问过于频繁，请稍后再试";
        /// <summary>
        /// 限流状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.TooManyRequests;
        /// <summary>
        /// 客户端ID头
        /// </summary>
        public string ClientIdHeader { get; set; } = "ClientId";
    }
}
