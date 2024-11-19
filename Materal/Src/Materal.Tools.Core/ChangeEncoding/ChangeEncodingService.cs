using Microsoft.Extensions.Logging;
using System.Text;
using Ude;

namespace Materal.Tools.Core.ChangeEncoding
{
    /// <summary>
    /// 更改编码服务
    /// </summary>
    public class ChangeEncodingService(ILogger<ChangeEncodingService>? logger = null) : IChangeEncodingService
    {
        static ChangeEncodingService()
        {
#if NET
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
        }
        /// <summary>
        /// 更改编码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        public async Task ChangeEncodingAsync(string path, ChangeEncodingOptions? options = null)
        {
            logger?.LogInformation($"转换开始...");
            options ??= new();
            FileInfo fileInfo = new(path);
            if (fileInfo.Exists)
            {
                await ChangeEncodingAsync(fileInfo, options);
            }
            else
            {
                DirectoryInfo directoryInfo = new(path);
                if (directoryInfo.Exists)
                {
                    await ChangeEncodingAsync(directoryInfo, options);
                }
                else
                {
                    throw new ToolsException($"路径{path}不存在");
                }
            }
            logger?.LogInformation($"转换完成");
        }
        /// <summary>
        /// 更改编码
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ToolsException"></exception>
        private async Task ChangeEncodingAsync(DirectoryInfo directoryInfo, ChangeEncodingOptions options)
        {
            if (!directoryInfo.Exists) throw new ToolsException("文件夹不存在");
            IEnumerable<FileInfo> fileInfos = directoryInfo.GetFiles().Where(options.Filter);
            foreach (FileInfo fileInfo in fileInfos)
            {
                await ChangeEncodingAsync(fileInfo, options);
            }
            if (options.Recursive)
            {
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo subDirectoryInfo in directoryInfos)
                {
                    await ChangeEncodingAsync(subDirectoryInfo, options);
                }
            }
        }
        /// <summary>
        /// 更改编码
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private async Task ChangeEncodingAsync(FileInfo fileInfo, ChangeEncodingOptions options)
        {
            logger?.LogInformation($"正在检测文件{fileInfo.FullName}");
            Encoding fileEncoding = GetFileEncoding(fileInfo);
            if (fileEncoding == options.WriteEncoding)
            {
                logger?.LogInformation($"编码为:{fileEncoding.EncodingName},无需转换");
                return;
            }
            else
            {
                Encoding readEncoding = options.ReadEncoding ?? fileEncoding;
#if NET
                string text = await File.ReadAllTextAsync(fileInfo.FullName, readEncoding);
                await File.WriteAllTextAsync(fileInfo.FullName, text, options.WriteEncoding);
#else
                string text = File.ReadAllText(fileInfo.FullName, readEncoding);
                File.WriteAllText(fileInfo.FullName, text, options.WriteEncoding);
                await Task.CompletedTask;
#endif
                logger?.LogInformation($"编码已从{fileEncoding.EncodingName}转换为:{options.WriteEncoding.EncodingName}");
            }
        }
        /// <summary>
        /// 获得文件编码
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private static Encoding GetFileEncoding(FileInfo fileInfo)
        {
            using FileStream fs = File.OpenRead(fileInfo.FullName);
            CharsetDetector detector = new();
            detector.Feed(fs);
            detector.DataEnd();
            if (detector.Charset != null)
            {
                return Encoding.GetEncoding(detector.Charset);
            }
            else
            {
                return Encoding.Default;
            }
        }
    }
}
