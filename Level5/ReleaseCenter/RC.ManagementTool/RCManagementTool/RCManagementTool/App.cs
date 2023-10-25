using Materal.BaseCore.Common;
using Materal.Utils.Http;
using Microsoft.Extensions.Configuration;
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
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            MateralCoreConfig.Configuration = configuration;
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(configuration);
            services.AddSingleton<IHttpHelper, HttpHelper>();
            services.AddSingleton<UserHttpClient>();
            ServiceProvider = services.BuildServiceProvider();
            #region ����HttpClient
            HttpClientHelper.CloseAutoToken();
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
        /// ҳ�浼��ʧ��
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e) => throw new InvalidOperationException($"����ҳ��{e.SourcePageType.FullName}ʧ��: {e.Exception}");
    }
}