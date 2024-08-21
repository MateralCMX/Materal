namespace Materal.Utils.Model
{
    /// <summary>
    /// 控制台消息模型
    /// </summary>
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
