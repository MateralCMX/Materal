namespace RC.Deploy.ServiceImpl.Models
{
    /// <summary>
    /// 应用程序任务
    /// </summary>
    public class ApplicationTask
    {
        /// <summary>
        /// 应用程序
        /// </summary>
        public ApplicationRuntimeModel? Application { get; set; }
        /// <summary>
        /// 目标任务
        /// </summary>
        public Func<Task>? TargetTask { get; set; }
        /// <summary>
        /// 目标任务
        /// </summary>
        public Action? TargetAction { get; set; }
    }
}
