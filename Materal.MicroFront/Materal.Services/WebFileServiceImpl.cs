using HtmlAgilityPack;
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
            foreach (FileInfo fileInfo in targetPathInfo.GetFiles("Index.html"))
            {
                if (fileInfo.Name.ToLower() != "index.html") continue;
                string html = File.ReadAllText(fileInfo.FullName);
                var document = new HtmlDocument();
                document.LoadHtml(html);
                HtmlNodeCollection scriptNodes = document.DocumentNode.SelectNodes("//script");
                serviceModel.Scripts = new List<string>();
                foreach (HtmlNode scriptNode in scriptNodes)
                {
                    string attributeValue = scriptNode.GetAttributeValue("src", string.Empty);
                    if (string.IsNullOrEmpty(attributeValue)) continue;
                    serviceModel.Scripts.Add(attributeValue);
                }
                HtmlNodeCollection linkNodes = document.DocumentNode.SelectNodes("//link");
                serviceModel.Links = new List<LinkModel>();
                foreach (HtmlNode linkNode in linkNodes)
                {
                    string hrefAttributeValue = linkNode.GetAttributeValue("href", string.Empty);
                    string relAttributeValue = linkNode.GetAttributeValue("rel", string.Empty);
                    string asAttributeValue = linkNode.GetAttributeValue("as", string.Empty);
                    if (string.IsNullOrEmpty(hrefAttributeValue) || string.IsNullOrEmpty(relAttributeValue)) continue;
                    serviceModel.Links.Add(new LinkModel
                    {
                        HrefAttribute = hrefAttributeValue,
                        RelAttribute = relAttributeValue,
                        AsAttribute = asAttributeValue
                    });
                }
            }
            return serviceModel;
        }
    }
}
