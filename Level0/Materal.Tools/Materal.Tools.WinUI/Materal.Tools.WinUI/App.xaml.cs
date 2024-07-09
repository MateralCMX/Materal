using Materal.Tools.Core.MateralPublish;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using System.Threading.Tasks;

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
        public App()
        {
            InitializeComponent();
            UnhandledException += App_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }
        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            LogError(e.Exception);
        }
        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            LogError(e.Exception);
        }
        private void LogError(Exception ex)
        {
        }
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }
    }
}
