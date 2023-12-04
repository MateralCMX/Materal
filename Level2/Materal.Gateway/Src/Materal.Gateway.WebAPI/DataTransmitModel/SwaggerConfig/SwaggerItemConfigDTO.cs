namespace Materal.Gateway.WebAPI.DataTransmitModel.SwaggerConfig
{
    /// <summary>
    /// Swagger项配置数据传输模型
    /// </summary>
    public class SwaggerItemConfigDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;
        /// <summary>
        /// 服务路径
        /// </summary>
        public string ServicePath { get; set; } = string.Empty;
        /// <summary>
        /// SwaggerJson文件地址
        /// </summary>
        public string? Url { get; set; } = string.Empty;
    }
}
