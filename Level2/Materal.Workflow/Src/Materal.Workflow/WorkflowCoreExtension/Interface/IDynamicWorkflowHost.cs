using Materal.Workflow.WorkflowCoreExtension.Interface;
using WorkflowCore.Interface;

namespace Materal.Workflow
{
    /// <summary>
    /// 动态工作流主机
    /// </summary>
    public interface IDynamicWorkflowHost : IWorkflowHost, IDynamicWorkflowController
    {
    }
}