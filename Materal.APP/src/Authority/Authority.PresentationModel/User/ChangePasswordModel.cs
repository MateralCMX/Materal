using System;
using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.User
{
    /// <summary>
    /// 更改密码请求模型
    /// </summary>
    public class ChangePasswordRequestModel
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "旧密码不能为空")]
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        public string NewPassword { get; set; }
    }
}
