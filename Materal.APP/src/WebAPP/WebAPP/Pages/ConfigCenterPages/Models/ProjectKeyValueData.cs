using ConfigCenter.DataTransmitModel.Project;
using Materal.APP.Core.Models;
using System.Collections.Generic;

namespace WebAPP.Pages.ConfigCenterPages.Models
{
    /// <summary>
    /// 项目KeyValue附加数据
    /// </summary>
    public class ProjectKeyValueData
    {
        /// <summary>
        /// 项目
        /// </summary>
        public ProjectListDTO Project { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public List<KeyValueModel> Namespaces { get; set; }
    }
}
