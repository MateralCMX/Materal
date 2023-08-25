namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志目标配置模型
    /// </summary>
    public class LoggerTargetConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; } = "Console";
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; set; } = "${DateTime}|${Level}|${CategoryName}|${Scope}\r\n${Message}\r\n${Exception}";
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string? ConnectionString { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int? Port { get; set; }
        /// <summary>
        /// 颜色组
        /// </summary>
        public LoggerColorsConfigModel Colors { get; set; } = new();
    }
}
