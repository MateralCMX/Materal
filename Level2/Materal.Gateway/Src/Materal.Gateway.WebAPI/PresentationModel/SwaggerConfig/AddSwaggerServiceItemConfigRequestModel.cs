using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.SwaggerConfig
{
    /// <summary>
    /// 添加Swagger项配置请求模型
    /// </summary>
    public class AddSwaggerServiceItemConfigRequestModel : AddSwaggerItemConfigRequestModel
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [Required(ErrorMessage = "ServerName必填")]
        public string ServiceName { get; set; } = "SwaggerAPI";
        /// <summary>
        /// 服务路径
        /// </summary>
        [Required(ErrorMessage = "ServerPath必填")]
        public string ServicePath { get; set; } = "/swagger/v1/swagger.json";
    }
}
