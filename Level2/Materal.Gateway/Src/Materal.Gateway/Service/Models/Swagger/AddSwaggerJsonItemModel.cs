using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.Swagger
{
    /// <summary>
    /// 添加Swagger项配置模型
    /// </summary>
    public class AddSwaggerJsonItemModel : AddSwaggerItemConfigModel
    {
        /// <summary>
        /// SwaggerJson文件地址
        /// </summary>
        [Required(ErrorMessage = "Url必填")]
        public string Url { get; set; } = string.Empty;
    }
}
