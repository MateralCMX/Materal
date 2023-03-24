using Materal.Workflow.StepBodys;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 普通节点处理器
    /// </summary>
    public class ThenStepHandler : BaseStepHandler<ThenStepData>, IStepHandler<ThenStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        public override object BuildStep(object stepBuilder, ThenStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Type? stepType = stepData.StepBodyType.GetTypeByTypeName(Array.Empty<object>());
            if (stepType == null) throw new WorkflowException($"找不到节点类型{stepData.StepBodyType}");
            stepBuilder = Then(stepBuilder, stepType);
            stepBuilder = Error(stepBuilder, stepData, stepHandlerBus);
            if (stepData.CompensateStep != null)
            {
                stepBuilder = CompensateWith(stepBuilder, stepData.CompensateStep, stepHandlerBus);
            }
            stepBuilder = HandlerThenStep(stepBuilder, stepData, stepHandlerBus);
            return stepBuilder;
        }
        /// <summary>
        /// 普通节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepType"></param>
        /// <returns></returns>
        private object Then(object stepBuilder, Type stepType)
        {
            WorkflowStep newStep = InstantiationWorkflowStep(stepType);
            IWorkflowBuilder<Dictionary<string, object?>> workflowBuilder = GetWorkflowBuilder(stepBuilder);
            workflowBuilder.AddStep(newStep);
            object newStepBuilder = InstantiationStepBuilder(workflowBuilder, newStep, stepType);
            newStep.Name ??= stepType.Name;
            WorkflowStep step = GetWorkflowStep(stepBuilder);
            step.Outcomes.Add(new ValueOutcome { NextStep = newStep.Id });
            return newStepBuilder;
        }
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        protected object Error(object stepBuilder, ThenStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            if (stepData.Error == null) return stepBuilder;
            WorkflowErrorHandling workflowErrorHandling = stepData.Error.HandlerType switch
            {
                ErrorHandlerTypeEnum.Retry => WorkflowErrorHandling.Retry,
                ErrorHandlerTypeEnum.Next => WorkflowErrorHandling.Compensate,
                _ => WorkflowErrorHandling.Terminate,
            };
            if (workflowErrorHandling == WorkflowErrorHandling.Retry && stepData.Error.RetryInterval == null) throw new WorkflowException("请指定重试间隔");
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "OnError", new object?[] { workflowErrorHandling, stepData.Error.RetryInterval }, new Type[] { typeof(WorkflowErrorHandling), typeof(TimeSpan?) });
            if (workflowErrorHandling == WorkflowErrorHandling.Compensate)
            {
                stepData.CompensateStep ??= new ThenStepData<EmptyStepBody>();
            }
            return stepBuilder;
        }
    }
}
