using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Transfer;
using Materal.Abstractions;

namespace Materal.Utils.CloudStorage.Tencent
{
    /// <summary>
    /// 腾讯云存储服务
    /// </summary>
    public class TencentCloudStorageService
    {
        private readonly CosXml cosXml;
        private readonly IOptionsMonitor<TencentCloudStorageConfig> _config;
        private readonly ITencentCloudStorageEventManager? _eventManager;
        /// <summary>
        /// 构造方法
        /// </summary>
        public TencentCloudStorageService(IOptionsMonitor<TencentCloudStorageConfig> config, ITencentCloudStorageEventManager? eventManager = null)
        {
            _config = config;
            _eventManager = eventManager;
            CosXmlConfig cosConfig = new CosXmlConfig.Builder().SetRegion(config.CurrentValue.DefaultRegion).Build();
            QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(config.CurrentValue.SecretID, config.CurrentValue.SecretKey, 600);
            cosXml = new CosXmlServer(cosConfig, qCloudCredentialProvider);
        }
        /// <summary>
        /// 获得临时秘钥
        /// </summary>
        /// <param name="allowActions">
        /// 允许的操作范围
        /// 请参阅 https://cloud.tencent.com/document/product/436/10009
        /// name/cos:PutObject
        /// name/cos:PostObject
        /// </param>
        /// <param name="allowPrefix">允许的路径,正则表达式</param>
        /// <param name="time"></param>
        /// <param name="bucketName"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public TemporaryKey GetTemporaryKey(string[] allowActions, string allowPrefix = "*", int time = 600, string? bucketName = null, string? region = null)
        {
            try
            {
                if (allowActions.Length <= 0) throw new TencentCloudStorageException("请设置允许的操作范围");
                string bucket = _config.CurrentValue.GetBucket(bucketName);
                region ??= _config.CurrentValue.DefaultRegion;
                Dictionary<string, object> values = new()
                {
                    { "bucket", bucket },
                    { "region", region },
                    { "allowPrefix", allowPrefix },
                    { "allowActions", allowActions },
                    { "durationSeconds", time },
                    { "secretId", _config.CurrentValue.SecretID },
                    { "secretKey", _config.CurrentValue.SecretKey }
                };
                Dictionary<string, object> credential = STSClient.genCredential(values);
                if (credential["Credentials"] is not JObject data) throw new TencentCloudStorageException("创建临时秘钥失败");
                if (credential["Expiration"] is not DateTime expiration) throw new TencentCloudStorageException("获取Expiration失败");
                if (expiration.Kind != DateTimeKind.Local)
                {
                    expiration = expiration.ToLocalTime();
                }
                TemporaryKey result = new()
                {
                    Token = data["Token"]?.ToString() ?? throw new TencentCloudStorageException("获取临时Token失败"),
                    SecretID = data["TmpSecretId"]?.ToString() ?? throw new TencentCloudStorageException("获取临时SecretID失败"),
                    SecretKey = data["TmpSecretKey"]?.ToString() ?? throw new TencentCloudStorageException("获取临时SecretKey失败"),
                    Expiration = expiration,
                    ExpirationTime = Convert.ToInt64(credential["ExpiredTime"] ?? throw new TencentCloudStorageException("获取临时ExpiredTime失败")),
                    StartTime = Convert.ToInt64(credential["StartTime"] ?? throw new TencentCloudStorageException("获取临时StartTime失败")),
                };
                return result;
            }
            catch (Exception ex)
            {
                throw HandlerException(ex);
            }
        }
        /// <summary>
        /// 对象是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public bool ObjectExist(string key, string? bucketName = null)
        {
            string bucket = _config.CurrentValue.GetBucket(bucketName);
            DoesObjectExistRequest request = new(bucket, key);
            bool exist = cosXml.DoesObjectExist(request);
            return exist;
        }
        /// <summary>
        /// 获得对象访问Url
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public string GetObjectUrl(string key, string? bucketName = null)
        {
            string bucket = _config.CurrentValue.GetBucket(bucketName);
            string requestURL = cosXml.GetObjectUrl(bucket, key);
            return requestURL;
        }
        /// <summary>
        /// 上传对象
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        /// <exception cref="TencentCloudStorageException"></exception>
        public async Task<string> UploadObjectAsync(string savePath, string? bucketName = null)
        {
            string key = _eventManager?.GetFileKey(savePath) ?? Path.GetFileName(savePath);
            await UploadObjectByKeyAsync(savePath, key, bucketName);
            return key;
        }
        /// <summary>
        /// 上传对象
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="key"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task UploadObjectByKeyAsync(string savePath, string key, string? bucketName = null)
        {
            string bucket = _config.CurrentValue.GetBucket(bucketName);
            TransferConfig transferConfig = new();
            _eventManager?.SetTransferConfig(transferConfig);
            TransferManager transferManager = new(cosXml, transferConfig);
            COSXMLUploadTask uploadTask = new(bucket, key);
            uploadTask.SetSrcPath(savePath);
            if (_eventManager is not null)
            {
                uploadTask.progressCallback = _eventManager.UploadObjecttProgress;
                uploadTask.successCallback = _eventManager.UploadObjectSuccess;
                uploadTask.failCallback = _eventManager.UploadObjectFail;
            }
            try
            {
                COSXMLUploadTask.UploadTaskResult result = await transferManager.UploadAsync(uploadTask);
                if (!result.IsSuccessful()) throw new TencentCloudStorageException("文件上传失败");
            }
            catch (Exception ex)
            {
                throw HandlerException(ex);
            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bucketName"></param>
        public void DeleteObject(string key, string? bucketName = null)
        {
            try
            {
                string bucket = _config.CurrentValue.GetBucket(bucketName);
                DeleteObjectRequest request = new(bucket, key);
                DeleteObjectResult result = cosXml.DeleteObject(request);
                if (!result.IsSuccessful()) throw new TencentCloudStorageException("文件删除失败");
            }
            catch (Exception ex)
            {
                throw HandlerException(ex);
            }
        }
        /// <summary>
        /// 下载对象
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="key"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        /// <exception cref="TencentCloudStorageException"></exception>
        public async Task DownloadObjectAsync(string savePath, string key, string? bucketName = null)
        {
            string directoryPath = Path.GetDirectoryName(savePath) ?? throw new MateralException("获取文件名称失败");
            string fileName = Path.GetFileName(savePath);
            await DownloadObjectAsync(directoryPath, fileName, key, bucketName);
        }
        /// <summary>
        /// 下载对象
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="fileName"></param>
        /// <param name="key"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        /// <exception cref="TencentCloudStorageException"></exception>
        public async Task DownloadObjectAsync(string directoryPath, string fileName, string key, string? bucketName)
        {
            string savePath = Path.Combine(directoryPath, fileName);
            if (File.Exists(savePath)) throw new TencentCloudStorageException("文件已存在");
            string bucket = _config.CurrentValue.GetBucket(bucketName);
            TransferConfig transferConfig = new();
            _eventManager?.SetTransferConfig(transferConfig);
            TransferManager transferManager = new(cosXml, transferConfig);
            COSXMLDownloadTask downloadTask = new(bucket, key, directoryPath, fileName);
            if (_eventManager is not null)
            {
                downloadTask.progressCallback = _eventManager.DownloadObjectProgress;
                downloadTask.successCallback = _eventManager.DownloadObjectSuccess;
                downloadTask.failCallback = _eventManager.DownloadObjectFail;
            }
            try
            {
                COSXMLDownloadTask.DownloadTaskResult result = await transferManager.DownloadAsync(downloadTask);
                if (!result.IsSuccessful()) throw new TencentCloudStorageException("文件下载失败");
            }
            catch (Exception ex)
            {
                throw HandlerException(ex);
            }
        }
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static Exception HandlerException(Exception ex) => ex switch
        {
            CosClientException => new TencentCloudStorageException("云存储客户端异常", ex),
            CosServerException => new TencentCloudStorageException("云存储服务端异常", ex),
            TencentCloudStorageException => ex,
            _ => new TencentCloudStorageException("请求异常", ex)
        };
    }
}
