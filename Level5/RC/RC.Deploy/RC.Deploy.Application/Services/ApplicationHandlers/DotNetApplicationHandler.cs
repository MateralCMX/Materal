using RC.Deploy.Application.Services.Models;

namespace RC.Deploy.Application.Services.ApplicationHandlers
{
    /// <summary>
    /// DotNet应用程序处理器
    /// </summary>
    public class DotNetApplicationHandler : ExeApplicationHandler
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        public override void StartApplication(ApplicationRuntimeModel applicationRuntime)
        {
            string startArgs = GetStartArgs(applicationRuntime);
            StartApplication(applicationRuntime, "dotnet.exe", startArgs);
        }
        /// <summary>
        /// 结束应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        public override void StopApplication(ApplicationRuntimeModel applicationRuntime) => StopApplication(applicationRuntime, ApplicationTypeEnum.DotNet);
        #region 私有方法
        /// <summary>
        /// 获得启动参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static string GetStartArgs(ApplicationRuntimeModel model) => string.IsNullOrEmpty(model.ApplicationInfo.RunParams) ? $"{model.ApplicationInfo.MainModule}" : $"{model.ApplicationInfo.MainModule} {model.ApplicationInfo.RunParams}";
        #endregion
    }
}
