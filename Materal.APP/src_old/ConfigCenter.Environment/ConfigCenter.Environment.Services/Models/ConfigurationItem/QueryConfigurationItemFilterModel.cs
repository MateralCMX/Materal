using System;
using Materal.Model;

namespace ConfigCenter.Environment.Services.Models.ConfigurationItem
{
    public class QueryConfigurationItemFilterModel : FilterModel
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Equal]
        public Guid? NamespaceID { get; set; }
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        [Equal]
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [Equal]
        public string Key { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Contains]
        public string Description { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Equal]
        public string ProjectName { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string[] NamespaceNames { get; set; }
    }
}
