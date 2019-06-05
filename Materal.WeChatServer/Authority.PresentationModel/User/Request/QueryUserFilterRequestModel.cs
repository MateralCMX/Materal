using System;
using System.ComponentModel.DataAnnotations;
using Authority.Common;
using Materal.Common;
namespace Authority.PresentationModel.User.Request
{
    /// <summary>
    /// 用户查询请求模型
    /// </summary>
    public class QueryUserFilterRequestModel : PageRequestModel
    {
        /// <summary>
        /// 账户
        /// </summary>
        [StringLength(100, ErrorMessage = "账户长度不能超过100")]
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(100, ErrorMessage = "姓名长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum? Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(15, ErrorMessage = "手机号码长度不能超过15")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
}
