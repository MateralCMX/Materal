using Materal.Model;

namespace ConfigCenter.Environment.PresentationModel.ConfigurationItem
{
    /// <summary>
    /// 查询配置项过滤器请求模型
    /// </summary>
    public class QueryConfigurationItemFilterRequestModel : FilterModel
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string[] NamespaceNames { get; set; }
    }
}
