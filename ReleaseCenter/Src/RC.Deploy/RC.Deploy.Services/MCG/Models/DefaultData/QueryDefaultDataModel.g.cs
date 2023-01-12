#nullable enable
using Materal.Model;
using Materal.BaseCore.Services;
using RC.Deploy.Enums;

namespace RC.Deploy.Services.Models.DefaultData
{
    /// <summary>
    /// 默认数据查询模型
    /// </summary>
    public partial class QueryDefaultDataModel : PageRequestModel, IQueryServiceModel
    {
        /// <summary>
        /// 应用程序类型
        /// </summary>
        [Equal]
        public ApplicationTypeEnum? ApplicationType { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        [Equal]
        public string? Key { get; set; }
        /// <summary>
        /// 唯一标识组
        /// </summary>
        [Contains("ID")]
        public List<Guid>? IDs { get; set; }
        /// <summary>
        /// 最大创建时间
        /// </summary>
        [LessThanOrEqual("CreateTime")]
        public DateTime? MaxCreateTime { get; set; }
        /// <summary>
        /// 最小创建时间
        /// </summary>
        [GreaterThanOrEqual("CreateTime")]
        public DateTime? MinCreateTime { get; set; }
    }
}
