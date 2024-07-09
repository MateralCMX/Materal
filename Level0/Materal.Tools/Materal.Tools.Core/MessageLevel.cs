using System.ComponentModel;

namespace Materal.Tools.Core
{
    /// <summary>
    /// 消息等级
    /// </summary>
    public enum MessageLevel : byte
    {
        /// <summary>
        /// 消息
        /// </summary>
        [Description("消息")]
        Information = 0,
        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warning = 1,
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error = 2
    }
}
