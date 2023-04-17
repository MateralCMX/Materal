using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    /// <summary>
    /// 节点
    /// </summary>
    public class Node : BaseDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 步骤唯一标识
        /// </summary>
        [Required]
        public Guid StepID { get; set; }
        /// <summary>
        /// 处理类型
        /// </summary>
        [Required]
        public NodeHandleTypeEnum HandleType { get; set; }
        /// <summary>
        /// 执行条件
        /// </summary>
        public string? RunConditionExpression { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// 处理数据
        /// </summary>
        public string? HandleData { get; set; }
    }
}
