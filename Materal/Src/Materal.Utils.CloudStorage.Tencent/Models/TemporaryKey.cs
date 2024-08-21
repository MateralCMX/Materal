namespace Materal.Utils.CloudStorage.Tencent.Models
{
    /// <summary>
    /// 临时秘钥
    /// </summary>
    public class TemporaryKey
    {
        /// <summary>
        /// 秘钥ID
        /// </summary>
        public string SecretID { get; set; } = string.Empty;
        /// <summary>
        /// 秘钥Key
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime Expiration { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public long ExpirationTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public long StartTime { get; set; }
    }
}
