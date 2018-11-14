using System.IO;

namespace Materal.FileHelper
{
    /// <summary>
    /// 文件夹信息扩展
    /// </summary>
    public static class DirectoryInfoExtension
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="obj">文件夹信息</param>
        /// <param name="targetPath">目标图路径</param>
        /// <param name="overwrite">覆盖标识</param>
        public static void Copy(this DirectoryInfo obj, string targetPath, bool overwrite = true)
        {
            string[] sourceFilesPath = Directory.GetFileSystemEntries(obj.FullName);
            foreach (string sourceFilePath in sourceFilesPath)
            {
                string directoryName = Path.GetDirectoryName(sourceFilePath);
                if (directoryName == null) continue;
                string[] forlders = directoryName.Split('\\');
                string lastDirectory = forlders[forlders.Length - 1];
                string dest = Path.Combine(targetPath, lastDirectory);
                if (File.Exists(sourceFilePath))
                {
                    if (!Directory.Exists(dest))
                    {
                        Directory.CreateDirectory(dest);
                    }
                    string sourceFileName = Path.GetFileName(sourceFilePath);
                    File.Copy(sourceFilePath, Path.Combine(dest, sourceFileName), overwrite);
                }
                else
                {
                    new DirectoryInfo(sourceFilePath).Copy(dest, overwrite);
                }
            }
        }
        /// <summary>
        /// 清空文件夹
        /// </summary>
        /// <param name="obj">文件夹信息</param>
        public static void Clear(this DirectoryInfo obj)
        {
            FileSystemInfo[] fileinfo = obj.GetFileSystemInfos();
            foreach (FileSystemInfo item in fileinfo)
            {
                if (item is DirectoryInfo directoryInfo)
                {
                    directoryInfo.Delete(true);
                }
                else
                {
                    File.Delete(item.FullName);
                }
            }
        }
    }
}
