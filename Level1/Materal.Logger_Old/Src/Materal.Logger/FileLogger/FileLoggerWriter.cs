namespace Materal.Logger.FileLogger
{
    /// <summary>
    /// 文件日志写入器
    /// </summary>
    public class FileLoggerWriter(FileLoggerTargetConfig targetConfig) : BatchLoggerWriter<FileLoggerWriterModel, FileLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(FileLoggerWriterModel[] models)
        {
            IGrouping<string, FileLoggerWriterModel>[] fileModels = models.GroupBy(m => m.Path).ToArray();
            Parallel.ForEach(fileModels, item =>
            {
                try
                {
                    string[] fileContents = item.ToArray().Select(m =>
                    {
                        var result = m.FileContent;
                        if (result.EndsWith("\r\n"))
                        {
                            result = result[1..^2];
                        }
                        return result;
                    }).ToArray();
                    string fileContent = string.Join("\r\n", fileContents);
                    SaveFile(fileContent, item.Key);
                }
                catch (Exception exception)
                {
                    LoggerHost.LoggerLog?.LogError($"保存日志文件[{item.Key}]失败", exception);
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
    }
}
