/*
 * Generator Code From MateralMergeBlock=>GeneratorQueryRequestModelAsync
 */
namespace RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem
{
    /// <summary>
    /// 配置项查询请求模型
    /// </summary>
    public partial class QueryConfigurationItemRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string? ProjectName { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid? NamespaceID { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string? NamespaceName { get; set; }
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
