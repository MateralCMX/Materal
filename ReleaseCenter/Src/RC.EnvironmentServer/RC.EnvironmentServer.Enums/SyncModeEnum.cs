using System.ComponentModel;

namespace RC.EnvironmentServer.Enums
{
    /// <summary>
    /// 同步模式枚举
    /// </summary>
    public enum SyncModeEnum
    {
        /// <summary>
        /// 缺少项
        /// </summary>
        [Description("缺少项")]
        Mission = 0,
        /// <summary>
        /// 替换
        /// </summary>
        [Description("替换")]
        Replace = 1,
        /// <summary>
        /// 覆盖
        /// </summary>
        [Description("覆盖")]
        Cover = 2,
    }
}