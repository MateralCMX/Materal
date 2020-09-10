using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.Environment.Services.Models.ConfigurationItem
{
    public class AddConfigurationItemModel
    {
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        [Required(ErrorMessage = "项目唯一标识不能为空")]
        public Guid ProjectID { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Required(ErrorMessage = "命名空间唯一标识不能为空")]
        public Guid NamespaceID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空"), StringLength(100, ErrorMessage = "项目名称长度不能超过100")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        [Required(ErrorMessage = "命名空间名称不能为空"), StringLength(100, ErrorMessage = "命名空间名称长度不能超过100")]
        public string NamespaceName { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "键不能为空"), StringLength(100, ErrorMessage = "键长度不能超过100")]
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述不能为空")]
        public string Description { get; set; }
    }
}
