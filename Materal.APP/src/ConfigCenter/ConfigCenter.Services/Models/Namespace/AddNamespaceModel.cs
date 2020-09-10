using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.Services.Models.Namespace
{
    public class AddNamespaceModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空"), StringLength(100, ErrorMessage = "名称长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述不能为空")]
        public string Description { get; set; }
        /// <summary>
        /// 所属项目唯一标识
        /// </summary>
        [Required(ErrorMessage = "所属项目唯一标识不能为空")]
        public Guid ProjectID { get; set; }
    }
}
