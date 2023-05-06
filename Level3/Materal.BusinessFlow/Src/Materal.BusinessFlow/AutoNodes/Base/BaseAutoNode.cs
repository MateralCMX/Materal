using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.AutoNodes;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow.AutoNodes.Base
{
    public abstract class BaseAutoNode : IAutoNode
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly IBusinessFlowUnitOfWork UnitOfWork;
        protected readonly IDataModelRepository DataModelRepository;
        protected readonly IDataModelFieldRepository DataModelFieldRepository;
        protected readonly IFlowTemplateRepository FlowTemplateRepository;
        protected readonly IStepRepository StepRepository;
        protected readonly INodeRepository NodeRepository;
        protected readonly IFlowRepository FlowRepository;
        protected readonly IFlowRecordRepository FlowRecordRepository;
        protected BaseAutoNode(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            UnitOfWork = ServiceProvider.GetService<IBusinessFlowUnitOfWork>() ?? throw new BusinessFlowException("获取工作单元失败");
            DataModelRepository = UnitOfWork.GetRepository<IDataModelRepository>();
            DataModelFieldRepository = UnitOfWork.GetRepository<IDataModelFieldRepository>();
            FlowTemplateRepository = UnitOfWork.GetRepository<IFlowTemplateRepository>();
            StepRepository = UnitOfWork.GetRepository<IStepRepository>();
            NodeRepository = UnitOfWork.GetRepository<INodeRepository>();
            FlowRepository = UnitOfWork.GetRepository<IFlowRepository>();
            FlowRecordRepository = UnitOfWork.GetRepository<IFlowRecordRepository>();
        }
        public virtual async Task ExcuteAsync(Guid flowTemplateID, Guid flowRecordID)
        {
            FlowRecord flowRecord = await FlowRecordRepository.FirstAsync(flowTemplateID, flowRecordID);
            Node node = await NodeRepository.FirstAsync(flowRecord.NodeID);
            Step step = await StepRepository.FirstAsync(node.StepID);
            FlowTemplate flowTemplate = await FlowTemplateRepository.FirstAsync(flowTemplateID);
            DataModel dataModel = await DataModelRepository.FirstAsync(flowTemplate.DataModelID);
            List<DataModelField> dataModelFields = await DataModelFieldRepository.FindAsync(m => m.DataModelID == flowTemplate.DataModelID);
            Dictionary<string, object?> flowData = await FlowRepository.GetDataAsync(flowTemplateID, flowRecord.FlowID, dataModelFields);
            AutoNodeModel autoNodeModel = new(flowTemplate, step, node, flowRecord, dataModel, dataModelFields, flowData);
            await ExcuteAsync(autoNodeModel);
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="autoNodeModel"></param>
        /// <returns></returns>
        public virtual Task ExcuteAsync(AutoNodeModel autoNodeModel)
        {
            Excute(autoNodeModel);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="autoNodeModel"></param>
        /// <returns></returns>
        public virtual void Excute(AutoNodeModel autoNodeModel) { }
    }
}
