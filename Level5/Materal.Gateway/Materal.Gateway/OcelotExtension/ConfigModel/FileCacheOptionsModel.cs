namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    /// <summary>
    /// 缓存配置模型
    /// </summary>
    public class FileCacheOptionsModel
    {
        /// <summary>
        /// 缓存时间
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public int TtlSeconds { get; set; } = 10;
        /// <summary>
        /// 缓存区键
        /// </summary>
        [Required(ErrorMessage = "必填")]
        public string Region { get; set; } = "CacheKey";
    }
}
