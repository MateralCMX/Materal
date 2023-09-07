using Materal.Logger.LoggerHandlers;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 文件日志目标配置模型
    /// </summary>
    public class FileLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "File";
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        public override ILoggerHandler GetLoggerHandler() => new FileLoggerHandler();
        private string _path = "C:\\MateralLogger\\FileLogger.log";
        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value)) throw new LoggerException("路径格式不能为空");
                if (!value.IsRelativePath() && !value.IsAbsolutePath()) throw new LoggerException("路径格式错误");
                _path = value;
            }
        }
        private string _format = "${DateTime}|${Level}|${CategoryName}|${Scope}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format
        {
            get => _format;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value)) throw new LoggerException("格式化字符串不能为空");
                _format = value;
            }
        }
    }
}
