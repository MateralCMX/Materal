using Materal.BusinessFlow.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    public class FlowRecord : BaseDomain
    {
        /// <summary>
        /// 流程唯一标识
        /// </summary>
        [Required]
        public Guid FlowID { get; set; }
        /// <summary>
        /// 步骤唯一标识
        /// </summary>
        [Required]
        public Guid StepID { get; set; }
        /// <summary>
        /// 节点唯一标识
        /// </summary>
        [Required]
        public Guid NodeID { get; set; }
        /// <summary>
        /// 可操作用户
        /// </summary>
        public Guid? UserID { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        public Guid? OperationUserID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public FlowRecordStateEnum State { get; set; } = FlowRecordStateEnum.Wait;
        /// <summary>
        /// 位序
        /// </summary>
        [Required]
        public int SortIndex { get; set; }
        /// <summary>
        /// 节点处理类型
        /// </summary>
        [Required]
        public NodeHandleTypeEnum NodeHandleType { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string? ResultMessage { get; set; }
    }
}
