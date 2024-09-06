/*
 * Generator Code From MateralMergeBlock=>GeneratorQueryModelAsync
 */
namespace RC.ServerCenter.Abstractions.Services.Models.Namespace
{
    /// <summary>
    /// 命名空间查询模型
    /// </summary>
    public partial class QueryNamespaceModel : PageRequestModel, IQueryServiceModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Contains]
        public string? Description { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Equal]
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        [Contains("ID")]
        public List<Guid>? IDs { get; set; }
        /// <summary>
        /// 最小创建时间
        /// </summary>
        [GreaterThanOrEqual("CreateTime")]
        public DateTime? MinCreateTime { get; set; }
        /// <summary>
        /// 最大创建时间
        /// </summary>
        [LessThanOrEqual("CreateTime")]
        public DateTime? MaxCreateTime { get; set; }
    }
}
