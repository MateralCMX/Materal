using System.ComponentModel.DataAnnotations;

namespace Materal.ApplicationUpdate.WebAPI.Models.User
{
    public class UserLoginRequestModel
    {
        /// <summary>
        /// 账户
        /// </summary>
        [Required(ErrorMessage = "账户不能为空"), StringLength(32, ErrorMessage = "账户长度不能超过32")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空"), StringLength(32, ErrorMessage = "密码长度不能超过32")]
        public string Password { get; set; }
    }
}
