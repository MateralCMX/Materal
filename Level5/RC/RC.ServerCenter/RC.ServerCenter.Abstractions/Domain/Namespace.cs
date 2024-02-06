namespace RC.ServerCenter.Abstractions.Domain
{
    /// <summary>
    /// 命名空间
    /// </summary>
    public class Namespace : BaseDomain, IDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [NotEdit]
        [Required(ErrorMessage = "名称为空"), StringLength(50, ErrorMessage = "名称过长")]
        [Contains]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Required(ErrorMessage = "描述为空"), StringLength(200, ErrorMessage = "描述过长")]
        [Contains]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [NotEdit]
        [Required(ErrorMessage = "为空")]
        [Equal]
        public Guid ProjectID { get; set; }
    }
}
