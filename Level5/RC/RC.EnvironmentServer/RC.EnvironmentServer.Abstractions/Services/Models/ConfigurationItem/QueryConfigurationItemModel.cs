namespace RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem
{
    /// <summary>
    /// 查询配置项模型
    /// </summary>
    public partial class QueryConfigurationItemModel
    {
        /// <summary>
        /// 命名空间名称组
        /// </summary>
        [Contains("NamespaceName")]
        public List<string>? NamespaceNames { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Contains("NamespaceID")]
        public List<Guid>? NamespaceIDs { get; set; }
    }
}
