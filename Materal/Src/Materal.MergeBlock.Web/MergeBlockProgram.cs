using Materal.Extensions.DependencyInjection;
using Materal.Extensions.DependencyInjection.AspNetCore;
using Materal.MergeBlock.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.Web
{
    /// <summary>
    /// MergeBlock程序
    /// </summary>
    public static class MergeBlockProgram
    {
        /// <summary>
        /// 配置
        /// </summary>
        public static event Action<IServiceCollection>? OnConfigureServices;
        /// <summary>
        /// 应用初始化
        /// </summary>
        public static event Action<IServiceProvider>? OnApplicationInitialization;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task RunAsync(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddMateralServiceProvider();
            builder.Services.AddSingleton(builder);
            builder.AddMergeBlockCore();
            OnConfigureServices?.Invoke(builder.Services);
            WebApplication app = builder.Build();
            app.UseMergeBlock();
            OnApplicationInitialization?.Invoke(app.Services);
            await app.RunAsync();
        }
    }
}
