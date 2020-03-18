using System;

namespace Materal.ConfigCenter.ConfigServer.Domain
{
    public class ConfigurationItem : BaseDomain
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid ProjectID { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid NamespaceID { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
