using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow.Steps
{
    /// <summary>
    /// 控制台消息节点
    /// </summary>
    public class ConsoleMessageStep : StepBody
    {
        /// <summary>
        /// 消息
        /// </summary>
        public object Message { get; set; } = string.Empty;
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.ffff}]->{Message}");
            return ExecutionResult.Next();
        }
    }
}
