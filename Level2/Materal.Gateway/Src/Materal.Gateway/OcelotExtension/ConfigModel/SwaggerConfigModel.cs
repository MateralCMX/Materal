using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// Swagger配置模型
    /// </summary>
    public class SwaggerConfigModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// SwaggerKey
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Key { get; set; } = "SwaggerKey";
        /// <summary>
        /// 服务发现
        /// </summary>
        public bool TakeServersFromDownstreamService { get; set; } = false;
        /// <summary>
        /// 配置项
        /// </summary>
        public List<SwaggerItemConfigModel> Config { get; set; } = new();
    }
}
