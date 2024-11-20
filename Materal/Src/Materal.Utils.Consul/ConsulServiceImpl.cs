using Materal.Utils.Consul.ConfigModels;
using Materal.Utils.Consul.Models;
using Materal.Utils.Http;
using Microsoft.Extensions.Logging;

namespace Materal.Utils.Consul
{
    /// <summary>
    /// Consul服务实现
    /// </summary>
    public class ConsulServiceImpl(IHttpHelper httpHelper, ILoggerFactory? loggerFactory = null) : IConsulService
    {
        private readonly List<ConsulScope> _consulScopes = [];
        private readonly ILogger? _logger = loggerFactory?.CreateLogger("ConsulUtils");
        /// <summary>
        /// 是否有这个节点
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public bool HasNode(Guid nodeID) => _consulScopes.Any(m => m.NodeID == nodeID && m.Config.Enable);
        /// <summary>
        /// 注册Consul配置
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        public async Task<Guid> RegisterConsulConfigAsync(ConsulConfig consulConfig) => await ChangeConsulConfigAsync(consulConfig.ServiceName, consulConfig);
        /// <summary>
        /// 更改Consul配置
        /// </summary>
        /// <param name="oldConsulConfig"></param>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        public async Task<Guid> ChangeConsulConfigAsync(ConsulConfig oldConsulConfig, ConsulConfig consulConfig) => await ChangeConsulConfigAsync(oldConsulConfig.ServiceName, consulConfig);
        /// <summary>
        /// 更改Consul配置
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        public async Task<Guid> ChangeConsulConfigAsync(string serviceName, ConsulConfig consulConfig)
        {
            ConsulScope? consulScope = _consulScopes.FirstOrDefault(m => m.Config.ServiceName == serviceName);
            return await ChangeConsulConfigAsync(consulScope, consulConfig);
        }
        /// <summary>
        /// 更改Consul配置
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        public async Task<Guid> ChangeConsulConfigAsync(Guid nodeID, ConsulConfig consulConfig)
        {
            ConsulScope? consulScope = _consulScopes.FirstOrDefault(m => m.NodeID == nodeID);
            return await ChangeConsulConfigAsync(consulScope, consulConfig);
        }
        /// <summary>
        /// 更改Consul配置
        /// </summary>
        /// <param name="consulScope"></param>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        public async Task<Guid> ChangeConsulConfigAsync(ConsulScope? consulScope, ConsulConfig consulConfig)
        {
            if (consulScope is null)
            {
                consulScope = new(consulConfig, httpHelper, _logger);
                _consulScopes.Add(consulScope);
            }
            else
            {
                await consulScope.ChangeConfigAsync(consulConfig);
            }
            return consulScope.NodeID;
        }
        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<ConsulServiceModel?> GetServiceInfoAsync(ConsulConfig consulConfig, Func<ConsulServiceModel, bool>? filter = null)
        {
            ConsulScope? consulScope = _consulScopes.FirstOrDefault(m => m.Config.ServiceName == consulConfig.ServiceName);
            if (consulScope is null)
            {
                Guid id = await RegisterConsulConfigAsync(consulConfig);
                return await GetServiceInfoAsync(id, filter);
            }
            return await consulScope.GetServiceInfoAsync(filter);
        }
        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        public async Task<ConsulServiceModel?> GetServiceInfoAsync(Guid nodeID, Func<ConsulServiceModel, bool>? filter = null)
        {
            ConsulScope consulScope = _consulScopes.FirstOrDefault(m => m.NodeID == nodeID) ?? throw new MateralConsulException("未找到Consul配置");
            return await consulScope.GetServiceInfoAsync(filter);
        }
        /// <summary>
        /// 获得服务信息
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        public async Task<ConsulServiceModel?> GetServiceInfoAsync(string consulUrl, Func<ConsulServiceModel, bool>? filter = null) => await ConsulScope.GetServiceInfoAsync(consulUrl, httpHelper, filter);
        /// <summary>
        /// 获得服务信息列表
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<ConsulServiceModel>> GetServiceListAsync(ConsulConfig consulConfig, Func<ConsulServiceModel, bool>? filter = null)
        {
            ConsulScope? consulScope = _consulScopes.FirstOrDefault(m => m.Config.ServiceName == consulConfig.ServiceName);
            if (consulScope is null)
            {
                Guid id = await RegisterConsulConfigAsync(consulConfig);
                return await GetServiceListAsync(id, filter);
            }
            return await consulScope.GetServiceListAsync(filter);
        }
        /// <summary>
        /// 获得服务信息列表
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ConsulServiceModel>> GetServiceListAsync(Guid nodeID, Func<ConsulServiceModel, bool>? filter = null)
        {
            ConsulScope consulScope = _consulScopes.FirstOrDefault(m => m.NodeID == nodeID) ?? throw new MateralConsulException("未找到Consul配置");
            return await consulScope.GetServiceListAsync(filter);
        }
        /// <summary>
        /// 获得服务信息列表
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<ConsulServiceModel>> GetServiceListAsync(string consulUrl, Func<ConsulServiceModel, bool>? filter = null) => await ConsulScope.GetServiceListAsync(consulUrl, httpHelper, filter);
        /// <summary>
        /// 注册Consul
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        public async Task RegisterConsulAsync(ConsulConfig consulConfig)
        {
            ConsulScope? consulScope = _consulScopes.FirstOrDefault(m => m.Config.ServiceName == consulConfig.ServiceName);
            if (consulScope is null)
            {
                Guid id = await RegisterConsulConfigAsync(consulConfig);
                await RegisterConsulAsync(id);
                return;
            }
            await consulScope.RegisterConsulAsync();
        }
        /// <summary>
        /// 注册Consul
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        public async Task RegisterConsulAsync(Guid nodeID)
        {
            ConsulScope consulScope = _consulScopes.FirstOrDefault(m => m.NodeID == nodeID) ?? throw new MateralConsulException("未找到Consul配置");
            await consulScope.RegisterConsulAsync();
        }
        /// <summary>
        /// 注册所有Consul
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        public async Task RegisterAllConsulAsync()
        {
            IEnumerable<ConsulScope> consulScopes = _consulScopes.Where(m => !m.IsRegister);
            foreach (ConsulScope consulScope in consulScopes)
            {
                await consulScope.RegisterConsulAsync();
            }
        }
        /// <summary>
        /// 反注册Consul
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        public async Task UnregisterConsulAsync(ConsulConfig consulConfig)
        {
            ConsulScope? consulScope = _consulScopes.FirstOrDefault(m => m.Config.ServiceName == consulConfig.ServiceName);
            if (consulScope is null)
            {
                Guid id = await RegisterConsulConfigAsync(consulConfig);
                await UnregisterConsulAsync(id);
                return;
            }
            await consulScope.UnregisterConsulAsync();
        }
        /// <summary>
        /// 反注册Consul
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        /// <exception cref="MateralConsulException"></exception>
        public async Task UnregisterConsulAsync(Guid nodeID)
        {
            ConsulScope consulScope = _consulScopes.FirstOrDefault(m => m.NodeID == nodeID) ?? throw new MateralConsulException("未找到Consul配置");
            await consulScope.UnregisterConsulAsync();
        }
        /// <summary>
        /// 反注册所有Consul
        /// </summary>
        /// <returns></returns>
        public async Task UnregisterAllConsulAsync()
        {
            IEnumerable<ConsulScope> consulScopes = _consulScopes.Where(m => m.IsRegister);
            foreach (ConsulScope consulScope in consulScopes)
            {
                await consulScope.UnregisterConsulAsync();
            }
        }
    }
}
