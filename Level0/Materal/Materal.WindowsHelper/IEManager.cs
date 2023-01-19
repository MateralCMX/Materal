using System;
using System.Runtime.InteropServices;

namespace Materal.WindowsHelper
{
    public static class IEManager
    {
        public enum ShowCommands
        {
            Hide = 0,
            ShowNormal = 1,
            Normal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            Maximize = 3,
            ShowNoactivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinnoactive = 7,
            ShowNa = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimize = 11,
            Max = 11
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
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 255", "", ShowCommands.Hide);
        }
    }
}
