using System.ComponentModel;

namespace Log.Common
{
    public enum SensitiveLogLevelEnum : byte
    {
        /// <summary>
        /// 增加
        /// </summary>
        [Description("增加")]
        Add = 0,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Edit = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 2,
        /// <summary>
        /// 业务
        /// </summary>
        [Description("业务")]
        Business = 3
    }
}
