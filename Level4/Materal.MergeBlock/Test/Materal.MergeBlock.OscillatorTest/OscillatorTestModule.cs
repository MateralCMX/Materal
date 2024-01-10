using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly("Oscillator测试模块", "OscillatorTest", ["Oscillator"])]
namespace Materal.MergeBlock.OscillatorTest
{
    /// <summary>
    /// Oscillator模块
    /// </summary>
    public class OscillatorTestModule : MergeBlockModule, IMergeBlockModule
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
                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OscillatorConfig.json");
                configurationBuilder.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
