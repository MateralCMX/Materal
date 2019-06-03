using System;
using System.ComponentModel.DataAnnotations;
namespace Authority.PresentationModel.ActionAuthority.Request
{
    /// <summary>
    /// 功能权限修改请求模型
    /// </summary>
    public class EditActionAuthorityRequestModel : AddActionAuthorityRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
    }
}
