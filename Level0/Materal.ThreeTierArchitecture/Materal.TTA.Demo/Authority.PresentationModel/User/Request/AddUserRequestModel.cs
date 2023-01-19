using System;
using System.ComponentModel.DataAnnotations;
using Authority.Common;

namespace Authority.PresentationModel.User.Request
{
    /// <summary>
    /// 用户添加请求模型
    /// </summary>
    public class AddUserRequestModel
    {
        /// <summary>
        /// 账户
        /// </summary>
        [Required(ErrorMessage = "账户不可以为空"), StringLength(100, ErrorMessage = "账户长度不能超过100")]
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不可以为空"), StringLength(100, ErrorMessage = "姓名长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别不可以为空")]
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(15, ErrorMessage = "手机号码长度不能超过15")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 角色ID组
        /// </summary>
        public Guid[] RoleIDs { get; set; }
    }
}
