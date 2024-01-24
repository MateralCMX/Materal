namespace RC.Deploy.Abstractions.RequestModel.DefaultData
{
    /// <summary>
    /// 默认数据查询请求模型
    /// </summary>
    public partial class QueryDefaultDataRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationTypeEnum? ApplicationType { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public string? Key { get; set; }
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
