using Materal.MergeBlock.Abstractions.Config;

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
            context.Services.AddMateralLogger(options =>
            {
                string applicationName = context.Configuration.GetValue(nameof(MergeBlockConfig.ApplicationName)) ?? "MergeBlockApplication";
                options.AddCustomConfig("ApplicationName", applicationName);
            });
            await base.OnConfigServiceAsync(context);
        }
    }
}
