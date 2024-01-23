namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 服务发现配置模型
    /// </summary>
    public class ServiceDiscoveryProviderModel
    {
        /// <summary>
        /// 主机
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Host { get; set; } = "127.0.0.1";
        /// <summary>
        /// 端口号
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public int Port { get; set; } = 8500;
        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Type { get; set; } = "Consul";
        /// <summary>
        /// SSL
        /// </summary>
        public bool IsSSL { get; set; } = false;
    }
}
