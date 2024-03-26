using Materal.Extensions;
using Materal.Logger.ConfigModels;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: MergeBlockAssembly(true)]
namespace Materal.MergeBlock.LoggerTest
{
    /// <summary>
    /// Logger模块
    /// </summary>
    public class LoggerTestModule() : MergeBlockWebModule("Logger测试模块", "LoggerTest", ["Logger"])
    {
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            string configFilePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "LoggerConfig.json");
            if (File.Exists(configFilePath))
            {
                context.Configuration.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
