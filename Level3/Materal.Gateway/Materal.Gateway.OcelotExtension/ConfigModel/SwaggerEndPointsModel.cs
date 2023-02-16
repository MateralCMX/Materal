namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// Swagger配置模型
    /// </summary>
    public class SwaggerEndPointsModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Key { get; set; } = "SwaggerKey";
        /// <summary>
        /// 配置项
        /// </summary>
        public List<SwaggerItemConfigModel> Config { get; set; } = new();
    }
}
