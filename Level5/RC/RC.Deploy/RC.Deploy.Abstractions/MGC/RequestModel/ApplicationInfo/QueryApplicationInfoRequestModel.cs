namespace RC.Deploy.Abstractions.RequestModel.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息查询请求模型
    /// </summary>
    public partial class QueryApplicationInfoRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 根路径
        /// </summary>
        public string? RootPath { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        public string? MainModule { get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationTypeEnum? ApplicationType { get; set; }
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
