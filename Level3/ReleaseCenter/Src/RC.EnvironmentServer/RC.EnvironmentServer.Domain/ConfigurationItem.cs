using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.Domain;
using Materal.Model;
using System.ComponentModel.DataAnnotations;

namespace RC.EnvironmentServer.Domain
{
    /// <summary>
    /// 配置项
    /// </summary>
    [Cache]
    [CodeGeneratorPlug("RC.Core.CodeGenerator", "InitProject")]
    public class ConfigurationItem : BaseDomain, IDomain
    {
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        [NotEditGenerator]
        [Required(ErrorMessage = "项目唯一标识为空")]
        [Equal]
        public Guid ProjectID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [NotAddGenerator, NotEditGenerator]
        [Required(ErrorMessage = "项目名称为空"), StringLength(50, ErrorMessage = "项目名称过长")]
        [Equal]
        public string ProjectName { get; set; } = string.Empty;
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [NotEditGenerator]
        [Required(ErrorMessage = "命名空间唯一标识为空")]
        [Equal]
        public Guid NamespaceID { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        [NotAddGenerator, NotEditGenerator]
        [Required(ErrorMessage = "命名空间名称为空"), StringLength(50, ErrorMessage = "命名空间名称过长")]
        [Equal]
        public string NamespaceName { get; set; } = string.Empty;
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "键为空"), StringLength(50, ErrorMessage = "键过长")]
        [Equal]
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值为空")]
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述为空"), StringLength(200, ErrorMessage = "描述过长")]
        public string Description { get; set; } = string.Empty;
    }
}
