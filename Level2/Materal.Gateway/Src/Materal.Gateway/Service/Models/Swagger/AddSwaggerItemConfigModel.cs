using System.ComponentModel.DataAnnotations;

namespace Materal.Gateway.WebAPI.PresentationModel.Swagger
{
    /// <summary>
    /// 添加Swagger项配置模型
    /// </summary>
    public abstract class AddSwaggerItemConfigModel
    {
        /// <summary>
        /// Swagger配置唯一标识
        /// </summary>
        public Guid SwaggerConfigID { get; set; }
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
    }
}
