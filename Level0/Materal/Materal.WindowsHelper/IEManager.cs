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
            Normal = 2,
            ShowMinimized = 3,
            ShowMaximized = 4,
            Maximize = 5,
            ShowNoactivate = 6,
            Show = 7,
            Minimize = 8,
            ShowMinnoactive = 9,
            ShowNa = 10,
            Restore = 11,
            ShowDefault = 12,
            ForceMinimize = 13,
            Max = 14
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
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);
        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearCache()
        {
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 255", "", ShowCommands.Hide);
        }
    }
}
