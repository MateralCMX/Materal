using Consul;
using Materal.APP.Core;
using Materal.APP.Core.Models;
using Materal.APP.WebAPICore.Models;
using Materal.ConvertHelper;
using Materal.NetworkHelper;
using NLog;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using Policy = Polly.Policy;

namespace Materal.APP.WebAPICore
{
    public static class ConsulManage
    {
        private static ConsulClient _consulClient;
        private static AgentServiceRegistration _registration;
        private static Logger _logger;
        private static Timer _healthTimer;
        private static Guid _id;
        public static void Init(ServiceType serviceType, string serviceName = null, params string[] tags)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                serviceName = $"{ApplicationConfig.WebAPIStartupConfig.AppName}API";
            }
            _id = Guid.NewGuid();
            _logger = LogManager.GetCurrentClassLogger();
            _consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(ApplicationConfig.ConsulConfig.Address);
            });
            _registration = GetAgentServiceRegistration(serviceType, serviceName, tags);
            _healthTimer = new Timer(ApplicationConfig.ConsulConfig.HealthInterval * 1000);
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
                _logger.Warn($"Consul服务反注册失败[{index}],{ApplicationConfig.ConsulConfig.ReconnectionInterval}秒后重试");
                return TimeSpan.FromSeconds(ApplicationConfig.ConsulConfig.ReconnectionInterval);
            });
            retryPolicy.Execute(() =>
            {
                _logger.Info("正在停止Consul健康检查");
                _healthTimer.Stop();
                _logger.Info("停止Consul健康检查成功");
                _logger.Info($"正在反注册Consul[{ApplicationConfig.ConsulConfig.Address}]服务");
                _consulClient.Agent.ServiceDeregister(_registration.ID).Wait();
                _logger.Info("Consul服务反注册成功");
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
                _logger.Warn($"Consul服务注册失败[{index}],{ApplicationConfig.ConsulConfig.ReconnectionInterval}秒后重试");
                return TimeSpan.FromSeconds(ApplicationConfig.ConsulConfig.ReconnectionInterval);
            });
            retryPolicy.Execute(() =>
            {
                _logger.Info($"正在注册Consul[{ApplicationConfig.ConsulConfig.Address}]服务");
                 _consulClient.Agent.ServiceRegister(_registration).Wait();
                _logger.Info("Consul服务注册成功");
                _healthTimer.Start();
            });
        }

        private static AgentServiceRegistration GetAgentServiceRegistration(ServiceType serviceType, string serviceName, params string[] tags)
        {
            List<string> tagsValue = new List<string> { "MateralAPP", serviceType.ToString() };
            tagsValue.AddRange(tags);
            return new AgentServiceRegistration
            {
                ID = _id.ToString(),
                Name = serviceName,
                Address = ApplicationConfig.BaseUrlConfig.Host,
                Port = ApplicationConfig.BaseUrlConfig.Port,
                Tags = tagsValue.ToArray(),
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    HTTP = $"{ApplicationConfig.BaseUrlConfig.Url}/api/Health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                }
            };
        }
        /// <summary>
        /// Consul健康检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void HealthTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _healthTimer.Stop();
            _logger.Info("Consul健康检查开始....");
            if (SendHealthRequestAsync().Result)
            {
                _logger.Info("Consul健康检查成功");
                _healthTimer.Start();
            }
            else
            {
                _logger.Info("Consul健康检查失败");
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
                ConsulServiceModel service = await GetServiceAsync(m => m.ID == _id.ToString());
                return service != null;
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return false;
            }
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<ConsulServiceModel> GetServiceAsync(Func<ConsulServiceModel, bool> filter = null)
        {
            List<ConsulServiceModel> consulServices = await GetServicesAsync(filter);
            return consulServices?.FirstOrDefault();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<List<ConsulServiceModel>> GetServicesAsync(Func<ConsulServiceModel, bool> filter = null)
        {
            string url = $"{ApplicationConfig.ConsulConfig.Address}/v1/agent/services";
            string requestText = await HttpManager.SendGetAsync(url);
            JsonDocument jsonDocument = JsonDocument.Parse(requestText);
            if (jsonDocument.RootElement.ValueKind != JsonValueKind.Object) throw new MateralAPPException("ConsulServices返回错误");
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
