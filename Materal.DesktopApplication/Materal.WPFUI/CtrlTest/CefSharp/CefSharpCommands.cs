using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Materal.WPFUI.CtrlTest.CefSharp
{
    public class CefSharpCommands
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
        /// 清理缓存
        /// </summary>
        public static RoutedUICommand DevTool { get; }
        /// <summary>
        /// 调试工具
        /// </summary>
        static CefSharpCommands()
        {
            CleanUpCache = new RoutedUICommand(
                "清理缓存",
                nameof(CleanUpCache),
                typeof(CefSharpCommands));
            GotoPage = new RoutedUICommand(
                "跳转页面",
                nameof(GotoPage),
                typeof(CefSharpCommands));
            DevTool = new RoutedUICommand(
                "调试工具",
                nameof(DevTool),
                typeof(CefSharpCommands));
        }
    }
}
