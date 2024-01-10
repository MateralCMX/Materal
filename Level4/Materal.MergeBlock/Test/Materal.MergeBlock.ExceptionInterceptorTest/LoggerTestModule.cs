using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly("ExceptionInterceptor测试模块", "ExceptionInterceptorTest", ["ExceptionInterceptor"])]
namespace Materal.MergeBlock.ExceptionInterceptorTest
{
    /// <summary>
    /// ExceptionInterceptor模块
    /// </summary>
    public class ExceptionInterceptorTestModule : MergeBlockModule, IMergeBlockModule
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
                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExceptionInterceptorConfig.json");
                configurationBuilder.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}
