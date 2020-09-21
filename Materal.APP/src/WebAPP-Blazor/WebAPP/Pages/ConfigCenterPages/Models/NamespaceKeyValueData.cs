using ConfigCenter.DataTransmitModel.Namespace;
using ConfigCenter.DataTransmitModel.Project;

namespace WebAPP.Pages.ConfigCenterPages.Models
{
    /// <summary>
    /// 项目KeyValue附加数据
    /// </summary>
    public class NamespaceKeyValueData
    {
        /// <summary>
        /// 项目
        /// </summary>
        public ProjectListDTO Project { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public NamespaceListDTO Namespace { get; set; }
    }
}
