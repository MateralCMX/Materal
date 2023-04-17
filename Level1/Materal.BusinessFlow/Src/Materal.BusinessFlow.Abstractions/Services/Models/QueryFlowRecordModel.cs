using Materal.BusinessFlow.Abstractions.Enums;
using Materal.Utils.Model;

namespace Materal.BusinessFlow.Abstractions.Services.Models
{
    public class QueryFlowRecordModel : PageRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Equal]
        public Guid? ID { get; set; }
        /// <summary>
        /// 流程唯一标识
        /// </summary>
        [Equal]
        public Guid? FlowID { get; set; }
        /// <summary>
        /// 流程模版唯一标识
        /// </summary>
        [Equal]
        public Guid FlowTemplateID { get; set; }
        /// <summary>
        /// 步骤唯一标识
        /// </summary>
        [Equal]
        public Guid? StepID { get; set; }
        /// <summary>
        /// 节点唯一标识
        /// </summary>
        [Equal]
        public Guid? NodeID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Equal]
        public FlowRecordStateEnum? State { get; set; }
        /// <summary>
        /// 负责用户
        /// </summary>
        [Equal]
        public Guid? UserID { get; set; }
        /// <summary>
        /// 处理用户
        /// </summary>
        [Equal]
        public Guid? OperationUserID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Equal]
        public int? SortIndex { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [GreaterThanOrEqual("CreateTime")]
        public DateTime? MinCreateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [LessThanOrEqual("CreateTime")]
        public DateTime? MaxCreateTime { get; set; }
    }
}
