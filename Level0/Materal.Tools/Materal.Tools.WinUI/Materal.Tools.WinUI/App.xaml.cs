using Materal.Tools.Core.MateralPublish;
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
            services.AddSingleton<IMateralPublishService, MateralPublishService>();
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
