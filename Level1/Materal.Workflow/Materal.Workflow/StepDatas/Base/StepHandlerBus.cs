using WorkflowCore.Interface;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 节点处理器总线
    /// </summary>
    public class StepHandlerBus : IStepHandlerBus
    {
        /// <summary>
        /// 工作流构建器
        /// </summary>
        public IWorkflowBuilder<Dictionary<string, object?>>? WorkflowBuilder { get; private set; }
        private readonly IServiceProvider _services;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="services"></param>
        public StepHandlerBus(IServiceProvider services)
        {
            _services = services;
        }
        /// <summary>
        /// 构建工作流
        /// </summary>
        /// <param name="workflowBuilder"></param>
        /// <param name="stepData"></param>
        /// <returns></returns>
        public object BuildWorkflow(IWorkflowBuilder<Dictionary<string, object?>> workflowBuilder, IStepData stepData)
        {
            WorkflowBuilder = workflowBuilder;
            return BuildStep(workflowBuilder, stepData);
        }
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <returns></returns>
        public object BuildStep(object stepBuilder, IStepData stepData)
        {
            IStepHandler? stepHandler = StepHandlerBusHelper.GetStepHandler(_services, stepData.GetType());
            if(stepHandler != null)
            {
                stepBuilder = stepHandler.Build(stepBuilder, stepData, this);
            }
            return stepBuilder;
        }
    }
}
