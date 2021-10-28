using System.Collections.Generic;

namespace Materal.APP.Core.ConfigModels
{
    public class WebAPIStartupConfig
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// SwaggerXml文件组
        /// </summary>
        public ICollection<string> SwaggerXmlPathArray{ get; set; }
    }
}
