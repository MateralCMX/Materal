using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.Namespace
{
    /// <summary>
    /// 修改命名空间请求模型
    /// </summary>
    public class EditNamespaceRequestModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
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
    }
}
