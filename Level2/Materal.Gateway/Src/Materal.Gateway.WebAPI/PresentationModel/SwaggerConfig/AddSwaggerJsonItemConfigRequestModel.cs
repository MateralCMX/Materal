using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.SwaggerConfig
{
    /// <summary>
    /// 添加Swagger项配置请求模型
    /// </summary>
    public class AddSwaggerJsonItemConfigRequestModel : AddSwaggerItemConfigRequestModel
    {
        /// <summary>
        /// SwaggerJson文件地址
        /// </summary>
        [Required(ErrorMessage = "Url必填")]
        public string Url { get; set; } = string.Empty;
    }
}
