namespace Materal.Logger.FileLogger
{
    /// <summary>
    /// 文件日志器配置
    /// </summary>
    public class FileLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; set; } = "${DateTime}|${Level}|${CategoryName}|${Scope}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// 构造方法
        /// </summary>
        public FileLoggerTargetOptions()
        {
            string tempPath = System.IO.Path.GetTempPath();
            tempPath = System.IO.Path.Combine(tempPath, "MateralLogger", "FileLogger.log");
            Path = tempPath;
        }
    }
}
