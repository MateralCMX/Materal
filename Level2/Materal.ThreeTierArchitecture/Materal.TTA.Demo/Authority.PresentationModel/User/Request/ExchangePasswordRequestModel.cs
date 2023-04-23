using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.User.Request
{
    /// <summary>
    /// 更改密码请求模型
    /// </summary>
    public class ExchangePasswordRequestModel
    {
        /// <summary>
        /// Token
        /// </summary>
        [Required(ErrorMessage = "Token不可以为空")]
        public string Token { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "旧密码不可以为空")]
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不可以为空")]
        public string NewPassword { get; set; }
    }
}
