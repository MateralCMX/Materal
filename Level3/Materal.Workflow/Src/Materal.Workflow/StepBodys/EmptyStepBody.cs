using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow.StepBodys
{
    /// <summary>
    /// 空节点
    /// </summary>
    public class EmptyStepBody : StepBody
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ExecutionResult Run(IStepExecutionContext context) => ExecutionResult.Next();
    }
}
