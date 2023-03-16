using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow.Steps
{
    /// <summary>
    /// 空节点
    /// </summary>
    public class EmptyStep : StepBody
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ExecutionResult Run(IStepExecutionContext context) => ExecutionResult.Next();
    }
}
