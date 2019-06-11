namespace Materal.Dispatcher.QuartzNet
{
    public class DispatcherConfigModel
    {
        /// <summary>
        /// 启用日志
        /// </summary>
        public bool EnableLog { get; set; } = true;
        /// <summary>
        /// 作业日志配置文件路径
        /// </summary>
        public string DispatcherConfigFilePath { get; set; } = "~/DispatcherConfig.xml";
        /// <summary>
        /// 调度器配置
        /// </summary>
        public SchedulerConfigModel SchedulerConfig { get; set; }
    }
}
