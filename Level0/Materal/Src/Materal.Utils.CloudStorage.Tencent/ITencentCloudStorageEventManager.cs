using COSXML.Model;
using COSXML.Transfer;

namespace Materal.Utils.CloudStorage.Tencent
{
    /// <summary>
    /// 腾讯云存储服务事件管理器
    /// </summary>
    public interface ITencentCloudStorageEventManager
    {
        /// <summary>
        /// 设置传输配置
        /// </summary>
        /// <param name="transferConfig"></param>
        void SetTransferConfig(TransferConfig transferConfig);
        /// <summary>
        /// 上传文件进度
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="total"></param>
        void UploadObjecttProgress(long completed, long total);
        /// <summary>
        /// 下载文件成功
        /// </summary>
        /// <param name="result"></param>
        void UploadObjectSuccess(CosResult result);
        /// <summary>
        /// 下载文件失败
        /// </summary>
        /// <param name="clientException"></param>
        /// <param name="serverException"></param>
        void UploadObjectFail(CosClientException clientException, CosServerException serverException);
        /// <summary>
        /// 下载文件进度
        /// </summary>
        /// <param name="completed"></param>
        /// <param name="total"></param>
        void DownloadObjectProgress(long completed, long total);
        /// <summary>
        /// 下载文件成功
        /// </summary>
        /// <param name="result"></param>
        void DownloadObjectSuccess(CosResult result);
        /// <summary>
        /// 下载文件失败
        /// </summary>
        /// <param name="clientException"></param>
        /// <param name="serverException"></param>
        void DownloadObjectFail(CosClientException clientException, CosServerException serverException);
        /// <summary>
        /// 获得文件Key
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        string GetFileKey(string filePath);
    }
}
