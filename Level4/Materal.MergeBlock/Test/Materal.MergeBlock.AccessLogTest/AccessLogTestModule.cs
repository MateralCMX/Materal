using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.AccessLogTest
{
    /// <summary>
    /// AccessLog模块
    /// </summary>
    public class AccessLogTestModule : MergeBlockModule, IMergeBlockModule
    {
        public AccessLogTestModule() : base("AccessLog测试模块", "AccessLogTest", ["AccessLog"])
        {

        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            string configFilePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "AccessLogConfig.json");
            if (File.Exists(configFilePath))
            {
                context.Configuration.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
