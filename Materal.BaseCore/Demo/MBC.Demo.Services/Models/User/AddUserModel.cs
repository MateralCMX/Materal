using Materal.BaseCore.Services;
using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.Services.Models.User
{
    /// <summary>
    /// 添加用户模型
    /// </summary>
    public class AddUserModel : IAddServiceModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(100, ErrorMessage = "账号过长")]
        public string Account { get; set; } = string.Empty;
    }
}
