using Common;
using Domain;
using System;
using System.Collections.Generic;
using Common.Tree;

namespace Authority.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    public sealed class Role : BaseEntity<Guid>, ITreeDomain<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public Role Parent { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public ICollection<Role> Child { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
        /// <summary>
        /// API权限
        /// </summary>
        public ICollection<RoleAPIAuthority> RoleAPIAuthorities { get; set; }
        /// <summary>
        /// 菜单权限
        /// </summary>
        public ICollection<RoleWebMenuAuthority> RoleWebMenuAuthorities { get; set; }
        /// <summary>
        /// 功能权限
        /// </summary>
        public ICollection<RoleActionAuthority> RoleActionAuthorities { get; set; }
    }
}
