#nullable enable
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Services;

namespace RC.Authority.Services.Models.User
{
    /// <summary>
    /// 用户添加模型
    /// </summary>
    public partial class AddUserModel : IAddServiceModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名最多100个字符")]
        public string Name { get; set; }  = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(100, ErrorMessage = "账号最多100个字符")]
        public string Account { get; set; }  = string.Empty;
    }
}
