using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.DTO
{
    public class FlowTemplateDTO : IDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 数据模型唯一标识
        /// </summary>
        [Required]
        public Guid DataModelID { get; set; }
        /// <summary>
        /// 数据模型名称
        /// </summary>
        [Required]
        public string DataModelName { get; set; } = string.Empty;
    }
}
