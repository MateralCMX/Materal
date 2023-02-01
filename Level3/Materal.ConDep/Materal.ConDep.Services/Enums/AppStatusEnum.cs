using System.ComponentModel;

namespace Materal.ConDep.Services.Enums
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
        Stop = 0,
        /// <summary>
        /// 开始
        /// </summary>
        [Description("运行中")]
        Start = 1,
        /// <summary>
        /// 停止中
        /// </summary>
        [Description("停止中")]
        Stopping = 2,
        /// <summary>
        /// 启动中
        /// </summary>
        [Description("启动中")]
        Starting = 3,
        /// <summary>
        /// 发生错误
        /// </summary>
        [Description("发生错误")]
        Error = 4
    }
}
