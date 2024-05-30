namespace Materal.Utils.Consul.ConfigModels
{
    /// <summary>
    /// 健康检查配置模型
    /// </summary>
    public class HealthConfig
    {
        /// <summary>
        /// 健康检查间隔(秒)
        /// </summary>
        public int Interval { get; set; } = 30;
        /// <summary>
        /// 健康检查地址
        /// </summary>
        public HttpUrlModel Url { get; set; } = new()
        {
            IsSSL = false,
            Host = "localhost",
            Port = 5000,
            Path = "/api/Health"
        };
    }
}
