using System.Linq.Expressions;
using WorkflowCore.Primitives;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 计划节点处理器
    /// </summary>
    public class ScheduleStepHandler : BaseStepHandler<ScheduleStepData>, IStepHandler<ScheduleStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, ScheduleStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            if(stepData.StepData != null)
            {
                Expression<Func<Dictionary<string, object?>, TimeSpan>> expression = m => stepData.Delay;
                stepBuilder = InvokeMethodByMethodName(stepBuilder, nameof(Schedule), new object?[] { expression });
                stepBuilder = Do(stepBuilder, stepData.StepData, stepHandlerBus);
            }
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
    }
}
