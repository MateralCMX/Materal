using RC.Deploy.Application.Services.Models;

namespace RC.Deploy.Application.Services.ApplicationHandlers
{
    /// <summary>
    /// 静态文档应用程序处理器
    /// </summary>
    public class StaticDocumentApplicationHandler : ApplicationHandler
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        /// <exception cref="RCException"></exception>
        public override async Task StartApplicationAsync(ApplicationRuntimeModel applicationRuntime)
        {
            if (applicationRuntime.ApplicationStatus != ApplicationStatusEnum.Stop) throw new RCException("应用程序尚未停止");
            applicationRuntime.ApplicationStatus = ApplicationStatusEnum.ReadyRun;
            applicationRuntime.ClearConsoleMessage();
            applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}准备启动....");
            applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}开始启动");
            applicationRuntime.ApplicationStatus = ApplicationStatusEnum.Runing;
            applicationRuntime.AddConsoleMessage($"{applicationRuntime.ApplicationInfo.Name}启动完毕");
            applicationRuntime.AddConsoleMessage($"网站[{applicationRuntime.ApplicationInfo.Name}]已启动:/{applicationRuntime.ApplicationInfo.RootPath}/{applicationRuntime.ApplicationInfo.MainModule}");
            await Task.CompletedTask;
        }
        /// <summary>
        /// 停止应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        public override async Task StopApplicationAsync(ApplicationRuntimeModel applicationRuntime) => await StopApplicationAsync(applicationRuntime, ApplicationTypeEnum.StaticDocument);
    }
}
