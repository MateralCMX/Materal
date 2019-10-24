using Materal.ConvertHelper;
using Materal.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Materal.FileHelper;

namespace Materal.Services
{
    public class WebFileServiceImpl : IWebFileService
    {
        private readonly string _configPath = $"{AppDomain.CurrentDomain.BaseDirectory}WebServiceConfig.json";
        private readonly List<ServiceModel> _services;
        public WebFileServiceImpl()
        {
            if (!File.Exists(_configPath)) throw new InvalidOperationException("配置文件丢失");
            string jsonString = File.ReadAllText(_configPath);
            _services = jsonString.JsonToObject<List<ServiceModel>>();
        }
        public List<ServiceModel> GetServices()
        {
            return _services;
        }

        public async Task EditServiceAsync(ServiceModel serviceModel)
        {
            ServiceModel model = _services.FirstOrDefault(m => m.Name == serviceModel.Name);
            if (model == null) throw new InvalidOperationException("服务不存在不能修改");
            serviceModel.CopyProperties(model);
            await SaveConfigAsync();
        }

        public async Task AddServiceAsync(ServiceModel serviceModel)
        {
            ServiceModel model = _services.FirstOrDefault(m => m.Name == serviceModel.Name);
            if (model != null) throw new InvalidOperationException("服务已存在不能添加");
            _services.Add(serviceModel);
            await SaveConfigAsync();
        }

        public bool HasService(string serviceName)
        {
            ServiceModel model = _services.FirstOrDefault(m => m.Name == serviceName);
            return model != null;
        }

        #region 私有方法
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        private async Task SaveConfigAsync()
        {
            string jsonString = _services.ToJson();
            await TextFileManager.WriteTextAsync(_configPath, jsonString);
        }
        #endregion
    }
}
