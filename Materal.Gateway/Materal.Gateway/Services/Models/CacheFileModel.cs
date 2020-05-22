namespace Materal.Gateway.Services.Models
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class CacheFileModel
    {
        /// <summary>
        /// 时间
        /// </summary>
        public int TtlSeconds { get; set; } = 60;
        /// <summary>
        /// Region
        /// </summary>
        public string Region { get; set; }
    }
}
