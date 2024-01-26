using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.Logger
{
    /// <summary>
    /// Consul模块
    /// </summary>
    public class ConsulModule : MergeBlockModule, IMergeBlockModule
    {
        private const string _configKey="Consul";
        private IConsulService? _consulService;
        private IOptionsMonitor<ConsulConfigModel>? _config;
        private bool isRegister = false;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ConsulConfigModel>(context.Configuration.GetSection(_configKey));
            context.Services.AddMateralConsulUtils();
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
            _config = context.ServiceProvider.GetRequiredService<IOptionsMonitor<ConsulConfigModel>>();
            _consulService = context.ServiceProvider.GetRequiredService<IConsulService>();
            _config.OnChange(config =>
            {
                if (config.Enable)
                {
                    RegisterConsul();
                }
                else
                {
                    UnregisterConsul();
                }
            });
            if (_config.CurrentValue.Enable)
            {
                RegisterConsul();
            }
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            await base.OnApplicationInitBeforeAsync(context);
        }
        private void RegisterConsul()
        {
            if(isRegister) return;
            isRegister = true;
            ThreadPool.QueueUserWorkItem(async state =>
            {
                if (_config is null || _consulService is null) return;
                await _consulService.RegisterConsulAsync(_config.CurrentValue);
            });
        }
        private void UnregisterConsul()
        {
            if (!isRegister || _consulService is null) return;
            _consulService.UnregisterConsulAsync().Wait();
            isRegister = false;
        }
        private void CurrentDomain_ProcessExit(object? sender, EventArgs e) => UnregisterConsul();
    }
}
