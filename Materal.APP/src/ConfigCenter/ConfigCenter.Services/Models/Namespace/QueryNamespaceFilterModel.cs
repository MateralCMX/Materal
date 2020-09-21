using Materal.Model;
using System;

namespace ConfigCenter.Services.Models.Namespace
{
    public class QueryNamespaceFilterModel : FilterModel
    {
        /// <summary>
        /// 所属项目唯一标识
        /// </summary>
        [Equal]
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Contains]
        public string Description { get; set; }
    }
}
