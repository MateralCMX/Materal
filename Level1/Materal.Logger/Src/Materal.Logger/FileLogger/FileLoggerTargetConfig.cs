namespace Materal.Logger.FileLogger
{
    /// <summary>
    /// 控制台目标配置
    /// </summary>
    public class FileLoggerTargetConfig : BatchTargetConfig<FileLoggerWriter>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "File";
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; } = "C:\\MateralLogger\\FileLogger.log";
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; set; } = "${DateTime}|${Level}|${CategoryName}|${Scope}\r\n${Message}\r\n${Exception}";
    }
}
