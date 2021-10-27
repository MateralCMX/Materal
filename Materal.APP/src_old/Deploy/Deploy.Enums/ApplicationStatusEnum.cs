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
        Runing,
        /// <summary>
        /// 正在停止
        /// </summary>
        [Description("正在停止")]
        Stoping,
        /// <summary>
        /// 已停止
        /// </summary>
        [Description("已停止")]
        Stop,
        /// <summary>
        /// 发生错误
        /// </summary>
        [Description("错误")]
        Error = byte.MaxValue
    }
}
