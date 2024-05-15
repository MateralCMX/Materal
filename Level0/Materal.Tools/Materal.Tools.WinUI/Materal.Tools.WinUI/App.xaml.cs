using Materal.Logger.Abstractions.Extensions;
using Materal.Logger.ConsoleLogger;
using Materal.Logger.Extensions;
using Materal.Tools.Core;
using Materal.Utils.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace Materal.Tools.WinUI
{
    public partial class App : Application
    {
        private Window? m_window;
        public static IServiceProvider ServiceProvider { get; }
        static App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMateralUtils();
            services.AddMateralLogger(options =>
            {
                options.AddConsoleTarget("ConsoleLogger").AddAllTargetsRule();
            }, true);
            services.AddMateralTools();
            ServiceProvider = services.BuildServiceProvider();
        }
        public App() => InitializeComponent();
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }
    }
}
