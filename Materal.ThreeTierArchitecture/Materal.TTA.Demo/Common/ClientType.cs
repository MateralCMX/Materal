using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// Token类型
    /// </summary>
    public enum ClientType : byte
    {
        /// <summary>
        /// Web用户
        /// </summary>
        [Description("Web用户")]
        Web = 0
    }
}
