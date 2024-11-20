using Consul;
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
    /// Consul作用域
    /// </summary>
    public class ConsulScope
    {
        private readonly IHttpHelper _httpHelper;
        private readonly ILogger? _logger;
        private readonly Timer _healthTimer;
        /// <summary>
        /// 是否注册
        /// </summary>
        public bool IsRegister { get; private set; } = false;
        /// <summary>
        /// 配置
        /// </summary>
        public ConsulConfig Config { get; private set; }
        /// <summary>
        /// 节点唯一标识
        /// </summary>
        public Guid NodeID { get; } = Guid.NewGuid();
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsulScope(ConsulConfig consulConfig, IHttpHelper httpHelper, ILogger? logger = null)
        {
            Config = consulConfig;
            _httpHelper = httpHelper;
            _logger = logger;
            _healthTimer = new(async _ => await HealthTimerHandlerAsync());
        }
        /// <summary>
        /// 改变配置
        /// </summary>
        /// <param name="consulConfig"></param>
        /// <returns></returns>
        public async Task ChangeConfigAsync(ConsulConfig consulConfig)
        {
            _healthTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            if (IsRegister)
            {
                await UnregisterConsulAsync();
            }
            Config = consulConfig;
            await RegisterConsulAsync();
        }
        /// <summary>
        /// 注册Consul服务
        /// </summary>
        public async Task RegisterConsulAsync()
        {
            if (!Config.Enable) return;
            if (IsRegister) return;
            IsRegister = true;
            PolicyBuilder policyBuilder = Policy.Handle<Exception>(ex =>
            {
                _logger?.LogWarning(ex, $"服务[{Config.ServiceName}]注册失败,{Config.Health.Interval}秒后重试");
                return true;
            });
            AsyncRetryPolicy retryPolicy = policyBuilder.WaitAndRetryForeverAsync(index => TimeSpan.FromSeconds(Config.Health.Interval));
            await retryPolicy.ExecuteAsync(async () =>
            {
                _logger?.LogInformation($"正在向Consul注册[{Config.ServiceName}]服务.....");
                using ConsulClient consulClient = new(config => config.Address = new Uri(Config.ConsulUrl.Url));
                AgentServiceRegistration registration = Config.GetAgentServiceRegistration(NodeID);
                await consulClient.Agent.ServiceRegister(registration);
                _healthTimer.Change(TimeSpan.FromSeconds(Config.Health.Interval), Timeout.InfiniteTimeSpan);
                _logger?.LogInformation($"服务[{Config.ServiceName}]注册成功\r\n健康检查地址为:{registration.Check.HTTP}");
            });
        }
        /// <summary>
        /// 健康检查定时器处理方法
        /// </summary>
        /// <returns></returns>
        private async Task HealthTimerHandlerAsync()
        {
            if (!Config.Enable) return;
            if (Config is null) return;
            _logger?.LogDebug("Consul健康检查开始....");
            if (await SendHealthRequestAsync())
            {
                _logger?.LogDebug("Consul健康检查成功");
                _healthTimer.Change(TimeSpan.FromSeconds(Config.Health.Interval), Timeout.InfiniteTimeSpan);
            }
            else
            {
                _logger?.LogWarning("Consul健康检查失败,正在重新注册...");
                IsRegister = false;
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
                if (!Config.Enable) return true;
                ConsulServiceModel? service = await GetServiceInfoAsync(m => m.ID is not null && m.ID == NodeID.ToString());
                return service is not null;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "发送健康检查请求失败");
                return false;
            }
        }
        /// <summary>
        /// 反注册Consul服务
        /// </summary>
        /// <returns></returns>
        public async Task UnregisterConsulAsync()
        {
            if (!Config.Enable) return;
            if (!IsRegister) return;
            IsRegister = false;
            _logger?.LogInformation($"正在向Consul反注册[{Config.ServiceName}]服务.....");
            using ConsulClient consulClient = new(config => config.Address = new Uri(Config.ConsulUrl.Url));
            AgentServiceRegistration registration = Config.GetAgentServiceRegistration(NodeID);
            try
            {
                await consulClient.Agent.ServiceDeregister(registration.ID);
                _logger?.LogInformation($"服务[{Config.ServiceName}]反注册成功");
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, $"服务[{Config.ServiceName}]反注册失败");
                IsRegister = true;
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
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<ConsulServiceModel>> GetServiceListAsync(Func<ConsulServiceModel, bool>? filter = null)
            => await GetServiceListAsync(Config.ConsulUrl.Url, _httpHelper, filter);
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="httpHelper"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<List<ConsulServiceModel>> GetServiceListAsync(string consulUrl, IHttpHelper httpHelper, Func<ConsulServiceModel, bool>? filter = null)
        {
            string url = $"{consulUrl}/v1/agent/services";
            string requestText = await httpHelper.SendGetAsync(url);
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
        /// 获得服务
        /// </summary>
        /// <param name="consulUrl"></param>
        /// <param name="httpHelper"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<ConsulServiceModel?> GetServiceInfoAsync(string consulUrl, IHttpHelper httpHelper, Func<ConsulServiceModel, bool>? filter = null)
        {
            List<ConsulServiceModel> consulServices = await GetServiceListAsync(consulUrl, httpHelper, filter);
            return consulServices.FirstOrDefault();
        }
    }
}
