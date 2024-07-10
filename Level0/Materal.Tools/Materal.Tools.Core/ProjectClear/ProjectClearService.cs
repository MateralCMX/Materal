using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.ProjectClear
{
    /// <summary>
    /// Materal版本服务
    /// </summary>
    public class ProjectClearService(ILogger<ProjectClearService>? logger = null) : IProjectClearService
    {
        private static readonly List<string> _dictionaryWhiteList =
        [
            ".vs",
            "bin",
            "obj",
            "node_modules"
        ];
        private static readonly List<string> _dictionaryBlackList =
        [
            ".git"
        ];
        /// <summary>
        /// 清理项目
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        public void ClearProject(string projectPath)
        {
            logger?.LogInformation($"开始清理...");
            DirectoryInfo projectDirectoryInfo = new(projectPath);
            if (!projectDirectoryInfo.Exists) throw new ToolsException($"\"{projectPath}\"路径不存在");
            ClearProject(projectDirectoryInfo);
            logger?.LogInformation($"清理结束");
        }
        /// <summary>
        /// 清理项目
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private void ClearProject(DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists) return;
            if (_dictionaryBlackList.Contains(directoryInfo.Name)) return;
            if (_dictionaryWhiteList.Contains(directoryInfo.Name))
            {
                DeleteDirectory(directoryInfo);
                return;
            }
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
            if (directoryInfos.Length > 0)
            {
                foreach (DirectoryInfo item in directoryInfos)
                {
                    ClearProject(item);
                }
                directoryInfos = directoryInfo.GetDirectories();
            }
            if (directoryInfos.Length <= 0 && directoryInfo.GetFiles().Length <= 0)
            {
                DeleteDirectory(directoryInfo);
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="directoryInfo"></param>
        private void DeleteDirectory(DirectoryInfo directoryInfo)
        {
            logger?.LogInformation($"移除文件夹:{directoryInfo.FullName}");
            try
            {
                directoryInfo.Delete(true);
            }
            catch
            {
                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    try
                    {
                        logger?.LogInformation($"移除文件:{fileInfo.FullName}");
                        fileInfo.Delete();
                    }
                    catch (Exception fileEx)
                    {
                        logger?.LogInformation($"移除文件失败:{fileInfo.FullName}", fileEx);
                        throw;
                    }
                }
                foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
                {
                    DeleteDirectory(subDirectoryInfo);
                }
                try
                {
                    directoryInfo.Delete(true);
                }
                catch (Exception subDirEx)
                {
                    logger?.LogError($"移除空文件夹失败:{directoryInfo.FullName}", subDirEx);
                    throw;
                }
            }
        }
    }
}
