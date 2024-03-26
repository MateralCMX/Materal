using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly(true)]
namespace Materal.MergeBlock.EventBusTest
{
    /// <summary>
    /// EventBus测试模块
    /// </summary>
    public class EventBusTestModule() : MergeBlockWebModule("EventBus测试模块", "EventBusTest", ["EventBus"])
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            string configFilePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "EventBusConfig.json");
            if (File.Exists(configFilePath))
            {
                context.Configuration.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
