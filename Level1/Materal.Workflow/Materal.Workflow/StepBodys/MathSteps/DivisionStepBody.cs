using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow.StepBodys
{
    /// <summary>
    /// 除法
    /// </summary>
    public class DivisionStepBody : StepBody
    {
        /// <summary>
        /// 左边的数字
        /// </summary>
        public decimal LeftNumber { get; set; } = 0;
        /// <summary>
        /// 右边的数字
        /// </summary>
        public decimal RightNumber { get; set; } = 1;
        /// <summary>
        /// 结果
        /// </summary>
        public decimal Result { get; set; } = 0;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Result = LeftNumber / RightNumber;
            return ExecutionResult.Next();
        }
    }
}
