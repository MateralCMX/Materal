namespace RC.Authority.Abstractions.Services.Models.User
{
    /// <summary>
    /// 用户树查询模型
    /// </summary>
    public partial class QueryUserTreeListModel : FilterModel
    {
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
