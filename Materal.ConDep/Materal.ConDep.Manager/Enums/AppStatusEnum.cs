using System.ComponentModel;

namespace Materal.ConDep.Manager.Enums
{
    /// <summary>
    /// 应用程序状态
    /// </summary>
    public enum AppStatusEnum
    {
        /// <summary>
        /// 停止
        /// </summary>
        [Description("停止")]
        Stop,
        /// <summary>
        /// 开始
        /// </summary>
        [Description("运行中")]
        Start,
        /// <summary>
        /// 停止中
        /// </summary>
        [Description("停止中")]
        Stopping,
        /// <summary>
        /// 启动中
        /// </summary>
        [Description("启动中")]
        Starting
    }
}
