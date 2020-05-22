using System.Collections.Generic;

namespace Materal.Gateway.Services.Models
{
    /// <summary>
    /// Ocelot文件模型
    /// </summary>
    public class OcelotFileModel
    {
        /// <summary>
        /// 项
        /// </summary>
        public List<ConfigItemFileModel> ReRoutes { get; set; } = new List<ConfigItemFileModel>();
        /// <summary>
        /// 全局配置
        /// </summary>
        public GlobalConfigFileModel GlobalConfiguration { get; set; } = new GlobalConfigFileModel();
    }
}
