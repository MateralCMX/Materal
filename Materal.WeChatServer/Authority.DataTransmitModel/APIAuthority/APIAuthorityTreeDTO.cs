using System;
using System.Collections.Generic;

namespace Authority.DataTransmitModel.APIAuthority
{
    /// <summary>
    /// API权限树形数据传输模型
    /// </summary>
    public class APIAuthorityTreeDTO
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
