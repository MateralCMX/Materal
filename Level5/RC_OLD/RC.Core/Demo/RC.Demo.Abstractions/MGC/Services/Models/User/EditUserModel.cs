namespace RC.Demo.Abstractions.Services.Models.User
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
        [Required(ErrorMessage = "姓名为空"), StringLength(100, ErrorMessage = "姓名过长")]
        public string Name { get; set; }  = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别为空")]
        public SexEnum Sex { get; set; }
    }
}
