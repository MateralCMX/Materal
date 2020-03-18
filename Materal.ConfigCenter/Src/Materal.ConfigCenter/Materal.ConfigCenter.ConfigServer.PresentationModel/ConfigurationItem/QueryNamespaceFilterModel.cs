using Materal.Model;

namespace Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem
{
    public class QueryConfigurationItemFilterModel : FilterModel
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        [Equal]
        public string ProjectName { get; set; }
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public string[] NamespaceNames { get; set; }
    }
}
