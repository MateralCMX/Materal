using System.Text.Json.Serialization;

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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TargetTypeEnum Type { get; set; } = TargetTypeEnum.Console;
        /// <summary>
        /// 格式化
        /// </summary>
        public string Format { get; set; } = "${DateTime}|${Level}|${CategoryName}:${Message}\r\n${Exception}";
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 缓冲区数量
        /// </summary>
        public int BufferCount { get; set; } = 100;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string? ConnectionString { get; set; }
        /// <summary>
        /// 颜色组
        /// </summary>
        public LoggerColorsConfigModel Colors { get; set; } = new();
    }
}
