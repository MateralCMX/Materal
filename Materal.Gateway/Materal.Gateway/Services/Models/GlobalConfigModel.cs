namespace Materal.Gateway.Services.Models
{
    /// <summary>
    /// 全局配置模型
    /// </summary>
    public class GlobalConfigFileModel
    {
        /// <summary>
        /// BaseUrl
        /// </summary>
        public string BaseUrl { get; set; } = ApplicationData.Host;
        /// <summary>
        /// 
        /// </summary>
        public ServiceDiscovery ServiceDiscoveryProvider { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ServiceDiscovery
    {
        /// <summary>
        /// 
        /// </summary>
        public string Host { get; set; } = "127.0.0.1";
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; } = 8500;
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = "Consul";
    }
}
