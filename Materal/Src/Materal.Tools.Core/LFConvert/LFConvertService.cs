
using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.LFConvert
{
    /// <summary>
    /// LF转换服务
    /// </summary>
    public class LFConvertService(ILogger<LFConvertService>? logger = null) : ILFConvertService
    {
        /// <summary>
        /// CRLF转换为LF
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public async Task CRLFToLFAsync(string path, LFConvertOptions? options = null)
        {
            logger?.LogInformation($"转换开始...");
            options ??= new();
            FileInfo fileInfo = new(path);
            if (fileInfo.Exists)
            {
                await CRLFToLFAsync(fileInfo);
            }
            else
            {
                DirectoryInfo directoryInfo = new(path);
                if (directoryInfo.Exists)
                {
                    await CRLFToLFAsync(directoryInfo, options);
                }
                else
                {
                    throw new ToolsException($"路径{path}不存在");
                }
            }
            logger?.LogInformation($"转换完成");
        }
        /// <summary>
        /// LF转换为CRLF
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public async Task LFToCRLFAsync(string path, LFConvertOptions? options = null)
        {
            logger?.LogInformation($"转换开始...");
            options ??= new();
            FileInfo fileInfo = new(path);
            if (fileInfo.Exists)
            {
                await LFToCRLFAsync(fileInfo);
            }
            else
            {
                DirectoryInfo directoryInfo = new(path);
                if (directoryInfo.Exists)
                {
                    await LFToCRLFAsync(directoryInfo, options);
                }
                else
                {
                    throw new ToolsException($"路径{path}不存在");
                }
            }
            logger?.LogInformation($"转换完成");
        }
        /// <summary>
        /// CRLF转换为LF
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        private async Task CRLFToLFAsync(DirectoryInfo directoryInfo, LFConvertOptions options)
        {
            if (!directoryInfo.Exists) throw new ToolsException("文件夹不存在");
            IEnumerable<FileInfo> fileInfos = directoryInfo.GetFiles().Where(options.Filter);
            foreach (FileInfo fileInfo in fileInfos)
            {
                await CRLFToLFAsync(fileInfo);
            }
            if (options.Recursive)
            {
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo subDirectoryInfo in directoryInfos)
                {
                    await CRLFToLFAsync(subDirectoryInfo, options);
                }
            }
        }
        /// <summary>
        /// LF转换为CRLF
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        private async Task LFToCRLFAsync(DirectoryInfo directoryInfo, LFConvertOptions options)
        {
            if (!directoryInfo.Exists) throw new ToolsException("文件夹不存在");
            IEnumerable<FileInfo> fileInfos = directoryInfo.GetFiles().Where(options.Filter);
            foreach (FileInfo fileInfo in fileInfos)
            {
                await LFToCRLFAsync(fileInfo);
            }
            if (options.Recursive)
            {
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo subDirectoryInfo in directoryInfos)
                {
                    await LFToCRLFAsync(subDirectoryInfo, options);
                }
            }
        }
        /// <summary>
        /// CRLF转换为LF
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private async Task CRLFToLFAsync(FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException($"文件{fileInfo.FullName}不存在");
            logger?.LogInformation($"正在转换{fileInfo.FullName}为LF");
#if NET
            string[] contents = await File.ReadAllLinesAsync(fileInfo.FullName);
#else
            string[] contents = File.ReadAllLines(fileInfo.FullName);
            await Task.CompletedTask;
#endif
            for (int i = 0; i < contents.Length; i++)
            {
                if (!contents[i].EndsWith("\r\n")) continue;
                contents[i] = contents[i].Replace("\r\n", "\n");
            }
            logger?.LogInformation($"转换{fileInfo.FullName}成功");
#if NET
            await File.WriteAllLinesAsync(fileInfo.FullName, contents);
#else
            File.WriteAllLines(fileInfo.FullName, contents);
            await Task.CompletedTask;
#endif
        }
        /// <summary>
        /// LF转换为CRLF
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private async Task LFToCRLFAsync(FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException($"文件{fileInfo.FullName}不存在");
            logger?.LogInformation($"正在转换{fileInfo.FullName}为LF");
#if NET
            string[] contents = await File.ReadAllLinesAsync(fileInfo.FullName);
#else
            string[] contents = File.ReadAllLines(fileInfo.FullName);
#endif
            for (int i = 0; i < contents.Length; i++)
            {
                if (!contents[i].EndsWith("\n")) continue;
                contents[i] = contents[i].Replace("\n", "\r\n");
            }
            logger?.LogInformation($"转换{fileInfo.FullName}成功");
#if NET
            await File.WriteAllLinesAsync(fileInfo.FullName, contents);
#else
            File.WriteAllLines(fileInfo.FullName, contents);
            await Task.CompletedTask;
#endif
        }
    }
}
