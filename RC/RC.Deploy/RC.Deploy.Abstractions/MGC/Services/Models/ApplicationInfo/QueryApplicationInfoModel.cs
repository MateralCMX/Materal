/*
 * Generator Code From MateralMergeBlock=>GeneratorQueryModelAsync
 */
namespace RC.Deploy.Abstractions.Services.Models.ApplicationInfo
{
    /// <summary>
    /// 应用程序信息查询模型
    /// </summary>
    public partial class QueryApplicationInfoModel : PageRequestModel, IQueryServiceModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Contains]
        public string? Name { get; set; }
        /// <summary>
        /// 根路径
        /// </summary>
        [Contains]
        public string? RootPath { get; set; }
        /// <summary>
        /// 主模块
        /// </summary>
        [Contains]
        public string? MainModule { get; set; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Equal]
        public ApplicationTypeEnum? ApplicationType { get; set; }
        /// <summary>
        /// 增量更新
        /// </summary>
        [Equal]
        public bool? IsIncrementalUpdating { get; set; }
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
