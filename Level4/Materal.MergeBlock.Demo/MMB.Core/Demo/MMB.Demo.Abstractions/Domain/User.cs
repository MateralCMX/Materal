namespace MMB.Demo.Abstractions.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseDomain, IDomain
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
        [Contains]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别为空")]
        [Equal]
        public SexEnum Sex { get; set; }
    }
}
