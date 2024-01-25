using Materal.Abstractions;
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
        public override async Task StartApplicationAsync(ApplicationRuntimeModel applicationRuntime)
        {
            string startArgs = await GetStartArgsAsync(applicationRuntime);
            await StartApplicationAsync(applicationRuntime, "dotnet.exe", startArgs);
        }
        /// <summary>
        /// 结束应用程序
        /// </summary>
        /// <param name="applicationRuntime"></param>
        public override async Task StopApplicationAsync(ApplicationRuntimeModel applicationRuntime) => await StopApplicationAsync(applicationRuntime, ApplicationTypeEnum.DotNet);
        #region 私有方法
        /// <summary>
        /// 获得启动参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<string> GetStartArgsAsync(ApplicationRuntimeModel model)
        {
            using IServiceScope scope = MateralServices.Services.CreateScope();
            IDefaultDataRepository defaultDataRepository = scope.ServiceProvider.GetRequiredService<IDefaultDataRepository>();
            List<DefaultData> defaultDatas = await defaultDataRepository.FindAsync(m => m.ApplicationType == ApplicationTypeEnum.DotNet);
            List<string> runParams = [];
            if(model.ApplicationInfo.RunParams is not null && !string.IsNullOrWhiteSpace(model.ApplicationInfo.RunParams))
            {
                runParams.Add(model.ApplicationInfo.RunParams);
            }
            foreach (DefaultData defaultData in defaultDatas)
            {
                runParams.Add($"--{defaultData.Key}={defaultData.Data}");
            }
            string runParamsStr = string.Join(" ", runParams);
            return string.IsNullOrWhiteSpace(runParamsStr) ? $"{model.ApplicationInfo.MainModule}" : $"{model.ApplicationInfo.MainModule} {runParamsStr}";
        }
        #endregion
    }
}
