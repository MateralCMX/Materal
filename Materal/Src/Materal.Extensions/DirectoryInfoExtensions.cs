namespace Materal.Extensions
{
    /// <summary>
    /// 文件夹信息扩展
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="directoryInfo">文件夹信息</param>
        /// <param name="targetPath">目标图路径</param>
        /// <param name="overwrite">覆盖标识</param>
        public static void CopyTo(this DirectoryInfo directoryInfo, string targetPath, bool overwrite = true) => CopyTo(directoryInfo, new DirectoryInfo(targetPath), overwrite);
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="directoryInfo">文件夹信息</param>
        /// <param name="targetDirectoryInfo">目标图路径</param>
        /// <param name="overwrite">覆盖标识</param>
        public static void CopyTo(this DirectoryInfo directoryInfo, DirectoryInfo targetDirectoryInfo, bool overwrite = true)
        {
            if (!targetDirectoryInfo.Exists)
            {
                targetDirectoryInfo.Create();
                targetDirectoryInfo.Refresh();
            }
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                fileInfo.CopyTo(Path.Combine(targetDirectoryInfo.FullName, fileInfo.Name), overwrite);
            }
            foreach (DirectoryInfo subDirectoryInfo in directoryInfos)
            {
                subDirectoryInfo.CopyTo(Path.Combine(targetDirectoryInfo.FullName, subDirectoryInfo.Name), overwrite);
            }
        }
        /// <summary>
        /// 清空文件夹
        /// </summary>
        /// <param name="directoryInfo">文件夹信息</param>
        public static void Clear(this DirectoryInfo directoryInfo)
        {
            FileInfo[] allFileInofs = directoryInfo.GetFiles();
            DirectoryInfo[] allDirectoryInfos = directoryInfo.GetDirectories();
            foreach (FileInfo fileInfo in allFileInofs)
            {
                fileInfo.Delete();
            }
            foreach (DirectoryInfo subDirectoryInfo in allDirectoryInfos)
            {
                subDirectoryInfo.Delete(true);
            }
        }
    }
}
