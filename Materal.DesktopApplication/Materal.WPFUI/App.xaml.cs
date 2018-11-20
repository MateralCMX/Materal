using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace Materal.WPFUI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var setting = new CefSettings
            {
                Locale = "zh-CN",
                CachePath = "/BrowserCache",
                AcceptLanguageList = "zh-CN,zh;q=0.8",
                LocalesDirPath = "/localeDir",
                LogFile = "/LogData",
                PersistSessionCookies = true,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36",
                UserDataPath = "/userData"
            };
            Cef.Initialize(setting);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Cef.Shutdown();
        }
    }
}
