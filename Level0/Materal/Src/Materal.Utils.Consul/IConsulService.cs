using Materal.Utils.Consul.ConfigModels;

namespace Materal.Utils.Consul
{
    /// <summary>
    /// Consul服务
    /// </summary>
    public interface IConsulService
    {
        /// <summary>
        /// 节点唯一标识
        /// </summary>
        Guid NodeID { get; }
        /// <summary>
        /// 注册Consul
        /// </summary>
        Task RegisterConsulAsync(ConsulConfigModel consulConfig);
        /// <summary>
        /// 反注册Consul
        /// </summary>
        Task UnregisterConsulAsync();
    }
}
