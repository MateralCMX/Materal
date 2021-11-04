using Deploy.Enums;
using System;
using System.IO;

namespace Deploy.ServiceImpl.Models
{
    public class DotNetApplicationHandler : ExeApplicationHandler
    {
        public override void StartApplication(ApplicationRuntimeModel applicationRuntime)
        {
            string startArgs = GetStartArgs(applicationRuntime);
            StartApplication(applicationRuntime, "dotnet.exe", startArgs);
        }

        public override void StopApplication(ApplicationRuntimeModel applicationRuntime)
        {
            StopApplication(applicationRuntime, ApplicationTypeEnum.DotNet);
        }

        #region 私有方法
        /// <summary>
        /// 获得启动参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetStartArgs(ApplicationRuntimeModel model)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application", model.Path, $"{model.MainModule}");
            string result = string.IsNullOrEmpty(model.RunParams) ? $"{path}" : $"{path} {model.RunParams}";
            return result;
        }

        #endregion
    }
}
