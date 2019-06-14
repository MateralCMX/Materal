using Common;
using Domain;
using System;
using System.Collections.Generic;
using Common.Tree;

namespace Authority.Domain
{
    /// <summary>
    /// API权限
    /// </summary>
    public class APIAuthority : BaseEntity<Guid>, ITreeDomain<Guid>
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public APIAuthority Parent { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public ICollection<APIAuthority> Child { get; set; }
        /// <summary>
        /// 角色API权限
        /// </summary>
        public ICollection<RoleAPIAuthority> RoleAPIAuthorities { get; set; }
    }
}
