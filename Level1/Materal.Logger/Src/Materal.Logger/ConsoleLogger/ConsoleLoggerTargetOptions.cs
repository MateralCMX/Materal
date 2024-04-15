namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 控制台日志目标配置
    /// </summary>
    public class ConsoleLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; set; } = "${DateTime}|${Level}|${CategoryName}|${Scope}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// 颜色配置
        /// </summary>
        public ColorsOptions Colors { get; set; } = new();
    }
}
