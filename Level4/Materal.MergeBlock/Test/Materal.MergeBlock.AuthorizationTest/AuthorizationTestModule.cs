using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly(true)]
namespace Materal.MergeBlock.AuthorizationTest
{
    /// <summary>
    /// Authorization模块
    /// </summary>
    public class AuthorizationTestModule() : MergeBlockWebModule("Authorization测试模块", "AuthorizationTest", ["Authorization"])
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
                string configFilePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "AuthorizationConfig.json");
                if (File.Exists(configFilePath))
                {
                    configurationBuilder.AddJsonFile(configFilePath, true, true);
                }
            }
            await base.OnConfigServiceBeforeAsync(context);
        }
    }
}
