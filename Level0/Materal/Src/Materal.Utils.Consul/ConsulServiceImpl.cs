using Consul;
using Materal.Abstractions;
using Materal.Utils.Consul.ConfigModels;
using Materal.Utils.Consul.Models;
using Materal.Utils.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Text.Json;
using Policy = Polly.Policy;

namespace Materal.Utils.Consul
{
    /// <summary>
    /// Consul服务实现
    /// </summary>
    public class ConsulServiceImpl : IConsulService
    {
        private readonly ILogger<ConsulServiceImpl>? _logger;
        private readonly Timer _healthTimer;
        private readonly IHttpHelper _httpHelper;
        /// <summary>
        /// 节点唯一标识
        /// </summary>
        public Guid NodeID { get; } = Guid.NewGuid();
        private ConsulConfigModel? _consulConfig;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsulServiceImpl(IHttpHelper httpHelper, ILogger<ConsulServiceImpl>? logger)
        {
            _httpHelper = httpHelper;
            _logger = logger;
            _healthTimer = new(async _ => await HealthTimerHandlerAsync());
        }
        /// <summary>
        /// 注册Consul服务
        /// </summary>
        /// <param name="consulConfig"></param>
        public async Task RegisterConsulAsync(ConsulConfigModel consulConfig)
        {
            if (_consulConfig is not null) throw new MateralException("Consul服务已注册");
            _consulConfig = consulConfig;
            await RegisterConsulAsync();
        }
        /// <summary>
        /// 反注册Consul服务
        /// </summary>
        /// <returns></returns>
        public async Task UnregisterConsulAsync()
        {
            if (_consulConfig is null) throw new MateralException("未设置Consul配置对象");
            _logger?.LogInformation($"正在向Consul反注册[{_consulConfig.ServiceName}]服务.....");
            using ConsulClient consulClient = new(config => config.Address = new Uri(_consulConfig.ConsulUrl.Url));
            AgentServiceRegistration registration = _consulConfig.GetAgentServiceRegistration(NodeID);
            try
            {
                await consulClient.Agent.ServiceDeregister(registration.ID);
                _logger?.LogInformation($"服务[{_consulConfig.ServiceName}]反注册成功");
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, $"服务[{_consulConfig.ServiceName}]反注册失败");
            }
            finally
            {
                _consulConfig = null;
            }
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<ConsulServiceModel?> GetServiceInfoAsync(Func<ConsulServiceModel, bool>? filter = null)
        {
            List<ConsulServiceModel> consulServices = await GetServiceListAsync(filter);
            return consulServices.FirstOrDefault();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<ConsulServiceModel?> GetServiceInfoAsync(string consulUrl, Func<ConsulServiceModel, bool>? filter = null)
        {
            List<ConsulServiceModel> consulServices = await GetServiceListAsync(consulUrl, filter);
            return consulServices.FirstOrDefault();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<ConsulServiceModel>> GetServiceListAsync(Func<ConsulServiceModel, bool>? filter = null)
        {
            if (_consulConfig is null) throw new MateralException("未设置Consul配置对象");
            return await GetServiceListAsync(_consulConfig.ConsulUrl.Url, filter);
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<ConsulServiceModel>> GetServiceListAsync(string consulUrl, Func<ConsulServiceModel, bool>? filter = null)
        {
            string url = $"{consulUrl}/v1/agent/services";
            string requestText = await _httpHelper.SendGetAsync(url);
            JsonDocument jsonDocument = JsonDocument.Parse(requestText);
            if (jsonDocument.RootElement.ValueKind != JsonValueKind.Object) throw new MateralException("ConsulServices返回错误");
            JsonElement.ObjectEnumerator element = jsonDocument.RootElement.EnumerateObject();
            List<JsonProperty> jsonProperties = [.. element];
            List<string> jsonTexts = jsonProperties.Select(jsonProperty => jsonProperty.Value.ToString()).ToList();
            List<ConsulServiceModel> result = jsonTexts.Select(jsonText => jsonText.JsonToObject<ConsulServiceModel>()).ToList();
            if (filter is not null)
            {
                result = result.Where(filter).ToList();
            }
            return result;
        }
        /// <summary>
        /// 注册Consul服务
        /// </summary>
        private async Task RegisterConsulAsync()
        {
            if (_consulConfig is null) throw new MateralException("未设置Consul配置对象");
            PolicyBuilder policyBuilder = Policy.Handle<Exception>();
            AsyncRetryPolicy retryPolicy = policyBuilder.WaitAndRetryForeverAsync(index =>
            {
                _logger?.LogWarning($"服务[{_consulConfig.ServiceName}]注册失败,{_consulConfig.HealthConfig.ReconnectionInterval}秒后重试");
                return TimeSpan.FromSeconds(_consulConfig.HealthConfig.ReconnectionInterval);
            });
            await retryPolicy.ExecuteAsync(async () =>
            {
                _logger?.LogInformation($"正在向Consul注册[{_consulConfig.ServiceName}]服务.....");
                using ConsulClient consulClient = new(config => config.Address = new Uri(_consulConfig.ConsulUrl.Url));
                AgentServiceRegistration registration = _consulConfig.GetAgentServiceRegistration(NodeID);
                await consulClient.Agent.ServiceRegister(registration);
                _healthTimer.Change(TimeSpan.FromSeconds(_consulConfig.HealthConfig.HealthInterval), Timeout.InfiniteTimeSpan);
                _logger?.LogInformation($"服务[{_consulConfig.ServiceName}]注册成功");
            });
        }
        /// <summary>
        /// 健康检查定时器处理方法
        /// </summary>
        /// <returns></returns>
        private async Task HealthTimerHandlerAsync()
        {
            if (_consulConfig is null) return;
            _logger?.LogDebug("Consul健康检查开始....");
            if (await SendHealthRequestAsync())
            {
                _logger?.LogDebug("Consul健康检查成功");
                _healthTimer.Change(TimeSpan.FromSeconds(_consulConfig.HealthConfig.HealthInterval), Timeout.InfiniteTimeSpan);
            }
            else
            {
                _logger?.LogWarning("Consul健康检查失败");
                await RegisterConsulAsync();
            }
        }
        /// <summary>
        /// 发送健康检查请求
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SendHealthRequestAsync()
        {
            try
            {
                ConsulServiceModel? service = await GetServiceInfoAsync(m => m.ID is not null && m.ID == NodeID.ToString());
                return service is not null;
            }
            catch (Exception)
            {
                _logger?.LogWarning("Consul健康检查失败");
                return false;
            }
        }
    }
}
