namespace Materal.Gateway.DataTransmitModel.Swagger
{
    /// <summary>
    /// Swager数据传输模型
    /// </summary>
    public class SwaggerDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Key { get; set; } = "SwaggerKey";
        /// <summary>
        /// 服务发现
        /// </summary>
        public bool TakeServersFromDownstreamService { get; set; } = false;
        /// <summary>
        /// 项
        /// </summary>
        public List<SwaggerItemDTO> Items { get; set; } = [];
    }
}
