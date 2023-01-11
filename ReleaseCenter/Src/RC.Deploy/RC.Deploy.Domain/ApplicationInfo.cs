using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.Domain;
using Materal.Model;
using RC.Deploy.Enums;
using System.ComponentModel.DataAnnotations;

namespace RC.Deploy.Domain
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class ApplicationInfo : BaseDomain, IDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [NotEditGenerator]
        [Required(ErrorMessage = "名称为空")]
        [Contains]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 根路径
        /// </summary>
        [NotEditGenerator]
        [Required(ErrorMessage = "根路径为空")]
        [Contains]
        public string RootPath { get; set; } = string.Empty;
        /// <summary>
        /// 主模块
        /// </summary>
        [Required(ErrorMessage = "主模块为空")]
        [Contains]
        public string MainModule { get; set; } = string.Empty;
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Required(ErrorMessage = "应用程序类型为空")]
        [Equal]
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 增量更新
        /// </summary>
        public bool IsIncrementalUpdating { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string? RunParams { get; set; }
    }
}
