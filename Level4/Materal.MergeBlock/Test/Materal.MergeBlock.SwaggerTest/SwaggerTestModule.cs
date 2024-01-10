using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly("Swagger测试模块", "SwaggerTest", ["Swagger"])]
namespace Materal.MergeBlock.SwaggerTest
{
    /// <summary>
    /// Swagger模块
    /// </summary>
    public class SwaggerTestModule : MergeBlockModule, IMergeBlockModule
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
                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SwaggerConfig.json");
                configurationBuilder.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
