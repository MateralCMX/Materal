namespace RC.EnvironmentServer.PresentationModel.ConfigurationItem
{
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
