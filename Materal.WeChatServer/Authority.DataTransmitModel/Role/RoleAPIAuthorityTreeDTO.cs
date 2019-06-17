using Common.Tree;
using System;
using System.Collections.Generic;

namespace Authority.DataTransmitModel.Role
{
    /// <summary>
    /// 角色权限树数据传输模型
    /// </summary>
    public class RoleAPIAuthorityTreeDTO : ITreeModel<RoleAPIAuthorityTreeDTO, Guid>
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
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public ICollection<RoleAPIAuthorityTreeDTO> Child { get; set; }
        /// <summary>
        /// 拥有标识
        /// </summary>
        public bool Owned { get; set; }
    }
}
