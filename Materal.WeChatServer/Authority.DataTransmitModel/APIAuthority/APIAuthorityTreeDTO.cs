using System;
using System.Collections.Generic;
using Common;
using Common.Tree;

namespace Authority.DataTransmitModel.APIAuthority
{
    /// <summary>
    /// API权限树形数据传输模型
    /// </summary>
    public class APIAuthorityTreeDTO : ITreeModel<APIAuthorityTreeDTO, Guid>
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
        /// 子级
        /// </summary>
        public ICollection<APIAuthorityTreeDTO> Child { get; set; }
    }
}
