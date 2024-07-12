using System.Data;
using System.Dynamic;
using System.Text.Json;
using System.Xml;

namespace Materal.Extensions
{
    /// <summary>
    /// 动态对象扩展
    /// </summary>
    public static class ExpandoObjectExtensions
    {
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this object? obj)
        {
            if (obj is null) return obj;
            Type objType = obj.GetType();
            if (objType.GetCustomAttribute<IgnoreToExpandoObjectAttribute>() is not null) return obj;
            if (obj is string stringValue) return stringValue.ToExpandoObject();
            if (obj is JsonElement jsonElement) return jsonElement.ToExpandoObject();
            if (obj is IEnumerable enumerable) return enumerable.ToExpandoObject();
            if (obj is DataTable dataTable) return dataTable.ToExpandoObject();
            if (!objType.IsClass) return obj;
            ExpandoObject result = new();
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (propertyInfo.GetCustomAttribute<IgnoreToExpandoObjectAttribute>() is not null) continue;
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
        public static string ToExpandoObject(this string stringValue) => stringValue;
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this XmlDocument document) => document.ChildNodes.ToExpandoObject();
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="xmlNodes"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this XmlNodeList xmlNodes)
        {
            List<string> names = [];
            bool isArray = false;
            foreach (XmlNode xmlNode in xmlNodes)
            {
                if (names.Contains(xmlNode.Name))
                {
                    isArray = true;
                    break;
                }
                names.Add(xmlNode.Name);
            }
            if (!isArray)
            {
                ExpandoObject result = new();
                foreach (XmlNode xmlNode in xmlNodes)
                {
                    result.TryAdd(xmlNode.Name, xmlNode.ToExpandoObject());
                }
                return result;
            }
            else
            {
                List<object?> result = [];
                foreach (XmlNode xmlNode in xmlNodes)
                {
                    result.Add(xmlNode.ToExpandoObject());
                }
                return result;
            }
        }
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this XmlNode xmlNode)
        {
            if (xmlNode.ChildNodes.Count > 0)
            {
                if (xmlNode.ChildNodes.Count == 1 && xmlNode.FirstChild is XmlText xmlText) return xmlText.Value?.ToExpandoObject();
                object? value = xmlNode.ChildNodes.ToExpandoObject();
                return value;
            }
            else
            {
                return xmlNode.Value?.ToExpandoObject();
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
            if (enumerable is byte[] bytes && bytes.Length == 16)
            {
                try
                {
                    return new Guid(bytes);
                }
                catch { }
            }
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
            List<string> keys = [];
            foreach (object? keyObj in dictionary.Keys)
            {
                string? key = keyObj is string keyValue ? keyValue : keyObj.ToString();
                if (key is null) continue;
                if (keys.Contains(key)) continue;
            }
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
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static object? ToExpandoObject(this DataTable dataTable)
        {
            List<ExpandoObject> list = [];
            foreach (DataRow row in dataTable.Rows)
            {
                ExpandoObject value = row.ToExpandoObject(dataTable.Columns);
                list.Add(value);
            }
            return list;
        }
        /// <summary>
        /// 转换为动态对象
        /// </summary>
        /// <param name="row"></param>
        /// <param name="dataColumns"></param>
        /// <returns></returns>
        public static ExpandoObject ToExpandoObject(this DataRow row, DataColumnCollection dataColumns)
        {
            ExpandoObject result = new();
            for (int i = 0; i < dataColumns.Count; i++)
            {
                object value = row[i];
                result.TryAdd(dataColumns[i].ColumnName, value.ToExpandoObject());
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
