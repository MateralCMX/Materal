using System;
using Domain;

namespace Authority.Domain
{
    /// <summary>
    /// 角色API权限
    /// </summary>
    public sealed class RoleAPIAuthority : BaseEntity<Guid>
    {
        /// <summary>
        /// 所属角色唯一标识
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// 所属API权限唯一标识
        /// </summary>
        public Guid APIAuthorityID { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 所属API权限
        /// </summary>
        public APIAuthority APIAuthority { get; set; }
    }
}
