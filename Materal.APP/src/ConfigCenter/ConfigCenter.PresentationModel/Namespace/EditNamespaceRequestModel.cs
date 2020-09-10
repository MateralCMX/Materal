using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.PresentationModel.Namespace
{
    /// <summary>
    /// 修改命名空间请求模型
    /// </summary>
    public class EditNamespaceRequestModel: AddNamespaceRequestModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
