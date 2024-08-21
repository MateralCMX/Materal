namespace Materal.Utils.Windows
{
    /// <summary>
    /// 资源管理器帮助类
    /// </summary>
    public static class ExplorerHelper
    {
        /// <summary>
        /// 打开资源管理器
        /// </summary>
        /// <param name="targetPath">目标文件夹目录</param>
        public static Process OpenExplorer(string targetPath)
        {
            Process result = new() { StartInfo = { FileName = "explorer", Arguments = @"/select," + targetPath } };
            result.Start();
            return result;
        }
    }
}
