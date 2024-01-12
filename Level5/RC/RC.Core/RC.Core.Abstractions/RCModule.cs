using Materal.MergeBlock.Abstractions.ControllerHttpHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RC.Core.Abstractions
{
    /// <summary>
    /// RC模块
    /// </summary>
    public abstract class RCModule() : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            if (context.Configuration is ConfigurationManager configurationManager)
            {
                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RCConfig.json");
                configurationManager.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.TryAddSingleton<IControllerHttpHelper, RCControllerHttpHelper>();
            await base.OnConfigServiceAsync(context);
        }
    }
}
