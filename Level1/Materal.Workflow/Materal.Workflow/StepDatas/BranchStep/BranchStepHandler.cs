using System.Linq.Expressions;
using WorkflowCore.Interface;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 分支节点处理器
    /// </summary>
    public class BranchStepHandler : BaseStepHandler<BranchStepData>, IStepHandler<BranchStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, BranchStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            stepBuilder = Branch(stepBuilder, stepData.StepDatas, stepHandlerBus);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
        /// <summary>
        /// 分支
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepDatas"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        private object Branch(object stepBuilder, List<IStepData> stepDatas, IStepHandlerBus stepHandlerBus)
        {
            if (stepDatas.Count <= 0) return stepBuilder;
            foreach (IStepData stepData in stepDatas)
            {
                object? newStepBuilder = GetNewBranchStepBuilder(stepData, stepHandlerBus);
                if (newStepBuilder == null) break;
                stepBuilder = Branch(stepBuilder, stepData, newStepBuilder);
            }
            return stepBuilder;
        }
        /// <summary>
        /// 分支
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="newStepBuilder"></param>
        /// <returns></returns>
        private object Branch(object stepBuilder, IStepData stepData, object newStepBuilder)
        {
            Type newStepBuilderType = newStepBuilder.GetType();
            if (newStepBuilderType.GenericTypeArguments.Length != 2) throw new WorkflowException("节点构建器类型错误");
            Type stepType = newStepBuilder.GetType().GenericTypeArguments[1];
            Expression<Func< Dictionary<string, object?>, object, bool>> decisionExpression = (m, n) => true;
            if (stepData is IfStepData ifStepData)
            {
                decisionExpression = GetDecisionConditionExpression<object>(ifStepData.Conditions, stepData);
            }
            InvokeGenericMethodByMethodName(stepBuilder, nameof(Branch), new Type[] { stepType }, new object?[] { decisionExpression, newStepBuilder });
            return stepBuilder;
        }
        /// <summary>
        /// 获得新的分支节点构建器
        /// </summary>
        /// <param name="branchData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        private object? GetNewBranchStepBuilder(IStepData branchData, IStepHandlerBus stepHandlerBus)
        {
            if (stepHandlerBus.WorkflowBuilder == null || stepHandlerBus.WorkflowBuilder is not IWorkflowBuilder<Dictionary<string, object?>> workflowBuilder) return null;
            IWorkflowBuilder<Dictionary<string, object?>> newWorkflowBuilder = workflowBuilder.CreateBranch();
            object result = BuildBranchStep(newWorkflowBuilder, branchData, stepHandlerBus);
            return result;
        }
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        private object BuildBranchStep(object stepBuilder, IStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            if (stepData is IfStepData ifStepData)
            {
                stepData = ifStepData.StepData;
            }
            if (stepData is not StartStepData)
            {
                stepData = new StartStepData()
                {
                    Next = stepData
                };
            }
            stepBuilder = stepHandlerBus.BuildStep(stepBuilder, stepData);
            return stepBuilder;
        }
    }
}
