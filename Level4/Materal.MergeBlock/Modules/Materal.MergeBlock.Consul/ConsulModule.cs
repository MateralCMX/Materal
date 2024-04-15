using Materal.Extensions;
using Materal.MergeBlock.Abstractions.WebModule.Models;
using Materal.Utils.Consul;
using Materal.Utils.Consul.ConfigModels;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.Consul
{
    /// <summary>
    /// Consul模块
    /// </summary>
    public class ConsulModule() : MergeBlockWebModule("服务发现模块", "Consul")
    {
        private readonly Dictionary<Guid, Guid> _configIDs = [];
        private IConsulService? _consulService;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IWebConfigServiceContext context)
        {
            context.Services.Configure<MergeBlockConsulConfig>(context.Configuration.GetSection(MergeBlockConsulConfig.ConfigKey));
            context.Services.AddMateralConsulUtils();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IWebApplicationContext context)
        {
            _consulService ??= context.ServiceProvider.GetRequiredService<IConsulService>();
            IOptionsMonitor<MergeBlockConsulConfig> mergeBlockConsulConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<MergeBlockConsulConfig>>();
            string configMD5 = mergeBlockConsulConfig.CurrentValue.ToJson().ToMd5_32Encode();
            mergeBlockConsulConfig.OnChange(async config =>
            {
                string newConfigMD5 = config.ToJson().ToMd5_32Encode();
                if (newConfigMD5 != configMD5)
                {
                    await RegisterConsulAsync(config, context.ServiceProvider);
                    configMD5 = newConfigMD5;
                }
            });
            await RegisterConsulAsync(mergeBlockConsulConfig.CurrentValue, context.ServiceProvider);
            await _consulService.RegisterAllConsulAsync();
            await base.OnApplicationInitAfterAsync(context);
        }
        /// <summary>
        /// 应用关闭
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationCloseAsync(IWebApplicationContext context)
        {
            _consulService ??= context.ServiceProvider.GetRequiredService<IConsulService>();
            await _consulService.UnregisterAllConsulAsync();
        }
        /// <summary>
        /// 注册Consul
        /// </summary>
        /// <param name="config"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private async Task RegisterConsulAsync(MergeBlockConsulConfig config, IServiceProvider serviceProvider)
        {
            _consulService ??= serviceProvider.GetRequiredService<IConsulService>();
            IEnumerable<WebModuleConsulConfig> webModuleConsulConfigs = serviceProvider.GetServices<WebModuleConsulConfig>();
            foreach (WebModuleConsulConfig webModuleConsulConfig in webModuleConsulConfigs)
            {
                ConsulConfig consulConfig = GetConsulConfig(config, webModuleConsulConfig);
                if (_configIDs.TryGetValue(webModuleConsulConfig.ID, out Guid id))
                {
                    await _consulService.ChangeConsulConfigAsync(id, consulConfig);
                }
                else
                {
                    id = await _consulService.RegisterConsulConfigAsync(consulConfig);
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
        private static ConsulConfig GetConsulConfig(MergeBlockConsulConfig mergeBlockConsulConfig, WebModuleConsulConfig webModuleConsulConfig)
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
