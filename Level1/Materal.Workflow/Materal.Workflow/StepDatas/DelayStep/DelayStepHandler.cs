using System.Linq.Expressions;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 延时节点处理器
    /// </summary>
    public class DelayStepHandler : BaseStepHandler<DelayStepData>, IStepHandler<DelayStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, DelayStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Expression<Func<Dictionary<string, object?>, TimeSpan>> expression = m => stepData.Delay;
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "Delay", new object[] { expression });
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
    }
}
