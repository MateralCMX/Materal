using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Model;
using Materal.ConvertHelper;

namespace Common
{
    public static class ApplicationConfig
    {
        private static IConfiguration _configuration;
        private static OcelotConfigModel _ocelotConfigModel;

        /// <summary>
        /// 配置对象
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration != null) return _configuration;
                _configuration = ConfigurationBuilder();
                return _configuration;
            }
            set => _configuration = value;
        }

        public static OcelotConfigModel OcelotConfigModel => _ocelotConfigModel ?? (_ocelotConfigModel = new OcelotConfigModel
        {
            GlobalConfiguration = new GlobalConfigurationModel
            {
                BaseUrl = Configuration["GlobalConfiguration:BaseUrl"]
            },
            ReRoutes = GetReRoutes()
        });
        /// <summary>
        /// 获取重定向路由模型
        /// </summary>
        /// <returns></returns>
        private static ReRouteModel[] GetReRoutes()
        {
            var reRouteModels = new List<ReRouteModel>();
            IConfigurationSection configurationSection = Configuration.GetSection("ReRoutes");
            IEnumerable<IConfigurationSection> configurationSections = configurationSection.GetChildren();
            Type modelType = typeof(ReRouteModel);
            foreach (IConfigurationSection item in configurationSections)
            {
                var tempModel = new ReRouteModel();
                IEnumerable<IConfigurationSection> tempChildren = item.GetChildren();
                foreach (IConfigurationSection children in tempChildren)
                {
                    PropertyInfo propertyInfo = modelType.GetProperty(children.Key);
                    if (propertyInfo == null) continue;
                    propertyInfo.SetValue(tempModel, children.Value.ConvertTo(propertyInfo.PropertyType));
                    if (propertyInfo.GetValue(tempModel) != null) continue;
                    switch (children.Key)
                    {
                        case "DownstreamHostAndPorts":
                            propertyInfo.SetValue(tempModel, GetDownstreamHostAndPorts(children));
                            break;
                        case "UpstreamHttpMethod":
                            propertyInfo.SetValue(tempModel, GetUpstreamHttpMethods(children));
                            break;
                    }
                }
                reRouteModels.Add(tempModel);
            }
            return reRouteModels.ToArray();
        }
        /// <summary>
        /// 获取下游主机和端口
        /// </summary>
        /// <param name="configurationSection"></param>
        /// <returns></returns>
        private static DownstreamHostAndPortModel[] GetDownstreamHostAndPorts(IConfiguration configurationSection)
        {
            var downstreamHostAndPortModels = new List<DownstreamHostAndPortModel>();
            IEnumerable<IConfigurationSection> configurationSections = configurationSection.GetChildren();
            Type modelType = typeof(DownstreamHostAndPortModel);
            foreach (IConfigurationSection item in configurationSections)
            {
                var tempModel = new DownstreamHostAndPortModel();
                IEnumerable<IConfigurationSection> tempChildren = item.GetChildren();
                foreach (IConfigurationSection children in tempChildren)
                {
                    PropertyInfo propertyInfo = modelType.GetProperty(children.Key);
                    if (propertyInfo == null) continue;
                    propertyInfo.SetValue(tempModel, children.Value.ConvertTo(propertyInfo.PropertyType));
                }
                downstreamHostAndPortModels.Add(tempModel);
            }
            return downstreamHostAndPortModels.ToArray();
        }
        /// <summary>
        /// 获取上游HttpMethod
        /// </summary>
        /// <param name="configurationSection"></param>
        /// <returns></returns>
        private static string[] GetUpstreamHttpMethods(IConfiguration configurationSection)
        {
            IEnumerable<IConfigurationSection> configurationSections = configurationSection.GetChildren();
            return configurationSections.Select(item => Convert.ToString(item.Value)).ToArray();
        }

        /// <summary>
        /// 配置生成
        /// </summary>
        /// <returns></returns>
        private static IConfiguration ConfigurationBuilder()
        {
            if (_configuration != null) return _configuration;

#if DEBUG
            const string appConfigFile = "ocelot.Development.json";
#else
            const string appConfigFile = "ocelot.json";
#endif
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appConfigFile);
            _configuration = builder.Build();
            return _configuration;
        }
    }
}
