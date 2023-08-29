using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow.StepBodys
{
    /// <summary>
    /// 抛错节点
    /// </summary>
    public class ThrowExceptionStepBody : StepBody
    {
        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; } = new WorkflowException("工作流异常");
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ExecutionResult Run(IStepExecutionContext context) => throw Exception;
    }
}
