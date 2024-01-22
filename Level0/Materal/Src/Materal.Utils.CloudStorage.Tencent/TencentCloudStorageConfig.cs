namespace Materal.Utils.CloudStorage.Tencent
{
    /// <summary>
    /// 腾讯云配置
    /// </summary>
    public class TencentCloudStorageConfig
    {
        /// <summary>
        /// 应用程序标识
        /// </summary>
        public string AppID { get; set; } = string.Empty;
        /// <summary>
        /// 秘钥ID
        /// </summary>
        public string SecretID { get; set; } = string.Empty;
        /// <summary>
        /// 秘钥Key
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;
        /// <summary>
        /// 默认存储桶名称
        /// </summary>
        public string DefaultBucketName { get; set; } = "Default";
        /// <summary>
        /// 默认区域
        /// </summary>
        public string DefaultRegion { get; set; } = "ap-guangzhou";//https://cloud.tencent.com/document/product/436/6224
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsOK => !string.IsNullOrWhiteSpace(AppID) && !string.IsNullOrWhiteSpace(SecretID) && !string.IsNullOrWhiteSpace(SecretKey);
        /// <summary>
        /// 获取存储桶名称
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public string GetBucket(string? bucketName = null) => $"{bucketName ?? DefaultBucketName}-{AppID}";
    }
}
