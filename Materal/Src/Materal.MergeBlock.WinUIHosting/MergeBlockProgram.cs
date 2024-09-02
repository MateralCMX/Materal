using Materal.Extensions.DependencyInjection;
using Materal.MergeBlock.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Materal.MergeBlock.WebHosting
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
        public static async Task RunAsync<TMainWindow>(LaunchActivatedEventArgs args)
            where TMainWindow : Window, new()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            if (File.Exists("appsettings.json"))
            {
                configurationBuilder.AddJsonFile("appsettings.json");
            }
#if DEBUG
            if (File.Exists("appsettings.Development.json"))
            {
                configurationBuilder.AddJsonFile("appsettings.Development.json");
            }
#endif
            MateralServices.Services = new ServiceCollection();
            IConfigurationRoot configuration = configurationBuilder.Build();
            MateralServices.Services.AddMergeBlockCore(configurationBuilder.Build());
            MergeBlockWinUIApp app = new(new TMainWindow());
            MateralServices.Services.AddSingleton(app);
            MateralServices.ServiceProvider = MateralServices.Services.BuildMateralServiceProvider();
            MateralServices.ServiceProvider.UseMergeBlock(app);
            app.MainWindow.Activate();
        }
    }
}
