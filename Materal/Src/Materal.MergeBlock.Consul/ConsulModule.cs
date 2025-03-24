using Materal.MergeBlock.Abstractions.Extensions;
using Materal.MergeBlock.Consul.Abstractions;
using Materal.Utils.Consul;
using Microsoft.Extensions.Configuration;

namespace Materal.MergeBlock.Consul
{
    /// <summary>
    /// Consul模块
    /// </summary>
    public class ConsulModule() : MergeBlockModule("服务发现模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            IConfigurationSection? section = context.Configuration?.GetSection(MergeBlockConsulOptions.ConfigKey);
            if (section is not null)
            {
                context.Services.Configure<MergeBlockConsulOptions>(section);
            }
            context.Services.AddMateralConsulUtils();
            context.Services.AddMergeBlockHostedService<ConsulModuleServer>();
        }
    }
}
