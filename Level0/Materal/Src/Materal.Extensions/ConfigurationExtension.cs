using System.Reflection;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// 配置对象扩展
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// 获得值对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? GetValueObject<T>(this IConfiguration configuration, string key)
        {
            string? value = configuration.GetValue(key);
            if (string.IsNullOrEmpty(value) || value == null) return default;
            T? result;
            Type tType = typeof(T);
            if (value.CanConvertTo(tType))
            {
                result = (T?)value.ConvertTo(tType);
            }
            else if (tType.GetCustomAttribute<SerializableAttribute>() != null)
            {
                result = value.JsonToDeserializeObject<T>();
            }
            else
            {
                result = value.JsonToObject<T>();
            }
            return result;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string? GetValue(this IConfiguration configuration, string key)
        {
            IConfigurationSection configSection = configuration.GetSection(key);
            if (!string.IsNullOrWhiteSpace(configSection.Value)) return configSection.Value;
            return GetObjectJson(configSection);
        }
        /// <summary>
        /// 获得对象Json
        /// </summary>
        /// <param name="configSection"></param>
        /// <returns></returns>
        private static string? GetObjectJson(IConfigurationSection configSection)
        {
            object? value = GetDictionaryObject(configSection);
            if (value != null)
            {
                return value.ToJson();
            }
            return null;
        }
        /// <summary>
        /// 获得动态对象
        /// </summary>
        /// <param name="configSection"></param>
        /// <returns></returns>
        private static object? GetDictionaryObject(IConfigurationSection configSection)
        {
            IConfigurationSection[] sectionItems = configSection.GetChildren().ToArray();
            if (sectionItems.Length == 0) return null;
            Dictionary<string, object?> propertyDic = new();
            List<object?> objects = new();
            bool isArray = sectionItems.First().Key == "0";
            foreach (IConfigurationSection sectionItem in sectionItems)
            {
                if (!string.IsNullOrWhiteSpace(sectionItem.Value))
                {
                    if (!isArray)
                    {
                        propertyDic.Add(sectionItem.Key, sectionItem.Value);
                    }
                    else
                    {
                        objects.Add(sectionItem.Value);
                    }
                }
                else
                {
                    object? value = GetDictionaryObject(sectionItem);
                    if (value == null) continue;
                    if (!isArray)
                    {
                        propertyDic.Add(sectionItem.Key, value);
                    }
                    else
                    {
                        objects.Add(value);
                    }
                }
            }
            return isArray ? objects : propertyDic;
        }
    }
}
