using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.EventBusTest
{
    public class EventBusTestModule : MergeBlockModule
    {
        public EventBusTestModule() : base("EventBus测试模块", "EventBusTest", ["EventBus"])
        {
        }
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
