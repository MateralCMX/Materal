using CefSharp;
using CefSharp.Wpf;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Materal.WPFUI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += Resolver;
        }
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            //if (!args.Name.StartsWith("CefSharp")) return null;
            //string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
            //string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
            //    Environment.Is64BitProcess ? "x64" : "x86",
            //    assemblyName);
            //return File.Exists(archSpecificPath)
            //    ? Assembly.LoadFile(archSpecificPath)
            //    : null;
            return null;

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            //var setting = new CefSettings
            //{
            //    Locale = "zh-CN",
            //    CachePath = "/BrowserCache",
            //    AcceptLanguageList = "zh-CN,zh;q=0.8",
            //    LocalesDirPath = "/localeDir",
            //    LogFile = "/LogData",
            //    PersistSessionCookies = true,
            //    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36",
            //    UserDataPath = "/userData",
            //    BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
            //        Environment.Is64BitProcess ? "x64" : "x86",
            //        "CefSharp.BrowserSubprocess.exe")
            //};
            //Cef.Initialize(setting);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            //base.OnExit(e);
            //Cef.Shutdown();
        }
    }
}
