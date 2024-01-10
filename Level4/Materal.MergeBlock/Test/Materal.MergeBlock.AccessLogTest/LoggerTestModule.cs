using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly("AccessLog测试模块", "AccessLogTest", ["AccessLog"])]
namespace Materal.MergeBlock.AccessLogTest
{
    /// <summary>
    /// AccessLog模块
    /// </summary>
    public class AccessLogTestModule : MergeBlockModule, IMergeBlockModule
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
                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AccessLogConfig.json");
                configurationBuilder.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
