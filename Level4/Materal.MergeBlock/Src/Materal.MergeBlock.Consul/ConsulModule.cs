using Materal.Utils.Consul;
using Materal.Utils.Consul.ConfigModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.Logger
{
    public class ConsulModule : MergeBlockModule, IMergeBlockModule
    {
        private const string _configKey="Consul";
        private IConsulService? consulService;
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddMateralConsulUtils();
            context.MvcBuilder?.AddApplicationPart(GetType().Assembly);
            await base.OnConfigServiceAsync(context);
        }
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
