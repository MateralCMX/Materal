using Deploy.Common;
using System.Diagnostics;
using Deploy.Enums;
using Materal.APP.Core;

namespace Deploy.ServiceImpl.Models
{
    public class StaticDocumentApplicationHandlerContext : ApplicationHandlerContext
    {
        public override Process GetProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.StaticDocument)
            {
                model.ApplicationStatus = ApplicationStatusEnum.Runing;
                model.ConsoleMessage.Add($"网站[{model.ApplicationInfo.Name}]已启动:{ApplicationConfig.Url}/{model.ApplicationInfo.Path}/{model.ApplicationInfo.MainModule}");
                return null;
            }
            if (Next != null)
            {
                return Next.GetProcess(model);
            }
            throw new DeployException("未识别应用程序类型");
        }
        public override void KillProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.StaticDocument)
            {
                return;
            }
            if (Next == null) throw new DeployException("未识别应用程序类型");
            Next.KillProcess(model);
        }
        public override void KillProcess(ApplicationRuntimeModel model, Process process)
        {
            if (model.ApplicationInfo.ApplicationType == ApplicationTypeEnum.StaticDocument)
            {
                KillProcess(process);
                return;
            }
            if (Next == null) throw new DeployException("未识别应用程序类型");
            Next.KillProcess(model, process);
        }
    }
}
