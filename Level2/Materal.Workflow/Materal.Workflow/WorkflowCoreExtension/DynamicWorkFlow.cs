using Materal.Workflow.StepDatas;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;

namespace Materal.Workflow.WorkflowCoreExtension
{
    /// <summary>
    /// 动态工作流
    /// </summary>
    public partial class DynamicWorkFlow : IWorkflow<Dictionary<string, object?>>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id => "Dynamic";
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version => 1;
        private readonly ILogger<DynamicWorkFlow> _logger;
        private readonly IStepHandlerBus _stepHandlerBus;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DynamicWorkFlow(ILogger<DynamicWorkFlow> logger, IStepHandlerBus stepHandlerBus)
        {
            _logger = logger;
            _stepHandlerBus = stepHandlerBus;
        }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Build(IWorkflowBuilder<Dictionary<string, object?>> builder) => throw new WorkflowException($"请勿主动注册动态工作流");
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="stepData"></param>
        public void Build(IWorkflowBuilder<Dictionary<string, object?>> builder, StartStepData stepData)
        {
            try
            {
                _stepHandlerBus.BuildWorkflow(builder, stepData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "动态工作流运行失败");
            }
        }
    }
}
