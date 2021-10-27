using System.Collections.Generic;

namespace Materal.APP.WebAPICore
{
    public class WebAPIStartupConfig
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 启用Https重定向
        /// </summary>
        public bool EnableHttpsRedirection { get; set; } = false;

        /// <summary>
        /// 启用授权
        /// </summary>
        public bool EnableAuthorization { get; set; } = true;

        /// <summary>
        /// 启用鉴权
        /// </summary>
        public bool EnableAuthentication { get; set; } = true;

        /// <summary>
        /// 启用响应压缩
        /// </summary>
        public bool EnableResponseCompression { get; set; } = true;

        /// <summary>
        /// 启用Swagger
        /// </summary>
        public bool EnableSwagger { get; set; } = true;

        /// <summary>
        /// SwaggerXml文件组
        /// </summary>
        public ICollection<string> SwaggerXmlPathArray{ get; set; }

        /// <summary>
        /// 启用跨域
        /// </summary>
        public bool EnableCors { get; set; } = true;
    }
}
