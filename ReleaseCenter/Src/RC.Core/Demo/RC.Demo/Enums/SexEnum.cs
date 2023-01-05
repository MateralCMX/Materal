using System.ComponentModel;

namespace RC.Demo.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum SexEnum : byte
    {
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman = 0,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1
    }
}
