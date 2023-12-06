using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.Swagger
{
    /// <summary>
    /// 添加Swagger配置模型
    /// </summary>
    public class AddSwaggerModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Key { get; set; } = "SwaggerKey";
        /// <summary>
        /// 服务发现
        /// </summary>
        public bool TakeServersFromDownstreamService { get; set; } = false;
    }
}
