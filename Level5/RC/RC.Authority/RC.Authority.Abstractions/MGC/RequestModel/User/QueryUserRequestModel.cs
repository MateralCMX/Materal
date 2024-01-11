namespace RC.Authority.Abstractions.RequestModel.User
{
    /// <summary>
    /// 用户查询请求模型
    /// </summary>
    public partial class QueryUserRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        public List<Guid>? IDs { get; set; }
        /// <summary>
        /// 最小创建时间
        /// </summary>
        public DateTime? MinCreateTime { get; set; }
        /// <summary>
        /// 最大创建时间
        /// </summary>
        public DateTime? MaxCreateTime { get; set; }
    }
}
