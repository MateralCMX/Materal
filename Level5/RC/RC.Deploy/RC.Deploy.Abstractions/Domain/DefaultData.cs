using Materal.MergeBlock.GeneratorCode.Attributers;

namespace RC.Deploy.Abstractions.Domain
{
    /// <summary>
    /// 默认数据
    /// </summary>
    public class DefaultData : BaseDomain, IDomain
    {
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Required(ErrorMessage = "应用程序类型为空")]
        [Equal]
        public ApplicationTypeEnum ApplicationType { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [Required(ErrorMessage = "数据为空")]
        [Equal]
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// 数据
        /// </summary>
        [NotQuery]
        [Required(ErrorMessage = "数据为空")]
        public string Data { get; set; } = string.Empty;
    }
}
