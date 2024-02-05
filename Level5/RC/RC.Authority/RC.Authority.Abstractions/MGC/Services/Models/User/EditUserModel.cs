namespace RC.Authority.Abstractions.Services.Models.User
{
    /// <summary>
    /// 用户修改模型
    /// </summary>
    public partial class EditUserModel : IEditServiceModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名最多100个字符")]
        public string Name { get; set; }  = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号为空"), StringLength(100, ErrorMessage = "账号最多100个字符")]
        public string Account { get; set; }  = string.Empty;
    }
}
