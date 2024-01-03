using Materal.Logger.ConfigModels;
using Materal.MergeBlock.Abstractions.Config;
using Microsoft.Extensions.Logging;

namespace Materal.MergeBlock.Logger
{
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LoggerModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddMateralLogger(config =>
            {
                string applicationName = context.Configuration.GetValue(nameof(MergeBlockConfig.ApplicationName)) ?? "MergeBlockApplication";
                config.AddCustomConfig("ApplicationName", applicationName);
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
        }
    }
}
