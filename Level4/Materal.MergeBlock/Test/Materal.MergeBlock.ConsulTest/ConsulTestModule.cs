using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly("Consul测试模块", "ConsulTest", ["Consul"])]
namespace Materal.MergeBlock.ConsulTest
{
    /// <summary>
    /// Consul模块
    /// </summary>
    public class ConsulTestModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            if(context.Configuration is IConfigurationBuilder configurationBuilder)
            {
                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConsulConfig.json");
                configurationBuilder.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
