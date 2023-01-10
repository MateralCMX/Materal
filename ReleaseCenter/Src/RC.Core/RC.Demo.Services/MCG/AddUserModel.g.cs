#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Services;

namespace RC.Demo.Services.Models.User
{
    /// <summary>
    ///  用户添加模型
    /// </summary>
    public partial class AddUserModel : IAddServiceModel
    {
        /// <summary>
        ///  姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
        public string Name { get; set; }  = string.Empty;
        /// <summary>
        ///  账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(50, ErrorMessage = "账号过长")]
        public string Account { get; set; }  = string.Empty;
        /// <summary>
        ///  密码
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(32, ErrorMessage = "密码过长")]
        public string Password { get; set; }  = string.Empty;
    }
}
