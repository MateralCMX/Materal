using Microsoft.Extensions.Localization;
using System.Runtime.CompilerServices;

namespace RCManagementTool
{
    public class App : Application
    {
        protected Window? MainWindow { get; private set; }
        public static IServiceProvider ServiceProvider { get; }
        static App()
        {
            IServiceCollection services = new ServiceCollection();
            ServiceProvider = services.BuildServiceProvider();
        }
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
#if NET6_0_OR_GREATER && WINDOWS && !HAS_UNO
		    MainWindow = new Window();
#else
            MainWindow = Window.Current;
#endif
            if (MainWindow.Content is not Frame rootFrame)
            {
                rootFrame = new Frame();
                MainWindow.Content = rootFrame;
                rootFrame.NavigationFailed += OnNavigationFailed;
            }
            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), args.Arguments);
            }
            MainWindow.Activate();
        }
        /// <summary>
        /// “≥√Êµº∫Ω ß∞‹
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e) => throw new InvalidOperationException($"º”‘ÿ“≥√Ê{e.SourcePageType.FullName} ß∞‹: {e.Exception}");
    }
}