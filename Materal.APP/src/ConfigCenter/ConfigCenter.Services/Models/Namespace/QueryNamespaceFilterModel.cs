using System;
using System.ComponentModel.DataAnnotations;
using Materal.Model;

namespace ConfigCenter.Services.Models.Namespace
{
    public class QueryNamespaceFilterModel : FilterModel
    {
        /// <summary>
        /// 所属项目唯一标识
        /// </summary>
        [Required(ErrorMessage = "所属项目唯一标识不能为空"), Equal]
        public Guid ProjectID { get; set; }
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
