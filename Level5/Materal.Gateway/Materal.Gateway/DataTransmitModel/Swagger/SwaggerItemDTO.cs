namespace Materal.Gateway.DataTransmitModel.Swagger
{
    /// <summary>
    /// Swagger项数据传输模型
    /// </summary>
    public class SwaggerItemDTO
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
        public string? ServiceName { get; set; } = string.Empty;
        /// <summary>
        /// 服务路径
        /// </summary>
        public string? ServicePath { get; set; } = string.Empty;
        /// <summary>
        /// SwaggerJson文件地址
        /// </summary>
        public string? Url { get; set; } = string.Empty;
    }
}
