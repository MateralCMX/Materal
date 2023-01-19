using System;
using System.Collections.Generic;
using Authority.Common;
using Domain;

namespace Authority.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public sealed class User : BaseEntity<Guid>
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDelete { get; set; }


        /// <summary>
        /// 用户角色
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
