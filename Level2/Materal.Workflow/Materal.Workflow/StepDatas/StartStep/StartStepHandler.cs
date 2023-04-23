using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 开始节点处理器
    /// </summary>
    public class StartStepHandler : BaseStepHandler<StartStepData>, IStepHandler<StartStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        public override object BuildStep(object stepBuilder, StartStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            if (stepBuilder is not IWorkflowBuilder workflowBuilder) throw new WorkflowException("开始节点不能作为子节点出现");
            stepBuilder = Start(workflowBuilder);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="workflowBuilder"></param>
        /// <returns></returns>
        private object Start(IWorkflowBuilder workflowBuilder)
        {
            static ExecutionResult step(IStepExecutionContext _) => ExecutionResult.Next();
            WorkflowStepInline workflowStep = new()
            {
                Name = "StartStep",
                Body = step
            };
            workflowBuilder.AddStep(workflowStep);
            object newStepBuilder = InstantiationStepBuilder(workflowBuilder, workflowStep, typeof(InlineStepBody));
            return newStepBuilder;
        }
    }
}
