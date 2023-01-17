#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.DataTransmitModel;

namespace RC.EnvironmentServer.DataTransmitModel.ConfigurationItem
{
    /// <summary>
    /// 配置项列表数据传输模型
    /// </summary>
    public partial class ConfigurationItemListDTO: IListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间为空")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        [Required(ErrorMessage = "项目唯一标识为空")]
        public Guid ProjectID { get; set; } 
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称为空"), StringLength(50, ErrorMessage = "项目名称过长")]
        public string ProjectName { get; set; }  = string.Empty;
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Required(ErrorMessage = "命名空间唯一标识为空")]
        public Guid NamespaceID { get; set; } 
        /// <summary>
        /// 命名空间名称
        /// </summary>
        [Required(ErrorMessage = "命名空间名称为空"), StringLength(50, ErrorMessage = "命名空间名称过长")]
        public string NamespaceName { get; set; }  = string.Empty;
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "键为空"), StringLength(50, ErrorMessage = "键过长")]
        public string Key { get; set; }  = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值为空")]
        public string Value { get; set; }  = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述为空"), StringLength(200, ErrorMessage = "描述过长")]
        public string Description { get; set; }  = string.Empty;
    }
}
