using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models
{
    public class QueryFlowRecordDTOModel : QueryFlowRecordModel
    {
        /// <summary>
        /// 流程模版名称
        /// </summary>
        [Contains]
        public string? FlowTemplateName { get; set; } = string.Empty;
        /// <summary>
        /// 步骤名称
        /// </summary>
        [Contains]
        public string? StepName { get; set; } = string.Empty;
        /// <summary>
        /// 节点名称
        /// </summary>
        [Contains]
        public string? NodeName { get; set; } = string.Empty;
    }
}
