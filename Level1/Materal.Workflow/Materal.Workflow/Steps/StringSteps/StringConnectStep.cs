using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow.Steps
{
    /// <summary>
    /// 字符串连接
    /// </summary>
    public class StringConnectStep : StepBody
    {
        /// <summary>
        /// 左边的字符串
        /// </summary>
        public string LeftString { get; set; } = string.Empty;
        /// <summary>
        /// 右边的字符串
        /// </summary>
        public string RightString { get; set; } = string.Empty;
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; } = string.Empty;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Result = LeftString + RightString;
            return ExecutionResult.Next();
        }
    }
}
