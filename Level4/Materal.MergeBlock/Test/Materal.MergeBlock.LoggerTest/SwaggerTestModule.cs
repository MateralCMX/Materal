using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly("Logger测试模块", "LoggerTest", ["Logger"])]
namespace Materal.MergeBlock.LoggerTest
{
    /// <summary>
    /// Logger模块
    /// </summary>
    public class LoggerTestModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            if(context.Configuration is IConfigurationBuilder configurationBuilder)
            {
                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LoggerConfig.json");
                configurationBuilder.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
