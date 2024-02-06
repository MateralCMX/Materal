namespace RC.ServerCenter.Abstractions.Domain
{
    /// <summary>
    /// 项目
    /// </summary>
    public class Project : BaseDomain, IDomain
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
    }
}
