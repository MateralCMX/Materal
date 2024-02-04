namespace RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem
{
    /// <summary>
    /// 查询配置项请求模型
    /// </summary>
    public partial class QueryConfigurationItemRequestModel
    {
        /// <summary>
        /// 命名空间名称组
        /// </summary>
        public List<string>? NamespaceNames { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public List<Guid>? NamespaceIDs { get; set; }
    }
}
