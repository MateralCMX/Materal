using Materal.Logger;
using Materal.Logger.ConfigModels;
using Materal.Logger.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.Logger
{
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LoggerModule() : MergeBlockModule("日志模块", "Logger")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            IOptionsMonitor<MergeBlockConfig> mergeBlockConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<MergeBlockConfig>>();
            context.Services.AddMateralLogger(context.Configuration, config =>
            {
                config.AddCustomConfig(nameof(MergeBlockConfig.ApplicationName), mergeBlockConfig.CurrentValue.ApplicationName);
            });
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            await context.ServiceProvider.UseMateralLoggerAsync();
            ILoggerFactory? loggerFactory = context.ServiceProvider.GetService<ILoggerFactory>();
            if (loggerFactory is null) return;
            MergeBlockHost.Logger = loggerFactory.CreateLogger("MergeBlock");
        }
        /// <summary>
        /// 应用程序关闭之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationCloseAfterAsync(IApplicationContext context) => await LoggerHost.ShutdownAsync();
    }
}
