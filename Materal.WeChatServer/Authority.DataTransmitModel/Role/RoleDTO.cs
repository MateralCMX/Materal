using System;
using System.Collections.Generic;
using Authority.DataTransmitModel.ActionAuthority;

namespace Authority.DataTransmitModel.Role
{
    /// <summary>
    /// 角色数据传输模型
    /// </summary>
    public class RoleDTO 
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 功能权限列表
        /// </summary>
        public ICollection<ActionAuthorityListDTO> ActionAuthorityList { get; set; }
        /// <summary>
        /// API权限列表
        /// </summary>
        public ICollection<RoleAPIAuthorityTreeDTO> APIAuthorityTreeList { get; set; }
        /// <summary>
        /// 网页菜单权限列表
        /// </summary>
        public ICollection<RoleWebMenuAuthorityTreeDTO> WebMenuAuthorityTreeList { get; set; }
    }
}
