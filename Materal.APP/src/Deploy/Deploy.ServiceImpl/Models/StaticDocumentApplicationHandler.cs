using Deploy.Common;
using Deploy.Enums;
using Materal.APP.Core;
using System.Collections.Generic;

namespace Deploy.ServiceImpl.Models
{
    public class StaticDocumentApplicationHandler : ApplicationHandler
    {
        public override void StartApplication(ApplicationRuntimeModel applicationRuntime)
        {
            if (applicationRuntime.Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            applicationRuntime.Status = ApplicationStatusEnum.ReadyRun;
            ConsoleMessage = new List<string>();
            applicationRuntime.Status = ApplicationStatusEnum.Running;
            ConsoleMessage.Add($"网站[{applicationRuntime.Name}]已启动:{ApplicationConfig.BaseUrlConfig.Url}/{applicationRuntime.Path}/{applicationRuntime.MainModule}");
        }

        public override void StopApplication(ApplicationRuntimeModel applicationRuntime)
        {
            StopApplication(applicationRuntime, ApplicationTypeEnum.StaticDocument);
        }
    }
}
