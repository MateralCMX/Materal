namespace Materal.Gateway.Service.Models.Tools
{
    /// <summary>
    /// 同步Swagger模型
    /// </summary>
    public class GetSwaggerFromConsulModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// 是否清空
        /// </summary>
        public bool Clear { get; set; } = false;
    }
}
