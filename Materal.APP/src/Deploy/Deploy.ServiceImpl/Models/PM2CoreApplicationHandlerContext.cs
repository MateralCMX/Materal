using System.Diagnostics;
using Deploy.Common;

namespace Deploy.ServiceImpl.Models
{
    public class PM2CoreApplicationHandlerContext : ApplicationHandlerContext
    {
        public override Process GetProcess(ApplicationRuntimeModel model)
        {
            if (model.ApplicationInfo.ApplicationType == Enums.ApplicationTypeEnum.PM2)
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
            if (model.ApplicationInfo.ApplicationType == Enums.ApplicationTypeEnum.PM2)
            {
                return;
            }
            if (_next == null) throw new DeployException("未识别应用程序类型");
            _next.KillProcess(model);
        }
    }
}
