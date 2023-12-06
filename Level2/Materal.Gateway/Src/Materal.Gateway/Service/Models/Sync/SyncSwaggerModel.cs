namespace Materal.Gateway.Service.Models.Sync
{
    /// <summary>
    /// 同步Swagger模型
    /// </summary>
    public class SyncSwaggerModel
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
