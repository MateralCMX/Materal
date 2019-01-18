using Materal.ConvertHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Materal.ConfigurationHelper
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// 获取配置文件数组对象值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="configuration">配置文件对象</param>
        /// <param name="rootName">根节点名称</param>
        /// <returns>类型组</returns>
        public static List<T> GetArrayObjectValue<T>(this IConfiguration configuration, string rootName)
        {
            var result = new List<T>();
            IConfigurationSection configurationSection = configuration.GetSection(rootName);
            IEnumerable<IConfigurationSection> configurationSections = configurationSection.GetChildren();
            Type modelType = typeof(T);
            foreach (IConfigurationSection item in configurationSections)
            {
                IEnumerable<IConfigurationSection> tempChildren = item.GetChildren();
                var tempModel = ConvertManager.GetDefaultObject<T>();
                foreach (IConfigurationSection children in tempChildren)
                {
                    PropertyInfo propertyInfo = modelType.GetProperty(children.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(tempModel, children.Value.ConvertTo(propertyInfo.PropertyType));
                    }
                }
                result.Add(tempModel);
            }
            return result;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(this IConfiguration configuration, string key, string value)
        {
            if (!(configuration is ConfigurationRoot configurationRoot)) return;
            foreach (IConfigurationProvider provider in configurationRoot.Providers)
            {
                if (provider is JsonConfigurationProvider jsonProvider)
                {
                    var configurationProvider = new MateralJsonConfigurationProvider(jsonProvider);
                    configurationProvider.Load();
                    configurationProvider.Set(key, value);
                    configurationProvider.Save();
                }
                /*
                 * todo:其它配置类型
                 */
                provider.Load();
            }
        }
    }
}
