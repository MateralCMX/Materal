using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.Domain;
using Materal.Model;
using System.ComponentModel.DataAnnotations;

namespace RC.ServerCenter.Domain
{
    /// <summary>
    /// 项目
    /// </summary>
    public class Project : BaseDomain, IDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [NotEditGenerator]
        [Required(ErrorMessage = "名称为空"), StringLength(50, ErrorMessage = "名称过长")]
        [Contains]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述为空"), StringLength(200, ErrorMessage = "描述过长")]
        public string Description { get; set; } = string.Empty;
    }
}
