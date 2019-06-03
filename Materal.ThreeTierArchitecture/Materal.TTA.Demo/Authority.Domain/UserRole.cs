using System;
using Domain;

namespace Authority.Domain
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public sealed class UserRole : BaseEntity<Guid>
    {
        /// <summary>
        /// 所属用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 所属角色唯一标识
        /// </summary>
        public Guid RoleID { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        public Authority.Domain.User User { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public Role Role { get; set; }
    }
}
