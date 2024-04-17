using Materal.Logger.BatchLogger;

namespace Materal.Logger.FileLogger
{
    /// <summary>
    /// 文件日志写入器
    /// </summary>
    public class FileLoggerWriter(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : BatchLoggerWriter<FileLoggerTargetOptions>(options, loggerInfo)
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="batchLogs"></param>
        /// <returns></returns>
        public override async Task LogAsync(BatchLog<FileLoggerTargetOptions>[] batchLogs)
        {
            FileLog[] fileLogs = batchLogs.Select(m => new FileLog(m, Options.CurrentValue)).ToArray();
            IGrouping<string, FileLog>[] fileModels = fileLogs.GroupBy(m => m.Path).ToArray();
            Parallel.ForEach(fileModels, item =>
            {
                try
                {
                    IEnumerable<string> fileContents = item.Select(m => m.FileContent);
                    string fileContent = string.Join("\r\n", fileContents);
                    SaveFile(fileContent, item.Key);
                }
                catch (Exception exception)
                {
                    LoggerInfo.LogError($"保存日志文件[{item.Key}]失败", exception);
                }
            });
            await Task.CompletedTask;
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="path"></param>
        private static void SaveFile(string fileContent, string path)
        {
            Encoding defaultEncoding = Encoding.UTF8;
            if (string.IsNullOrWhiteSpace(path)) return;
            FileInfo fileInfo = new(path);
            DirectoryInfo directoryInfo;
            string? directoryPath = Path.GetDirectoryName(path);
            if (string.IsNullOrWhiteSpace(directoryPath)) return;
            directoryInfo = new(directoryPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            if (!fileContent.EndsWith("\r\n"))
            {
                fileContent += "\r\n";
            }
            if (!fileInfo.Exists)
            {
                File.WriteAllText(fileInfo.FullName, fileContent, defaultEncoding);
            }
            else
            {
                File.AppendAllText(path, fileContent, defaultEncoding);
            }
        }
        private class FileLog(BatchLog<FileLoggerTargetOptions> batchLog, LoggerOptions options)
        {
            /// <summary>
            /// 路径
            /// </summary>
            public string Path { get; } = batchLog.Log.ApplyText(batchLog.TargetOptions.Path, options);
            /// <summary>
            /// 文件内容
            /// </summary>
            public string FileContent { get; } = batchLog.Log.ApplyText(batchLog.TargetOptions.Format, options);
        }
    }
}
