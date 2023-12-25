namespace Materal.Logger.Models
{
    /// <summary>
    /// 控制台日志目标配置模型
    /// </summary>
    public class ConsoleLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "Console";
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        /// <paramref name="loggerRuntime"></paramref>
        public override ILoggerHandler GetLoggerHandler(LoggerRuntime loggerRuntime) => new ConsoleLoggerHandler(loggerRuntime);
        /// <summary>
        /// 颜色组
        /// </summary>
        public LoggerColorsConfigModel Colors { get; set; } = new();
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
