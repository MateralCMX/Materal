using System.Collections;
using System.Dynamic;
using System.Reflection;
using System.Text.Json;

namespace System
{
    /// <summary>
    /// 动态对象扩展
    /// </summary>
    public static class ExpandoObjectExtension
    {
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this object? obj)
        {
            if (obj is null) return obj;
            if (obj is string stringValue) return stringValue.ToExpandoObject();
            if (obj is JsonElement jsonElement) return jsonElement.ToExpandoObject();
            if (obj is IEnumerable enumerable) return enumerable.ToExpandoObject();
            if (!obj.GetType().IsClass) return obj;
            ExpandoObject result = new();
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (!propertyInfo.CanRead) continue;
                object? value = propertyInfo.GetValue(obj);
                value = value?.ToExpandoObject();
                result.TryAdd(propertyInfo.Name, value);
            }
            return result;
        }
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this string stringValue)
        {
            if (stringValue is null) return null;
            if (stringValue.IsArrayJson())
            {
                return (object)stringValue.JsonToDeserializeObject<List<ExpandoObject>>() ?? stringValue;
            }
            else if (stringValue.IsObjectJson())
            {
                return (object)stringValue.JsonToDeserializeObject<ExpandoObject>() ?? stringValue;
            }
            else
            {
                return stringValue;
            }
        }
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="jsonElement"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this JsonElement jsonElement) => jsonElement.ValueKind switch
        {
            JsonValueKind.String => jsonElement.GetString()?.ToExpandoObject(),
            JsonValueKind.Number => jsonElement.GetDecimal(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Undefined => null,
            JsonValueKind.Null => null,
            _ => jsonElement.ToString().ToExpandoObject(),
        };
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="expandoObject"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this ExpandoObject expandoObject) => expandoObject;
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this IEnumerable enumerable)
        {
            if (enumerable is null) return null;
            if (enumerable is ExpandoObject expandoObject) return expandoObject.ToExpandoObject();
            if (enumerable is IDictionary dictionary) return dictionary.ToExpandoObject();
            List<object?> result = [];
            foreach (object item in enumerable)
            {
                if (item is null) continue;
                result.Add(item.ToExpandoObject());
            }
            return result;
        }
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this IDictionary dictionary)
        {
            if (dictionary is null) return null;
            ExpandoObject result = new();
            foreach (object? item in dictionary)
            {
                if (item is null || item is not DictionaryEntry dictionaryEntry) continue;
                object keyObj = dictionaryEntry.Key;
                string? key = keyObj is string keyValue ? keyValue : keyObj.ToString();
                if (key is null) continue;
                result.TryAdd(key, dictionaryEntry.Value.ToExpandoObject());
            }
            return result;
        }
#if NETSTANDARD
        /// <summary>
        /// 尝试添加
        /// </summary>
        /// <param name="expandoObject"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool TryAdd(this ExpandoObject expandoObject, string key, object? value)
        {
            if (expandoObject is null) throw new ArgumentNullException(nameof(expandoObject));
            if (key is null) throw new ArgumentNullException(nameof(key));
            if (expandoObject is not IDictionary<string, object?> expandoDictionary) throw new ArgumentNullException(nameof(expandoObject));
            if (expandoDictionary.ContainsKey(key)) return false;
            expandoDictionary.Add(key, value);
            return true;
        }
#endif
    }
}
