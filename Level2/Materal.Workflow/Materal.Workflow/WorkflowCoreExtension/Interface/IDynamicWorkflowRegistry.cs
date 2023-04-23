using Materal.Workflow.StepDatas;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow.WorkflowCoreExtension.Interface
{
    /// <summary>
    /// 动态工作流注登记处
    /// </summary>
    public interface IDynamicWorkflowRegistry : IWorkflowRegistry
    {
        /// <summary>
        /// 获得动态工作流定义
        /// </summary>
        /// <param name="stepData"></param>
        /// <returns></returns>
        WorkflowDefinition GetDynamicWorkflowDefinition(StartStepData stepData);
    }
}
