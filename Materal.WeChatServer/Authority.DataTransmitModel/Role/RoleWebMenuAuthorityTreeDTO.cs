using System;
using System.Collections.Generic;
using System.Text;

namespace Authority.DataTransmitModel.Role
{
    /// <summary>
    /// 角色网页菜单权限树数据传输模型
    /// </summary>
    public class RoleWebMenuAuthorityTreeDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public List<RoleWebMenuAuthorityTreeDTO> Child { get; set; }
        /// <summary>
        /// 拥有标识
        /// </summary>
        public bool Owned { get; set; }
    }
}
