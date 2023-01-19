using System;
using Domain;

namespace Authority.Domain
{
    /// <summary>
    /// 角色菜单权限
    /// </summary>
    public sealed class RoleWebMenuAuthority : BaseEntity<Guid>
    {
        /// <summary>
        /// 所属角色唯一标识
        /// </summary>
        public Guid RoleID { get; set; }
        /// <summary>
        /// 所属菜单权限唯一标识
        /// </summary>
        public Guid WebMenuAuthorityID { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 所属菜单权限唯一标识
        /// </summary>
        public WebMenuAuthority WebMenuAuthority { get; set; }
    }
}
