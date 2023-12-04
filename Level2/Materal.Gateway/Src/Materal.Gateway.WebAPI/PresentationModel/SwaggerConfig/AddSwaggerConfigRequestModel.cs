using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.SwaggerConfig
{
    /// <summary>
    /// 添加Swagger配置请求模型
    /// </summary>
    public class AddSwaggerConfigRequestModel
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
