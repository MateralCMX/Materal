using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.Application
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Password { get; set; } = string.Empty;
    }
}
