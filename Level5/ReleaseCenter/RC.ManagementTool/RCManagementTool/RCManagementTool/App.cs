using Materal.Utils.Http;
using RC.Authority.HttpClient;
using RC.Core.HttpClient;
using RCManagementTool.Manager;

namespace RCManagementTool
{
    public class App : Application
    {
        protected Window? MainWindow { get; private set; }
        public static IServiceProvider ServiceProvider { get; }
        static App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IHttpHelper, HttpHelper>();
            services.AddSingleton<UserHttpClient>();
            ServiceProvider = services.BuildServiceProvider();
            #region …Ë÷√HttpClient
            HttpClientHelper.CloseAutoToken();
            HttpClientHelper.GetUrl = (url, appName) => $"http://175.27.254.187:8700/RC{appName}API/api/{url}";
            HttpClientHelper.GetToken = () => AuthorityManager.Token;
            HttpClientHelper.GetTokenInterval = () => AuthorityManager.Interval;
            #endregion
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