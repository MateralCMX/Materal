using System;
using Materal.APP.Core.Models;

namespace ConfigCenter.Domain
{
    /// <summary>
    /// 命名空间
    /// </summary>
    public class Namespace : BaseDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid ProjectID { get; set; }
    }
}
