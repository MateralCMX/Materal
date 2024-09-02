using Materal.Logger.Abstractions.Extensions;
using Materal.Logger.Extensions;
using Microsoft.Extensions.Configuration;

namespace Materal.MergeBlock.Logger
{
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LoggerModule() : MergeBlockModule("日志模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMateralLogger(config =>
            {
                IConfigurationSection? section = context.Configuration?.GetSection("MergeBlock:ApplicationName");
                string applicationName = "MateralMergeBlock应用程序";
                if (section is not null && !string.IsNullOrWhiteSpace(section.Value))
                {
                    applicationName = section.Value;
                }
                config.TryAddCustomData("ApplicationName", applicationName);
            }, true);
            base.OnConfigureServices(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            ILoggerFactory? loggerFactory = context.ServiceProvider.GetService<ILoggerFactory>();
            if (loggerFactory is null) return;
            MateralServices.Logger = loggerFactory.CreateLogger("MergeBlock");
            base.OnApplicationInitialization(context);
        }
    }
}
