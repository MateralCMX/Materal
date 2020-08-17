using System.ComponentModel.DataAnnotations;

namespace Materal.ConDep.Center.PresentationModel.User
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public class LoginRequestModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
