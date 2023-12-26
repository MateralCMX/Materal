namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 队列控制台目标配置
    /// </summary>
    public class QueueConsoleLoggerTargetConfig : QueueTargetConfig<QueueConsoleLoggerWriter>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "QueueConsole";
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; set; } = "${DateTime}|${Level}|${CategoryName}|${Scope}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// 颜色组
        /// </summary>
        public ColorsConfig Colors { get; set; } = new();
    }
}
