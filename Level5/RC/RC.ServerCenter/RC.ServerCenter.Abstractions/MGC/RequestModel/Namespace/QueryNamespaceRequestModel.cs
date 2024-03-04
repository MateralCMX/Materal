/*
 * Generator Code From MateralMergeBlock=>GeneratorQueryRequestModel
 */
namespace RC.ServerCenter.Abstractions.RequestModel.Namespace
{
    /// <summary>
    /// 命名空间查询请求模型
    /// </summary>
    public partial class QueryNamespaceRequestModel : PageRequestModel, IQueryRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid? ProjectID { get; set; }
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
