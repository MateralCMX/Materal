using System;
using System.Runtime.InteropServices;

namespace Materal.WPFCommon
{
    /// <summary>
    /// 浏览器控件帮助类
    /// </summary>
    public class WebBrowserHelper
    {
        public enum ShowCommands
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }
        /// <summary>
        /// 执行Shell
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpOperation"></param>
        /// <param name="lpFile"></param>
        /// <param name="lpParameters"></param>
        /// <param name="lpDirectory"></param>
        /// <param name="nShowCmd"></param>
        /// <returns></returns>
        [DllImport("shell32.dll")]  
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);
        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearCache()
        {
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 255", "", ShowCommands.SW_HIDE);
        }
    }
}
