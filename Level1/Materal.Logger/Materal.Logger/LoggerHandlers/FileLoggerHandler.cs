using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 文件日志处理器
    /// </summary>
    public class FileLoggerHandler : BufferLoggerHandler<FileModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        public FileLoggerHandler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target) : base(rule, target)
        {
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="exception"></param>
        /// <param name="threadID"></param>
        public override void Handler(LogLevel logLevel, string message, string? categoryName, LoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            if (Target.Path == null || string.IsNullOrWhiteSpace(Target.Path)) return;
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
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="datas"></param>
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
                LoggerLog.LogError("保存文件失败", exception);
            }
        }
    }
}
