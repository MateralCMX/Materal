using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Materal.Tools.Core.ProjectClear
{
    /// <summary>
    /// Materal项目清理服务
    /// </summary>
    public class ProjectClearService(ProjectClearConfig? config = null, ILogger<ProjectClearService>? logger = null) : IProjectClearService
    {
        private readonly ProjectClearConfig _config = config ?? new();
        private readonly ConcurrentDictionary<string, bool> _processedPaths = new();
        private readonly ClearProgress _progress = new();
        /// <inheritdoc/>
        public void ClearProject(string projectPath, IProgress<ClearProgress>? progress = null) => ClearProjectAsync(projectPath, progress).GetAwaiter().GetResult();
        /// <inheritdoc/>
        public async Task ClearProjectAsync(string projectPath, IProgress<ClearProgress>? progress = null, CancellationToken cancellationToken = default)
        {
            logger?.LogInformation("开始清理...");
            DirectoryInfo projectDirectoryInfo = new(projectPath);
            if (!projectDirectoryInfo.Exists) throw new ToolsException($"\"{projectPath}\"路径不存在");
            _processedPaths.Clear();
            _progress.ProcessedFiles = 0;
            _progress.ProcessedDirectories = 0;
            await ClearProjectInternalAsync(projectDirectoryInfo, progress, cancellationToken);
            logger?.LogInformation($"清理结束，共处理{_progress.ProcessedFiles}个文件，{_progress.ProcessedDirectories}个文件夹");
        }
        /// <summary>
        /// 清理项目内部
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="progress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task ClearProjectInternalAsync(DirectoryInfo directoryInfo, IProgress<ClearProgress>? progress, CancellationToken cancellationToken)
        {
            if (!directoryInfo.Exists) return;
            if (_config.DictionaryBlackList.Contains(directoryInfo.Name)) return;
            cancellationToken.ThrowIfCancellationRequested();
            _progress.CurrentPath = directoryInfo.FullName;
            if (_config.DictionaryWhiteList.Contains(directoryInfo.Name))
            {
                await DeleteDirectoryAsync(directoryInfo, progress, cancellationToken);
                return;
            }
            List<Task> tasks = [];
            foreach (DirectoryInfo subDir in directoryInfo.GetDirectories())
            {
                if (_processedPaths.TryAdd(subDir.FullName, true))
                {
                    tasks.Add(ClearProjectInternalAsync(subDir, progress, cancellationToken));
                }
            }
            await Task.WhenAll(tasks);
            if (_config.CanDeleteEmptyDirectory && directoryInfo.GetDirectories().Length <= 0 && !directoryInfo.GetFiles().Any(m => _config.FileFilter?.Invoke(m) ?? true))
            {
                await DeleteDirectoryAsync(directoryInfo, progress, cancellationToken);
            }
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="progress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task DeleteDirectoryAsync(DirectoryInfo directoryInfo, IProgress<ClearProgress>? progress, CancellationToken cancellationToken)
        {
            logger?.LogInformation($"移除文件夹:{directoryInfo.FullName}");
            try
            {
                await Task.Run(() => directoryInfo.Delete(true), cancellationToken);
                _progress.ProcessedDirectories++;
                progress?.Report(new()
                {
                    CurrentPath = directoryInfo.FullName,
                    ProcessedFiles = _progress.ProcessedFiles,
                    ProcessedDirectories = _progress.ProcessedDirectories
                });
            }
            catch
            {
                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    try
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        if (!(_config.FileFilter?.Invoke(fileInfo) ?? true)) continue;
                        logger?.LogInformation($"移除文件:{fileInfo.FullName}");
                        await Task.Run(() => fileInfo.Delete(), cancellationToken);
                        _progress.ProcessedFiles++;
                        progress?.Report(new()
                        {
                            CurrentPath = fileInfo.FullName,
                            ProcessedFiles = _progress.ProcessedFiles,
                            ProcessedDirectories = _progress.ProcessedDirectories
                        });
                    }
                    catch (Exception fileEx)
                    {
                        logger?.LogError($"移除文件失败:{fileInfo.FullName}", fileEx);
                        throw;
                    }
                }
                List<Task> tasks = [];
                foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
                {
                    if (!_processedPaths.TryAdd(subDirectoryInfo.FullName, true)) continue;
                    tasks.Add(DeleteDirectoryAsync(subDirectoryInfo, progress, cancellationToken));
                }
                await Task.WhenAll(tasks);
                try
                {
                    await Task.Run(() => directoryInfo.Delete(true), cancellationToken);
                    _progress.ProcessedDirectories++;
                    progress?.Report(new()
                    {
                        CurrentPath = directoryInfo.FullName,
                        ProcessedFiles = _progress.ProcessedFiles,
                        ProcessedDirectories = _progress.ProcessedDirectories
                    });
                }
                catch (Exception ex)
                {
                    logger?.LogError($"移除空文件夹失败:{directoryInfo.FullName}", ex);
                    throw;
                }
            }
        }
    }
}
