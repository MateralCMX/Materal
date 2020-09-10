using System;

namespace Materal.APP.Core
{
    /// <summary>
    /// 控制台消息模型
    /// </summary>
    internal class ConsoleMessageModel
    {
        /// <summary>
        /// 新行
        /// </summary>
        public bool NewLine { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 控制台颜色
        /// </summary>
        public ConsoleColor ConsoleColor { get; set; }
    }
}
