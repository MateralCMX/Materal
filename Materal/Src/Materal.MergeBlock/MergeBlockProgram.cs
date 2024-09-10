using Materal.MergeBlock.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task RunAsync(string[] args, CancellationToken? cancellationToken = null)
        {
            DirectoryInfo directoryInfo = new(AppContext.BaseDirectory);
            if (!directoryInfo.Exists) return;
            FileInfo[] fileInfos = directoryInfo.GetFiles("Materal.MergeBlock.Web.dll");
            if (fileInfos.Length > 0)
            {
                await WebRunAsync(args, cancellationToken);
            }
            else
            {
                await ConsoleRunAsync(args, cancellationToken);
            }
        }
        private static async Task ConsoleRunAsync(string[] args, CancellationToken? cancellationToken = null)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.AddMateralServiceProvider();
            builder.Services.AddSingleton(builder);
#if DEBUG
            if (File.Exists("appsettings.Development.json"))
            {
                builder.Configuration.AddJsonFile("appsettings.Development.json", true, true);
            }
#endif
            builder.AddMergeBlockCore();
            OnConfigureServices?.Invoke(builder.Services);
            IHost app = builder.Build();
            app.UseMateralServiceProvider();
            app.UseMergeBlock();
            OnApplicationInitialization?.Invoke(app.Services);
            if (cancellationToken is null)
            {
                await app.RunAsync();
            }
            else
            {
                await app.RunAsync(cancellationToken.Value);
            }
        }
        private static async Task WebRunAsync(string[] args, CancellationToken? cancellationToken = null)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddMateralServiceProvider();
            builder.Services.AddSingleton(builder);
            builder.AddMergeBlockCore();
            OnConfigureServices?.Invoke(builder.Services);
            WebApplication app = builder.Build();
            app.UseMateralServiceProvider();
            app.UseMergeBlock();
            OnApplicationInitialization?.Invoke(app.Services);
            if (cancellationToken is null)
            {
                await app.RunAsync();
            }
            else
            {
                await app.RunAsync(cancellationToken.Value);
            }
        }
    }
}
