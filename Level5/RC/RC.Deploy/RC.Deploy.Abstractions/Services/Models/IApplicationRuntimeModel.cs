namespace RC.Deploy.Abstractions.Services.Models
{
    /// <summary>
    /// 应用程序运行时模型
    /// </summary>
    public interface IApplicationRuntimeModel
    {
        /// <summary>
        /// 应用程序信息
        /// </summary>
        Domain.ApplicationInfo ApplicationInfo { get; set; }
        /// <summary>
        /// 应用程序状态
        /// </summary>
        ApplicationStatusEnum ApplicationStatus { get; set; }
        /// <summary>
        /// 压缩包文件组文件夹路径
        /// </summary>
        string RarFilesDirectoryPath { get; }
        /// <summary>
        /// 发布文件夹路径
        /// </summary>
        string PublishDirectoryPath { get; }
        /// <summary>
        /// 执行启动任务
        /// </summary>
        void ExecuteStartTask();
        /// <summary>
        /// 执行关闭任务
        /// </summary>
        void ExecuteStopTask();
        /// <summary>
        /// 执行应用最后一个文件任务
        /// </summary>
        void ExecuteApplyLatestFileTask();
        /// <summary>
        /// 执行应用文件任务
        /// </summary>
        void ExecuteApplyFileTask(string fileName);
        /// <summary>
        /// 杀死程序
        /// </summary>
        Task KillAsync();
        /// <summary>
        /// 添加控制台消息
        /// </summary>
        /// <param name="message"></param>
        void AddConsoleMessage(string message);
        /// <summary>
        /// 清空控制台消息
        /// </summary>
        void ClearConsoleMessage();
        /// <summary>
        /// 获得控制台消息
        /// </summary>
        /// <returns></returns>
        List<string> GetConsoleMessages();
        /// <summary>
        /// 获得压缩包文件列表名称
        /// </summary>
        /// <returns></returns>
        FileInfo[]? GetRarFileNames();
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        Task ShutDownAsync();
    }
}
