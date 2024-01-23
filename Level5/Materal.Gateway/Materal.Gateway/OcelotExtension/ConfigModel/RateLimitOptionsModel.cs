namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 限流配置模型
    /// </summary>
    public class RateLimitOptionsModel
    {
        /// <summary>
        /// 是否启用限流
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public bool EnableRateLimiting { get; set; } = true;
        /// <summary>
        /// 统计时间(1s 1m 1h 1d)
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Period { get; set; } = "10s";
        /// <summary>
        /// 统计时间内允许请求的最大次数
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public int Limit { get; set; } = 5;
        /// <summary>
        /// 限流后重试时间(s)
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public int PeriodTimespan { get; set; } = 10;
        /// <summary>
        /// 白名单
        /// HttpHeaders中加入ClientId:MyClient
        /// </summary>
        public List<string> ClientWhitelist { get; set; } = [];
    }
}
