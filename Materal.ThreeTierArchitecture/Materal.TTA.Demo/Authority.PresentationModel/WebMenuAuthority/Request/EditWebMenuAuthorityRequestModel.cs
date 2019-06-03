using System;
using System.ComponentModel.DataAnnotations;
namespace Authority.PresentationModel.WebMenuAuthority.Request
{
    /// <summary>
    /// 网页菜单权限修改请求模型
    /// </summary>
    public class EditWebMenuAuthorityRequestModel : AddWebMenuAuthorityRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
    }
}
