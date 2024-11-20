namespace Materal.Gateway.ConfigModel
{
    /// <summary>
    /// Swagger服务配置模型
    /// </summary>
    public class SwaggerServiceConfigModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Name { get; set; } = "SwaggerAPI";
        /// <summary>
        /// 路径
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Path { get; set; } = "/swagger/v1/swagger.json";
    }
}
