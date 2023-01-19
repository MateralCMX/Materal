using System.Diagnostics;

namespace Materal.WindowsHelper
{
    /// <summary>
    /// 资源管理器帮助类
    /// </summary>
    public static class ExplorerManager
    {
        /// <summary>
        /// 打开资源管理器
        /// </summary>
        /// <param name="targetPath">目标文件夹目录</param>
        public static void OpenExplorer(string targetPath)
        {
            var proc = new Process { StartInfo = { FileName = "explorer", Arguments = @"/select," + targetPath } };
            proc.Start();
        }
    }
}
