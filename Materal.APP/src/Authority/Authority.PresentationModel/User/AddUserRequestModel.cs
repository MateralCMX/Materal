using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.User
{
    /// <summary>
    /// 添加用户请求模型
    /// </summary>
    public class AddUserRequestModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空"), StringLength(100, ErrorMessage = "账号长度不能超过100")]
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空"), StringLength(100, ErrorMessage = "姓名长度不能超过100")]
        public string Name { get; set; }
    }
}
