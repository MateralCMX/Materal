using Microsoft.Extensions.Configuration;

namespace Materal.Extensions
{
    /// <summary>
    /// 配置对象扩展
    /// </summary>
    public static class ConfigurationExtensions
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
            if (string.IsNullOrEmpty(value) || value is null) return default;
            T? result;
            Type tType = typeof(T);
            if (tType.IsGenericType && tType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                tType = tType.GetGenericArguments()[0];
            }
            if (value.CanConvertTo(tType))
            {
                result = (T?)value.ConvertTo(tType);
            }
            else if (tType.IsEnum)
            {
                result = (T?)Enum.Parse(tType, value);
            }
            else if (value.IsJson())
            {
                result = value.JsonToDeserializeObject<T>();
            }
            else
            {
                result = default;
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
            if (value is not null)
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
            Dictionary<string, object?> propertyDic = [];
            List<object?> objects = [];
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
                    if (value is null) continue;
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
