using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.Domain;
using Materal.Utils.Model;
using System.ComponentModel.DataAnnotations;

namespace RC.ServerCenter.Domain
{
    /// <summary>
    /// 命名空间
    /// </summary>
    [CodeGeneratorPlug("RC.Core.CodeGenerator", "InitProject")]
    public class Namespace : BaseDomain, IDomain
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
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [NotEditGenerator]
        [Required(ErrorMessage = "为空")]
        [Equal]
        public Guid ProjectID { get; set; }
    }
}
