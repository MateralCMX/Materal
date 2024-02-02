namespace RC.Authority.Abstractions.DTO.User
{
    /// <summary>
    /// 用户列表数据传输模型
    /// </summary>
    public partial class UserListDTO : IListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识为空")]
        public Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间为空")]
        public DateTime CreateTime { get; set; }
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
