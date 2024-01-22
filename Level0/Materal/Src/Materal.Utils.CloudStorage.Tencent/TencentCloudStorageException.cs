namespace Materal.Utils.CloudStorage.Tencent
{
    /// <summary>
    /// 腾讯云存储异常
    /// </summary>
    public class TencentCloudStorageException : UtilException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public TencentCloudStorageException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public TencentCloudStorageException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public TencentCloudStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
