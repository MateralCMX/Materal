using System;
using System.ComponentModel.DataAnnotations;
namespace Authority.PresentationModel.APIAuthority.Request
{
    /// <summary>
    /// API权限修改请求模型
    /// </summary>
    public class EditAPIAuthorityRequestModel : AddAPIAuthorityRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
    }
}
