using System;
using Materal.CacheHelper;
using Materal.ConvertHelper;
using Materal.DateTimeHelper;
using Materal.Gateway.Models;
using Materal.NetworkHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Materal.Gateway.Services.Models
{
    /// <summary>
    /// Ocelot模型
    /// </summary>
    public class OcelotConfigModel
    {
        private readonly ICacheManager _cacheManager;
        private const string tokenKey = "OcelotToken";
        private readonly string _path;
        /// <summary>
        /// Ocelot模型
        /// </summary>
        /// <param name="path"></param>
        public OcelotConfigModel(string path)
        {
            _path = path;
            _cacheManager = ApplicationData.GetService<ICacheManager>();
            InitByFile();
        }
        /// <summary>
        /// 项
        /// </summary>
        public List<ConfigItemModel> Items { get; set; } = new List<ConfigItemModel>();
        /// <summary>
        /// 全局配置
        /// </summary>
        public GlobalConfigModel GlobalConfig { get; set; } = new GlobalConfigModel();
        /// <summary>
        /// 保存
        /// </summary>
        public async Task SaveAsync()
        {
            var fileModel = new OcelotFileModel
            {
                GlobalConfiguration =
                {
                    ServiceDiscoveryProvider = new ServiceDiscovery
                    {
                        Host = GlobalConfig.ConsulHost, Port = GlobalConfig.ConsulPort
                    }
                }
            };
            fileModel.ReRoutes.AddRange(Items.Select(m => new ConfigItemFileModel(m)));
            string json = fileModel.ToJson();
            string url = $"{ApplicationData.Host}{ApplicationData.OcelotConfigUrl}/configuration";
            Dictionary<string, string> heads = await GetHeadsAsync();
            await HttpManager.SendPostAsync(url, json, heads, Encoding.UTF8);
        }
        #region 私有方法
        /// <summary>
        /// 获得头
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string, string>> GetHeadsAsync()
        {
            var result = new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {await GetTokenAsync()}",
                ["Content-Type"] = "application/json"
            };
            return result;
        }
        /// <summary>
        /// 通过文件初始化
        /// </summary>
        private void InitByFile()
        {
            if (!File.Exists(_path))
            {
                InitByNew();
            }
            string jsonString = File.ReadAllText(_path);
            var fileModel = jsonString.JsonToObject<OcelotFileModel>();
            GlobalConfig.ConsulHost = fileModel.GlobalConfiguration.ServiceDiscoveryProvider.Host;
            GlobalConfig.ConsulPort = fileModel.GlobalConfiguration.ServiceDiscoveryProvider.Port;
            Items.AddRange(fileModel.ReRoutes.Select(m =>
            {
                var result = m.CopyProperties<ConfigItemModel>();
                result.ID = Guid.NewGuid();
                result.EnableCache = m.FileCacheOptions.TtlSeconds > 0;
                return result;
            }));
        }
        /// <summary>
        /// 通过新建初始化
        /// </summary>
        private void InitByNew()
        {
            GlobalConfig = new GlobalConfigModel();
            Task.Run(async () => await SaveAsync());
        }
        /// <summary>
        /// 获得Token
        /// </summary>
        private async Task<string> GetTokenAsync()
        {
            var token = _cacheManager.Get<string>(tokenKey);
            if (!string.IsNullOrEmpty(token)) return token;
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            var postContent = new MultipartFormDataContent();
            postContent.Headers.Add("ContentType", "multipart/form-data");
            postContent.Add(new StringContent("admin"), "client_id");
            postContent.Add(new StringContent("secret"), "client_secret");
            postContent.Add(new StringContent("admin"), "scope");
            postContent.Add(new StringContent("client_credentials"), "grant_type");
            string url = $"{ApplicationData.Host}{ApplicationData.OcelotConfigUrl}/connect/token";
            HttpResponseMessage response = await client.PostAsync(url, postContent);
            string responseStr = await response.Content.ReadAsStringAsync();
            var ocelotToken = responseStr.JsonToObject<OcelotTokenModel>();
            _cacheManager.SetByAbsolute(tokenKey, ocelotToken.access_token, ocelotToken.expires_in - 10, DateTimeTypeEnum.Minute);
            return ocelotToken.access_token;
        }
        #endregion
    }
}
