using Consul;

namespace Materal.Utils.Consul.ConfigModels
{
    /// <summary>
    /// Consul配置模型
    /// </summary>
    public class ConsulConfig
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; } = "MyService";
        /// <summary>
        /// 标签
        /// </summary>
        public string[] Tags { get; set; } = [];
        /// <summary>
        /// 地址
        /// </summary>
        public HttpUrlModel ConsulUrl { get; set; } = new()
        {
            IsSSL = false,
            Host = "127.0.0.1",
            Port = 8500
        };
        /// <summary>
        /// 服务地址
        /// </summary>
        public HttpUrlModel ServiceUrl { get; set; } = new()
        {
            IsSSL = false,
            Host = "localhost",
            Port = 5000
        };
        /// <summary>
        /// 健康检查
        /// </summary>
        public HealthConfig Health { get; set; } = new();
        /// <summary>
        /// 获取Consul服务注册信息
        /// </summary>
        /// <returns></returns>
        public AgentServiceRegistration GetAgentServiceRegistration(Guid nodeID) => new()
        {
            ID = nodeID.ToString(),
            Name = ServiceName,
            Address = ServiceUrl.Host,
            Port = ServiceUrl.Port,
            Tags = Tags,
            Check = new AgentServiceCheck
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                HTTP = $"{Health.Url.Url}?id={nodeID}",
                TLSSkipVerify = Health.Url.IsSSL,
                Interval = TimeSpan.FromSeconds(Health.Interval),
                Timeout = TimeSpan.FromSeconds(5),
            }
        };
    }
}
