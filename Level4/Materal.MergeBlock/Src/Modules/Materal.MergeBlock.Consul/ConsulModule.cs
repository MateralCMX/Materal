using Materal.Utils.Consul;
using Materal.Utils.Consul.ConfigModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.Logger
{
    /// <summary>
    /// Consul模块
    /// </summary>
    public class ConsulModule : MergeBlockModule, IMergeBlockModule
    {
        private const string _configKey="Consul";
        private IConsulService? consulService;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddMateralConsulUtils();
            context.MvcBuilder?.AddApplicationPart(GetType().Assembly);
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            IConfiguration configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            ConsulConfigModel consulConfig = configuration.GetValueObject<ConsulConfigModel>(_configKey) ?? throw new MergeBlockException($"未找到Consul配置[{_configKey}]");
            if (consulConfig.Enable)
            {
                ThreadPool.QueueUserWorkItem(async state =>
                {
                    consulService = MergeBlockManager.ServiceProvider.GetRequiredService<IConsulService>();
                    await consulService.RegisterConsulAsync(consulConfig);
                });
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            }
            await base.OnApplicationInitBeforeAsync(context);
        }
        private void CurrentDomain_ProcessExit(object? sender, EventArgs e) => consulService?.UnregisterConsulAsync().Wait();
    }
}
