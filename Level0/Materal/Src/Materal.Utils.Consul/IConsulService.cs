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
        /// 有这个节点
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        bool HasNode(Guid nodeID);
        /// <summary>
        /// 添加Consul配置
        /// </summary>
        /// <param name="consulConfig"></param>
        Task<Guid> RegisterConsulConfigAsync(ConsulConfig consulConfig);
        /// <summary>
        /// 更改Consul配置
        /// </summary>
        /// <param name="oldConsulConfig"></param>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        Task<Guid> ChangeConsulConfigAsync(ConsulConfig oldConsulConfig, ConsulConfig consulConfig);
        /// <summary>
        /// 更改Consul配置
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        Task<Guid> ChangeConsulConfigAsync(string serviceName, ConsulConfig consulConfig);
        /// <summary>
        /// 更改Consul配置
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        Task<Guid> ChangeConsulConfigAsync(Guid nodeID, ConsulConfig consulConfig);
        /// <summary>
        /// 更改Consul配置
        /// </summary>
        /// <param name="consulScope"></param>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        Task<Guid> ChangeConsulConfigAsync(ConsulScope? consulScope, ConsulConfig consulConfig);
        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<ConsulServiceModel?> GetServiceInfoAsync(ConsulConfig consulConfig, Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        Task<ConsulServiceModel?> GetServiceInfoAsync(Guid nodeID, Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        Task<ConsulServiceModel?> GetServiceInfoAsync(string consulUrl, Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 获得服务信息列表
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<List<ConsulServiceModel>> GetServiceListAsync(ConsulConfig consulConfig, Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 获得服务信息列表
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<List<ConsulServiceModel>> GetServiceListAsync(Guid nodeID, Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 获得服务信息列表
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<ConsulServiceModel>> GetServiceListAsync(string consulUrl, Func<ConsulServiceModel, bool>? filter = null);
        /// <summary>
        /// 注册Consul
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        Task RegisterConsulAsync(ConsulConfig consulConfig);
        /// <summary>
        /// 注册Consul
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        Task RegisterConsulAsync(Guid nodeID);
        /// <summary>
        /// 注册所有Consul
        /// </summary>
        /// <returns></returns>
        Task RegisterAllConsulAsync();
        /// <summary>
        /// 反注册Consul
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        Task UnregisterConsulAsync(ConsulConfig consulConfig);
        /// <summary>
        /// 反注册Consul
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        Task UnregisterConsulAsync(Guid nodeID);
        /// <summary>
        /// 反注册所有Consul
        /// </summary>
        /// <returns></returns>
        Task UnregisterAllConsulAsync();
    }
}
