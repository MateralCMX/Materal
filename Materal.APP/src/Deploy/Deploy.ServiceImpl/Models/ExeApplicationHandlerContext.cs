using System.Diagnostics;
using Deploy.Common;

namespace Deploy.ServiceImpl.Models
{
    public class ExeApplicationHandlerContext : ApplicationHandlerContext
    {
        public override Process GetProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == Enums.ApplicationTypeEnum.Exe)
            {
                return null;
            }
            if (_next != null)
            {
                return _next.GetProcess(model);
            }
            throw new DeployException("未识别应用程序类型");
        }

        public override void KillProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == Enums.ApplicationTypeEnum.Exe)
            {
                return;
            }
            if (_next == null) throw new DeployException("未识别应用程序类型");
            _next.KillProcess(model);
        }
    }
}
