using System;
using System.Collections.Generic;
using System.Reflection;
using Materal.ConvertHelper;
using Microsoft.Extensions.Configuration;

namespace Materal.ConfigurationHelper
{
    public static class ConfigurationExtended
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
                IEnumerable<IConfigurationSection> tempChildrens = item.GetChildren();
                var tempModel = ConvertManager.GetDefultObject<T>();
                foreach (IConfigurationSection children in tempChildrens)
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
    }
}
