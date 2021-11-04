using Deploy.Enums;

namespace Deploy.ServiceImpl.Models
{
    public class NodeJSApplicationHandler : ExeApplicationHandler
    {
        public override void StartApplication(ApplicationRuntimeModel applicationRuntime)
        {
            StartApplication(applicationRuntime, "node.exe", applicationRuntime.RunParams);
        }

        public override void StopApplication(ApplicationRuntimeModel applicationRuntime)
        {
            StopApplication(applicationRuntime, ApplicationTypeEnum.DotNet);
        }
    }
}
