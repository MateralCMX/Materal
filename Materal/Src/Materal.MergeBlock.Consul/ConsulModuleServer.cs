using Materal.Extensions;
using Materal.MergeBlock.Consul.Abstractions;
using Materal.Utils.Consul;
using Materal.Utils.Consul.ConfigModels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.Consul
{
    /// <summary>
    /// Consul服务
    /// </summary>
    public class ConsulModuleServer(IConsulService consulService, IEnumerable<ModuleConsulConfig> moduleConsulConfigs, IOptionsMonitor<MergeBlockConsulOptions> mergeBlockConsulConfig) : IHostedService
    {
        private readonly Dictionary<Guid, Guid> _configIDs = [];
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            string configMD5 = mergeBlockConsulConfig.CurrentValue.ToJson().ToMd5_32Encode();
            mergeBlockConsulConfig.OnChange(async config =>
            {
                string newConfigMD5 = config.ToJson().ToMd5_32Encode();
                if (newConfigMD5 != configMD5)
                {
                    await RegisterConsulAsync(config);
                    configMD5 = newConfigMD5;
                }
            });
            await RegisterConsulAsync(mergeBlockConsulConfig.CurrentValue);
            await consulService.RegisterAllConsulAsync();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken) => await consulService.UnregisterAllConsulAsync();
        /// <summary>
        /// 注册Consul
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private async Task RegisterConsulAsync(MergeBlockConsulOptions config)
        {
            foreach (ModuleConsulConfig webModuleConsulConfig in moduleConsulConfigs)
            {
                ConsulConfig consulConfig = GetConsulConfig(config, webModuleConsulConfig);
                if (_configIDs.TryGetValue(webModuleConsulConfig.ID, out Guid id))
                {
                    await consulService.ChangeConsulConfigAsync(id, consulConfig);
                }
                else
                {
                    id = await consulService.RegisterConsulConfigAsync(consulConfig);
                    _configIDs.Add(webModuleConsulConfig.ID, id);
                }
            }
        }
        /// <summary>
        /// 获取Consul配置
        /// </summary>
        /// <param name="mergeBlockConsulConfig"></param>
        /// <param name="webModuleConsulConfig"></param>
        /// <returns></returns>
        private static ConsulConfig GetConsulConfig(MergeBlockConsulOptions mergeBlockConsulConfig, ModuleConsulConfig webModuleConsulConfig)
        {
            ConsulConfig result = new()
            {
                Enable = mergeBlockConsulConfig.Enable,
                ServiceName = $"{webModuleConsulConfig.ServiceName}{mergeBlockConsulConfig.ServiceNameSuffix}",
                Tags = [.. mergeBlockConsulConfig.Tags, .. webModuleConsulConfig.Tags],
                ConsulUrl = mergeBlockConsulConfig.ConsulUrl,
                ServiceUrl = mergeBlockConsulConfig.ServiceUrl,
                Health = new HealthConfig
                {
                    Interval = mergeBlockConsulConfig.HealthInterval,
                    Url = mergeBlockConsulConfig.HealthUrl,
                }
            };
            if (webModuleConsulConfig.HealthPath is not null && !string.IsNullOrWhiteSpace(webModuleConsulConfig.HealthPath))
            {
                result.Health.Url.Path = webModuleConsulConfig.HealthPath;
            }
            return result;
        }
    }
}
