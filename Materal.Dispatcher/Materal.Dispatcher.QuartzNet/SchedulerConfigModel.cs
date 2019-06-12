namespace Materal.Dispatcher.QuartzNet
{
    /// <summary>
    /// 调度器配置模型
    /// </summary>
    public class SchedulerConfigModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "MateralDispatcher";
        /// <summary>
        /// 线程数
        /// </summary>
        public uint TreadCount { get; set; } = 5;
        /// <summary>
        /// 访问端口号
        /// </summary>
        public uint Port { get; set; } = 8008;
        /// <summary>
        /// 绑定名称
        /// </summary>
        public string BindName { get; set; } = "QuartzScheduler";
        /// <summary>
        /// 启用作业日志
        /// </summary>
        public bool EnableJobLog { get; set; } = true;
        /// <summary>
        /// 启用调度器日志
        /// </summary>
        public bool EnableSchedulerLog { get; set; } = true;
        /// <summary>
        /// 启用自定义作业工厂
        /// </summary>
        public bool EnableCustomJobFactory { get; set; } = true;
        /// <summary>
        /// 启用触发器日志
        /// </summary>
        public bool EnableTriggerLog { get; set; } = true;
    }
}
