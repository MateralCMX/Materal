using System.ComponentModel.DataAnnotations;

namespace Materal.ConfigCenter.ProtalServer.PresentationModel.User
{
    public class AddUserModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空"), StringLength(100, ErrorMessage = "账号长度不能超过100")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
