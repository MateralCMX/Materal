using Materal.Model;

namespace RC.EnvironmentServer.Services.Models.ConfigurationItem
{
    public partial class QueryConfigurationItemModel
    {
        /// <summary>
        /// 命名空间名称组
        /// </summary>
        [Contains]
        public List<string>? NamespaceNames { get; set; }
    }
}
