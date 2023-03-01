using Consul;
using Materal.Abstractions;
using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Common;
using Materal.BaseCore.WebAPI.Models;
using Materal.Utils.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Text.Json;
using System.Timers;
using Policy = Polly.Policy;
using Timer = System.Timers.Timer;

namespace Materal.BaseCore.WebAPI
{
    /// <summary>
    /// 服务发现
    /// </summary>
    public class ConsulManager
    {
        private static ConsulClient? _consulClient;
        private static AgentServiceRegistration? _registration;
        private static readonly ILogger<ConsulManager>? _logger;
        private static Timer? _healthTimer;
        private static readonly IHttpHelper? _httpHelper;
        /// <summary>
        /// 节点ID
        /// </summary>
        public static Guid NodeID { get; private set; }
        static ConsulManager()
        {
            NodeID = Guid.NewGuid();
            _httpHelper = MateralServices.GetService<IHttpHelper>();
            _logger = MateralServices.GetServiceOrDefatult<ILogger<ConsulManager>>();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="tags"></param>
        public static void Init(params string[] tags)
        {
            string serviceName = $"{WebAPIConfig.AppName}API";
            _consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(WebAPIConfig.ConsulConfig.Url);
            });
            _registration = GetAgentServiceRegistration(serviceName, tags);
            _healthTimer = new Timer(WebAPIConfig.ConsulConfig.HealthInterval * 1000);
            _healthTimer.Elapsed += HealthTimerElapsed;
        }
        /// <summary>
        /// 反注册Consul
        /// </summary>
        public static void UnregisterConsul()
        {
            PolicyBuilder policyBuilder = Policy.Handle<Exception>();
            RetryPolicy retryPolicy = policyBuilder.WaitAndRetryForever(index =>
            {
                _logger?.LogWarning($"Consul服务反注册失败[{index}],{WebAPIConfig.ConsulConfig.ReconnectionInterval}秒后重试");
                return TimeSpan.FromSeconds(WebAPIConfig.ConsulConfig.ReconnectionInterval);
            });
            retryPolicy.Execute(() =>
            {
                _logger?.LogInformation("正在停止Consul健康检查");
                _healthTimer?.Stop();
                _logger?.LogInformation("停止Consul健康检查成功");
                _logger?.LogInformation($"正在反注册Consul[{WebAPIConfig.ConsulConfig.Url}]服务");
                if (_consulClient != null && _registration != null)
                {
                    _consulClient.Agent.ServiceDeregister(_registration.ID).Wait();
                }
                _logger?.LogInformation("Consul服务反注册成功");
            });
        }
        /// <summary>
        /// 注册Consul
        /// </summary>
        public static void RegisterConsul()
        {
            PolicyBuilder policyBuilder = Policy.Handle<Exception>();
            RetryPolicy retryPolicy = policyBuilder.WaitAndRetryForever(index =>
            {
                _logger?.LogWarning($"Consul服务注册失败[{index}],{WebAPIConfig.ConsulConfig.ReconnectionInterval}秒后重试");
                return TimeSpan.FromSeconds(WebAPIConfig.ConsulConfig.ReconnectionInterval);
            });
            retryPolicy.Execute(() =>
            {
                _logger?.LogInformation($"正在注册Consul[{WebAPIConfig.ConsulConfig.Url}]服务");
                if (_consulClient != null && _registration != null)
                {
                    _consulClient.Agent.ServiceRegister(_registration).Wait();
                }
                _logger?.LogInformation("Consul服务注册成功");
                _healthTimer?.Start();
            });
        }

        private static AgentServiceRegistration GetAgentServiceRegistration(string serviceName, params string[] tags)
        {
            List<string> tagsValue = new() { "MateralCore" };
            tagsValue.AddRange(tags);
            string healthUrl = $"{WebAPIConfig.ExternalUrl.Url}/api/Health?id={NodeID}";
            bool isHttps = healthUrl.StartsWith("https");
            _logger?.LogInformation($"健康检查地址:{healthUrl}");
            return new AgentServiceRegistration
            {
                ID = NodeID.ToString(),
                Name = serviceName,
                Address = WebAPIConfig.ExternalUrl.Host,
                Port = WebAPIConfig.ExternalUrl.Port,
                Tags = tagsValue.ToArray(),
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    HTTP = healthUrl,
                    TLSSkipVerify = isHttps,
                    Interval = TimeSpan.FromSeconds(WebAPIConfig.ConsulConfig.HealthInterval),
                    Timeout = TimeSpan.FromSeconds(5),
                }
            };
        }
        /// <summary>
        /// Consul健康检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void HealthTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            _healthTimer?.Stop();
            _logger?.LogDebug("Consul健康检查开始....");
            if (SendHealthRequestAsync().Result)
            {
                _logger?.LogDebug("Consul健康检查成功");
                _healthTimer?.Start();
            }
            else
            {
                _logger?.LogWarning("Consul健康检查失败");
                RegisterConsul();
            }
        }
        /// <summary>
        /// 发送健康检查请求
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SendHealthRequestAsync()
        {
            try
            {
                ConsulServiceModel? service = await GetServiceAsync(m => m.ID != null && m.ID == NodeID.ToString());
                return service != null;
            }
            catch (Exception)
            {
                _logger?.LogWarning("Consul健康检查失败");
                return false;
            }
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<ConsulServiceModel?> GetServiceAsync(Func<ConsulServiceModel, bool>? filter = null)
        {
            List<ConsulServiceModel> consulServices = await GetServicesAsync(filter);
            return consulServices.FirstOrDefault();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<List<ConsulServiceModel>> GetServicesAsync(Func<ConsulServiceModel, bool>? filter = null)
        {
            if (_httpHelper == null) throw new MateralException("获取HttpHelper失败");
            string url = $"{WebAPIConfig.ConsulConfig.Url}/v1/agent/services";
            string requestText = await _httpHelper.SendGetAsync(url);
            JsonDocument jsonDocument = JsonDocument.Parse(requestText);
            if (jsonDocument.RootElement.ValueKind != JsonValueKind.Object) throw new MateralCoreException("ConsulServices返回错误");
            JsonElement.ObjectEnumerator element = jsonDocument.RootElement.EnumerateObject();
            List<JsonProperty> jsonProperties = element.ToList();
            List<string> jsonTexts = jsonProperties.Select(jsonProperty => jsonProperty.Value.ToString()).ToList();
            List<ConsulServiceModel> result = jsonTexts.Select(jsonText => jsonText.JsonToObject<ConsulServiceModel>()).ToList();
            if (filter != null)
            {
                result = result.Where(filter).ToList();
            }
            return result;
        }
    }
}
