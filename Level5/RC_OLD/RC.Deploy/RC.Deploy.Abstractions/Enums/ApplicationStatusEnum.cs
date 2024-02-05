namespace RC.Deploy.Abstractions.Enums
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
        ReadyRun = 0,
        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Runing = 1,
        /// <summary>
        /// 正在停止
        /// </summary>
        [Description("正在停止")]
        Stoping = 2,
        /// <summary>
        /// 已停止
        /// </summary>
        [Description("已停止")]
        Stop = 3,
        /// <summary>
        /// 更新中
        /// </summary>
        [Description("更新中")]
        Update = 4
    }
}
