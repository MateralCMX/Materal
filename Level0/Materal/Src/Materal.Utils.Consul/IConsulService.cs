using Materal.Utils.Consul.ConfigModels;
using Materal.Utils.Consul.Models;

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
        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<ConsulServiceModel?> GetServiceInfoAsync(Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 获得服务信息列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<ConsulServiceModel>> GetServiceListAsync(Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<ConsulServiceModel?> GetServiceInfoAsync(string consulUrl, Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 获得服务信息列表
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<ConsulServiceModel>> GetServiceListAsync(string consulUrl, Func<ConsulServiceModel, bool>? filter = null);
    }
}
