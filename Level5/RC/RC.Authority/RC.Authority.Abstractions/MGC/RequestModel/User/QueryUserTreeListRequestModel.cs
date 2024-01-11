namespace RC.Authority.Abstractions.RequestModel.User
{
    /// <summary>
    /// 用户树查询请求模型
    /// </summary>
    public partial class QueryUserTreeListRequestModel : FilterModel
    {
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
