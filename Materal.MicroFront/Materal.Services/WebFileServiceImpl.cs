using Materal.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Materal.Services
{
    public class WebFileServiceImpl : IWebFileService
    {
        private List<ServiceModel> _services;
        public WebFileServiceImpl()
        {
            InitServices();
        }
        public List<ServiceModel> GetServices()
        {
            return _services;
        }

        public void DeleteService(string serviceName)
        {
            string servicesPath = $"{AppDomain.CurrentDomain.BaseDirectory}HtmlPages/{serviceName}";
            if (!Directory.Exists(servicesPath)) throw new InvalidOperationException("该服务不存在");
            Directory.Delete(servicesPath, true);
            InitServices();
        }

        public void InitServices()
        {
            string servicesPath = $"{AppDomain.CurrentDomain.BaseDirectory}HtmlPages";
            var blackList = new []
            {
                "Manager",
                "Portal"
            };
            _services = new List<ServiceModel>();
            if (!Directory.Exists(servicesPath)) throw new InvalidOperationException("请重新部署服务");
            DirectoryInfo[] directoryInfos = new DirectoryInfo(servicesPath).GetDirectories();
            directoryInfos = directoryInfos.Where(m => !blackList.Contains(m.Name)).ToArray();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                _services.Add(GetServiceModel(directoryInfo.FullName));
            }
        }

        private ServiceModel GetServiceModel(string targetPath)
        {
            var serviceModel = new ServiceModel();
            var targetPathInfo = new DirectoryInfo(targetPath);
            serviceModel.Name = targetPathInfo.Name;
            return serviceModel;
        }
    }
}
