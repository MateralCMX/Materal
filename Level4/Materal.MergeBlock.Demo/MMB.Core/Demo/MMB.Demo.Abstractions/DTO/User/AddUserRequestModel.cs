namespace MMB.Demo.Abstractions.DTO.User
{
    /// <summary>
    /// 用户添加请求模型
    /// </summary>
    public partial class AddUserRequestModel : IAddRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别为空")]
        public SexEnum Sex { get; set; }
    }
}
