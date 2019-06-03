using System;
using Domain;

namespace Authority.Domain
{
    /// <summary>
    /// 角色功能权限
    /// </summary>
    public sealed class RoleActionAuthority : BaseEntity<Guid>
    {
        /// <summary>
        /// 所属角色唯一标识
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// 所属功能权限唯一标识
        /// </summary>
        public Guid ActionAuthorityID { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 所属功能权限
        /// </summary>
        public ActionAuthority ActionAuthority { get; set; }
    }
}
