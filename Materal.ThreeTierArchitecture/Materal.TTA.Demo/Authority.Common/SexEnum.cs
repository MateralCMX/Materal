using System.ComponentModel;

namespace Authority.Common
{
    public enum SexEnum : byte
    {
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 0,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 1,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 2
    }
}
