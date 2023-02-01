#nullable enable
using Materal.Model;
using Materal.BaseCore.Services;

namespace RC.EnvironmentServer.Services.Models.ConfigurationItem
{
    /// <summary>
    /// 配置项查询模型
    /// </summary>
    public partial class QueryConfigurationItemModel : PageRequestModel, IQueryServiceModel
    {
        /// <summary>
        /// 项目唯一标识
        /// </summary>
        [Equal]
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Equal]
        public string? ProjectName { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Equal]
        public Guid? NamespaceID { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        [Equal]
        public string? NamespaceName { get; set; }
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
