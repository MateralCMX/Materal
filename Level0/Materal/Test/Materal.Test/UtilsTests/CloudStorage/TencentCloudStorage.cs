using Materal.Utils.CloudStorage.Tencent;
using Materal.Utils.CloudStorage.Tencent.Models;
using Microsoft.Extensions.Configuration;

namespace Materal.Test.UtilsTests.CloudStorage
{
    [TestClass]
    public class TencentCloudStorage : BaseTest
    {
        private readonly TencentCloudStorageService _cloudStorageService;
        private const string fileKey = "你问我.gif";
        public TencentCloudStorage()
        {
            _cloudStorageService = ServiceProvider.GetRequiredService<TencentCloudStorageService>();
        }
        public override void AddConfig(IConfigurationBuilder builder)
        {
            builder.AddJsonFile("UtilsTests\\CloudStorage\\TencentCloudStorageConfig.json", true, true);
        }
        public override void AddServices(IServiceCollection services)
        {
            services.AddTencentCloudStorage();
            services.Configure<TencentCloudStorageConfig>(Configuration);
        }
        [TestMethod]
        public void GenerateTemporaryKeyTest()
        {
            string[] allowActions = [
                "name/cos:PutObject",
                "name/cos:PostObject",
                "name/cos:InitiateMultipartUpload",
                "name/cos:ListMultipartUploads",
                "name/cos:ListParts",
                "name/cos:UploadPart",
                "name/cos:CompleteMultipartUpload",
            ];
            TemporaryKey temporaryKey = _cloudStorageService.GetTemporaryKey(allowActions, "*");
            Console.WriteLine($"SecretID:{temporaryKey.SecretID}");
            Console.WriteLine($"SecretKey:{temporaryKey.SecretKey}");
            Console.WriteLine($"Token:{temporaryKey.Token}");
            Console.WriteLine($"StartTime:{temporaryKey.StartTime}");
            Console.WriteLine($"ExpirationTime:{temporaryKey.ExpirationTime}");
            Console.WriteLine($"Expiration:{temporaryKey.Expiration}");
        }
        [TestMethod]
        public void ObjectExistTest()
        {
            bool result = _cloudStorageService.ObjectExist(fileKey);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GetObjectUrlTest()
        {
            string result = _cloudStorageService.GetObjectUrl(fileKey);
            Console.WriteLine(result);
        }
        [TestMethod]
        public async Task UpdateObjectTestAsync()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            filePath = Path.Combine(filePath, "UtilsTests", "CloudStorage", "Downloads");
            if (!Directory.Exists(filePath)) throw new MateralException("文件夹不存在");
            filePath = Path.Combine(filePath, fileKey);
            if (!File.Exists(filePath)) throw new MateralException("文件不存在");
            if (_cloudStorageService.ObjectExist(fileKey))
            {
                _cloudStorageService.DeleteObject(fileKey);
            }
            string key = await _cloudStorageService.UploadObjectAsync(filePath);
            Assert.AreEqual(fileKey, key);
        }
        [TestMethod]
        public void DeleteObjectTest()
        {
            if (_cloudStorageService.ObjectExist(fileKey))
            {
                _cloudStorageService.DeleteObject(fileKey);
            }
        }
        [TestMethod]
        public async Task DownloadObjectTestAsync()
        {
            string saveFilePath = AppDomain.CurrentDomain.BaseDirectory;
            saveFilePath = Path.Combine(saveFilePath, "UtilsTests", "CloudStorage", "Downloads");
            if (!Directory.Exists(saveFilePath))
            {
                Directory.CreateDirectory(saveFilePath);
            }
            saveFilePath = Path.Combine(saveFilePath, fileKey);
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }
            await _cloudStorageService.DownloadObjectAsync(saveFilePath, fileKey);
        }
    }
}
