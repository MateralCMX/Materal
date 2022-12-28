namespace Materal.Common.Models
{
    public class ConsoleMessageModel
    {
        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor Color { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 参数
        /// </summary>
        public object?[]? Args { get; set; }
    }
}
