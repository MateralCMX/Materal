using System;
using System.ComponentModel.DataAnnotations;
namespace Authority.PresentationModel.Role.Request
{
    /// <summary>
    /// 角色修改请求模型
    /// </summary>
    public class EditRoleRequestModel : AddRoleRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
    }
}
