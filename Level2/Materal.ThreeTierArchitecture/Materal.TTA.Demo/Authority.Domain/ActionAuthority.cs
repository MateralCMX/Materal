using System;
using System.Collections.Generic;
using Domain;

namespace Authority.Domain
{
    /// <summary>
    /// 功能权限
    /// </summary>
    public sealed class ActionAuthority : BaseEntity<Guid>
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
        /// 功能组标识
        /// </summary>
        public string ActionGroupCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public ICollection<RoleActionAuthority> RoleActionAuthorities { get; set; }
    }
}
