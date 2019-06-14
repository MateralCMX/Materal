using Common;
using Domain;
using System;
using System.Collections.Generic;
using Common.Tree;

namespace Authority.Domain
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    public sealed class WebMenuAuthority : BaseEntity<Guid>, ITreeDomain<Guid>
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
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
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
        public WebMenuAuthority Parent { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public ICollection<WebMenuAuthority> Child { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public ICollection<RoleWebMenuAuthority> RoleWebMenuAuthorities { get; set; }
    }
}
