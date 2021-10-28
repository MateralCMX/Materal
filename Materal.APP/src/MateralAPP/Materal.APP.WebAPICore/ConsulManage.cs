using Consul;
using Materal.APP.Core;
using Materal.APP.WebAPICore.Models;
using Materal.ConvertHelper;
using Materal.NetworkHelper;
using MateralAPP.Common.Models;
using Microsoft.Extensions.Hosting;
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
    public class ConsulManage
    {
        private readonly ConsulClient _consulClient;
        private readonly AgentServiceRegistration _registration;
        private readonly Logger _logger;
        private readonly Timer _healthTimer;
        private readonly Guid _id;
        public ConsulManage(ServiceType serviceType)
        {
            _id = Guid.NewGuid();
            _logger = LogManager.GetCurrentClassLogger();
            _consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(ApplicationConfig.ConsulConfig.Address);
            });
            _registration = new AgentServiceRegistration
            {
                ID = _id.ToString(),
                Name = $"{ApplicationConfig.WebAPIStartupConfig.AppName}API",
                Address = ApplicationConfig.BaseUrlConfig.Host,
                Port = ApplicationConfig.BaseUrlConfig.Port,
                Tags = new[] { "MateralAPP", serviceType.ToString() },
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    HTTP = $"{ApplicationConfig.BaseUrlConfig.Url}/api/Health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                }
            };
            _healthTimer = new Timer(ApplicationConfig.ConsulConfig.HealthInterval * 1000);
            _healthTimer.Elapsed += HealthTimerElapsed;
            IHostApplicationLifetime lifetime = ApplicationConfig.GetService<IHostApplicationLifetime>();
            lifetime?.ApplicationStopping.Register(() =>
            {
                _consulClient.Agent.ServiceDeregister(_registration.ID).Wait();
            });
        }
        /// <summary>
        /// 注册Consul
        /// </summary>
        public void RegisterConsul()
        {
            PolicyBuilder policyBuilder = Policy.Handle<Exception>();
            RetryPolicy retryPolicy = policyBuilder.WaitAndRetryForever(count =>
            {
                _logger.Warn($"Consul服务注册失败[{count}],{ApplicationConfig.ConsulConfig.ReconnectionInterval}秒后重试");
                return TimeSpan.FromSeconds(ApplicationConfig.ConsulConfig.ReconnectionInterval);
            });
            retryPolicy.Execute(() =>
            {
                _logger.Info("正在注册Consul服务....");
                _consulClient.Agent.ServiceRegister(_registration).Wait();
                _logger.Info("Consul服务注册成功");
                _healthTimer.Start();
            });
        }
        /// <summary>
        /// Consul健康检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HealthTimerElapsed(object sender, ElapsedEventArgs e)
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
        private async Task<bool> SendHealthRequestAsync()
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
        public async Task<ConsulServiceModel> GetServiceAsync(Func<ConsulServiceModel, bool> filter = null)
        {
            List<ConsulServiceModel> consulServices = await GetServicesAsync(filter);
            return consulServices?.FirstOrDefault();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<ConsulServiceModel>> GetServicesAsync(Func<ConsulServiceModel, bool> filter = null)
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
