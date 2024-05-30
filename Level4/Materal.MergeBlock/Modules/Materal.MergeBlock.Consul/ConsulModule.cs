using Materal.MergeBlock.Abstractions.WebModule.Models;
using Materal.Utils.Consul;

namespace Materal.MergeBlock.Consul
{
    /// <summary>
    /// Consul模块
    /// </summary>
    public class ConsulModule() : MergeBlockWebModule("服务发现模块", "Consul")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IWebConfigServiceContext context)
        {
            context.Services.Configure<MergeBlockConsulConfig>(context.Configuration.GetSection(MergeBlockConsulConfig.ConfigKey));
            context.Services.AddMateralConsulUtils();
            context.Services.AddHostedService<ConsulServer>();
            await base.OnConfigServiceAsync(context);
        }
    }
}
