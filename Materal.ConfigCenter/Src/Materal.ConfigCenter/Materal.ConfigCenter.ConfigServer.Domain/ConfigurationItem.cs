using System;

namespace Materal.ConfigCenter.ConfigServer.Domain
{
    public class ConfigurationItem : BaseDomain
    {
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        public Guid ProjectID { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid NamespaceID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string NamespaceName { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
