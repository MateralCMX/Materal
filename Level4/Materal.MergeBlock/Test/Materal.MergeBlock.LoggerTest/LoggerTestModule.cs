using Materal.Extensions;
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
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            if (context.Configuration is IConfigurationBuilder configurationBuilder)
            {
                string configFilePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "LoggerConfig.json");
                if (File.Exists(configFilePath))
                {
                    configurationBuilder.AddJsonFile(configFilePath, true, true);
                }
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
