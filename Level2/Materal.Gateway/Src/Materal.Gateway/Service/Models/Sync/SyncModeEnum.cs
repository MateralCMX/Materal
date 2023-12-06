using System.ComponentModel;

namespace Materal.Gateway.Service.Models.Sync
{
    /// <summary>
    /// 同步模式枚举
    /// </summary>
    public enum SyncModeEnum
    {
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add = 0,
        /// <summary>
        /// 替换
        /// </summary>
        [Description("替换")]
        Replace = 1,
        /// <summary>
        /// 覆盖
        /// </summary>
        [Description("覆盖")]
        Cover = 2
    }
}
