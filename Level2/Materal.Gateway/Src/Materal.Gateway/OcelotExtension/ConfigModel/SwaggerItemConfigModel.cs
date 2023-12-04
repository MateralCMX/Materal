using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// Swagger项配置模型
    /// </summary>
    public class SwaggerItemConfigModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Name { get; set; } = "SwaggerAPI";
        /// <summary>
        /// 版本
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Version { get; set; } = "v1";
        /// <summary>
        /// 服务配置
        /// </summary>
        public SwaggerServiceConfigModel? Service { get; set; }
        /// <summary>
        /// SwaggerJson文件地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Url { get; set; }
    }
}
