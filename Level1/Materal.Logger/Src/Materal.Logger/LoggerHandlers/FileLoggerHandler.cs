using System.Text;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 文件日志处理器
    /// </summary>
    public class FileLoggerHandler : BufferLoggerHandler<FileLoggerHandlerModel>
    {
        /// <summary>
        /// 处理合格的数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void HandlerOKData(FileLoggerHandlerModel[] datas)
        {
            IGrouping<string, FileLoggerHandlerModel>[] fileModels = datas.GroupBy(m => m.Path).ToArray();
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
                    LoggerLog.LogError($"日志记录到文件[{item.Key}]失败：", exception);
                }
            });
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="path"></param>
        private void SaveFile(string fileContent, string path)
        {
            Encoding defaultEncoding = Encoding.UTF8;
            try
            {
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
            catch (Exception exception)
            {
                LoggerLog.LogError("保存文件失败", exception);
            }
        }
    }
}
