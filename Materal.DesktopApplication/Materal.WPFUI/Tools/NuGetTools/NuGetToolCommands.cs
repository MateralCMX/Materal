using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Materal.WPFUI.Tools.NuGetTools
{
    public class NuGetToolCommands
    {
        /// <summary>
        /// 浏览项目地址
        /// </summary>
        public static RoutedUICommand BrowseProjectAddress { get; }
        /// <summary>
        /// 浏览目标地址
        /// </summary>
        public static RoutedUICommand BrowseTargetAddress { get; }
        /// <summary>
        /// 导出
        /// </summary>
        public static RoutedUICommand Export { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        static NuGetToolCommands()
        {
            BrowseProjectAddress = new RoutedUICommand(
                "浏览项目地址",
                nameof(BrowseProjectAddress),
                typeof(MainWindowCommands));
            BrowseTargetAddress = new RoutedUICommand(
                "浏览目标地址",
                nameof(BrowseTargetAddress),
                typeof(MainWindowCommands));
            Export = new RoutedUICommand(
                "导出",
                nameof(Export),
                typeof(MainWindowCommands));
        }
    }
}
