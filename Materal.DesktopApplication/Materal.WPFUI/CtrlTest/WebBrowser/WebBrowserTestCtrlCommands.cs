using System.Windows.Input;

namespace Materal.WPFUI.CtrlTest.WebBrowser
{
    public class WebBrowserTestCtrlCommands
    {
        /// <summary>
        /// 清理缓存
        /// </summary>
        public static RoutedUICommand CleanUpCache { get; }
        /// <summary>
        /// 跳转页面
        /// </summary>
        public static RoutedUICommand GotoPage { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        static WebBrowserTestCtrlCommands()
        {
            CleanUpCache = new RoutedUICommand(
                "清理缓存",
                nameof(CleanUpCache),
                typeof(MainWindowCommands));
            GotoPage = new RoutedUICommand(
                "跳转页面",
                nameof(GotoPage),
                typeof(MainWindowCommands));
        }
    }
}
