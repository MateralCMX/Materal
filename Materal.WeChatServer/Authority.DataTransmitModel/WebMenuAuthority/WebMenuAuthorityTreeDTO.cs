using Common;
using System;
using System.Collections.Generic;
using Common.Tree;

namespace Authority.DataTransmitModel.WebMenuAuthority
{
    /// <summary>
    /// 网页菜单权限树形数据传输模型
    /// </summary>
    public class WebMenuAuthorityTreeDTO : ITreeModel<WebMenuAuthorityTreeDTO, Guid>
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
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public ICollection<WebMenuAuthorityTreeDTO> Child { get; set; }
    }
}
