using Materal.BusinessFlow.Abstractions.Domain;

namespace Materal.BusinessFlow.AutoNodes.Base
{
    /// <summary>
    /// 自动节点模型
    /// </summary>
    public class AutoNodeModel
    {
        /// <summary>
        /// 流程模版
        /// </summary>
        public FlowTemplate FlowTemplate { get; }
        /// <summary>
        /// 步骤
        /// </summary>
        public Step Step { get; }
        /// <summary>
        /// 节点
        /// </summary>
        public Node Node { get; }
        /// <summary>
        /// 流程记录
        /// </summary>
        public FlowRecord FlowRecord { get; }
        /// <summary>
        /// 数据模型
        /// </summary>
        public DataModel DataModel { get; }
        /// <summary>
        /// 数据模型字段
        /// </summary>
        public List<DataModelField> DataModelFields { get; }
        /// <summary>
        /// 流程数据
        /// </summary>
        public Dictionary<string, object?> FlowData { get; }
        public AutoNodeModel(FlowTemplate flowTemplate, Step step, Node node, FlowRecord flowRecord, DataModel dataModel, List<DataModelField> dataModelFields, Dictionary<string, object?> flowData)
        {
            FlowTemplate = flowTemplate;
            Step = step;
            Node = node;
            FlowRecord = flowRecord;
            DataModel = dataModel;
            DataModelFields = dataModelFields;
            FlowData = flowData;
        }
    }
}
