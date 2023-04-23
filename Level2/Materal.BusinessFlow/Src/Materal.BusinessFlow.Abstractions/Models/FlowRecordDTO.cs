using Materal.BusinessFlow.Abstractions.Domain;

namespace Materal.BusinessFlow.Abstractions.Models
{
    public class FlowRecordDTO : FlowRecord
    {
        /// <summary>
        /// 流程模版唯一标识
        /// </summary>
        public Guid FlowTemplateID { get; set; }
        /// <summary>
        /// 流程模版名称
        /// </summary>
        public string FlowTemplateName { get; set; } = string.Empty;
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; } = string.Empty;
        /// <summary>
        /// 状态文本
        /// </summary>
        public string StateText => State.GetDescription();
    }
}
