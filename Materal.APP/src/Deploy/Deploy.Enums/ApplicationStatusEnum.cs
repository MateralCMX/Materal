using System.ComponentModel;

namespace Deploy.Enums
{
    /// <summary>
    /// 应用程序状态枚举
    /// </summary>
    public enum ApplicationStatusEnum : byte
    {
        /// <summary>
        /// 准备运行
        /// </summary>
        [Description("准备运行")]
        ReadyRun,
        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Running = 1,
        /// <summary>
        /// 正在停止
        /// </summary>
        [Description("正在停止")]
        Stopping = 2,
        /// <summary>
        /// 已停止
        /// </summary>
        [Description("已停止")]
        Stop = 3,
        /// <summary>
        /// 发布中
        /// </summary>
        [Description("发布中")]
        Release = 4
    }
}
