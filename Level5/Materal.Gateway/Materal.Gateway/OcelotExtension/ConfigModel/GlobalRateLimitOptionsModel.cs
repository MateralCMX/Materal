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
        [Required(ErrorMessage = "必填")]
        public string QuotaExceededMessage { get; set; } = "访问过于频繁，请稍后再试";
        /// <summary>
        /// 限流状态码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.TooManyRequests;
        /// <summary>
        /// 客户端ID头
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string ClientIdHeader { get; set; } = "ClientId";
    }
}
