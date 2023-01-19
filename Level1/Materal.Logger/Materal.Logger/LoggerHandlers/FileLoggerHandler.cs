using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Materal.Logger.LoggerHandlers
{
    public class FileLoggerHandler : BufferLoggerHandler<FileModel>
    {
        public FileLoggerHandler(MateralLoggerRuleConfigModel rule, MateralLoggerTargetConfigModel target) : base(rule, target)
        {
        }
        public override void Handler(LogLevel logLevel, string message, string? categoryName, MateralLoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            if (string.IsNullOrWhiteSpace(Target.Path)) return;
            string writeMessage = FormatMessage(Target.Format, logLevel, message, categoryName, scope, dateTime, exception, threadID);
            string path = FormatPath(Target.Path, logLevel, categoryName, scope, dateTime, threadID);
            var data = new FileModel
            {
                FileContent = writeMessage,
                Path = path
            };
            PushData(data);
            SendMessage(writeMessage, logLevel);
        }
        protected override void SaveData(FileModel[] datas)
        {
            IGrouping<string, FileModel>[] fileModels = datas.GroupBy(m => m.Path).ToArray();
            Parallel.ForEach(fileModels, item =>
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
                if (string.IsNullOrWhiteSpace(Target.Path)) return;
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
                MateralLoggerLog.LogError("保存文件失败", exception);
            }
        }
    }
}
