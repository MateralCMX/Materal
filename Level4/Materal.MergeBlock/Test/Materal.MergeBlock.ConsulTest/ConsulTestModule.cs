using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.ConsulTest
{
    /// <summary>
    /// Consul模块
    /// </summary>
    public class ConsulTestModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsulTestModule() : base("Consul测试模块", "ConsulTest", ["Consul"])
        {

        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            string configFilePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "ConsulConfig.json");
            if (File.Exists(configFilePath))
            {
                context.Configuration.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddConsulConfig("ConsulTest", ["ConsulTest"]);
            await base.OnConfigServiceAsync(context);
        }
    }
}
