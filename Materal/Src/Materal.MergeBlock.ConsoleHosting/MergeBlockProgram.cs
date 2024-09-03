using Materal.Extensions.DependencyInjection;
using Materal.MergeBlock.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.ConsoleHosting
{
    /// <summary>
    /// MergeBlock程序
    /// </summary>
    public static class MergeBlockProgram
    {
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task RunAsync(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.UseMateralServiceProvider();
            builder.Services.AddSingleton(builder);
            builder.Services.AddSingleton<IHostApplicationBuilder>(builder);
#if DEBUG
            if (File.Exists("appsettings.Development.json"))
            {
                builder.Configuration.AddJsonFile("appsettings.Development.json", true, true);
            }
#endif
            builder.AddMergeBlockCore();
            IHost app = builder.Build();
            app.UseMergeBlock();
            await app.RunAsync();
        }
    }
}
