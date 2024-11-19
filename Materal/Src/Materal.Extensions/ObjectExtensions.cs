using System.Data;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 对象是否为空或者空字符串
        /// </summary>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyString(this object inputObj) => inputObj switch
        {
            null => true,
            string inputStr => string.IsNullOrEmpty(inputStr),
            _ => false,
        };
        /// <summary>
        /// 对象是否为空或者空或者空格字符串
        /// </summary>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpaceString(this object inputObj) => inputObj switch
        {
            null => true,
            string inputStr => string.IsNullOrWhiteSpace(inputStr),
            _ => false,
        };
        /// <summary>
        /// 对象是否相等
        /// </summary>
        /// <param name="aModel"></param>
        /// <param name="bModel"></param>
        /// <param name="maps"></param>
        /// <returns></returns>
        public static bool Equals(this object aModel, object bModel, Dictionary<string, Func<object?, bool>> maps)
        {
            Type aType = aModel.GetType();
            Type bType = bModel.GetType();
            foreach (PropertyInfo aProperty in aType.GetProperties())
            {
                object? aValue = aProperty.GetValue(aModel);
                if (maps.TryGetValue(aProperty.Name, out Func<object?, bool>? value))
                {
                    bool mapResult = value.Invoke(aValue);
                    if (!mapResult) return false;
                }
                else
                {
                    PropertyInfo? bProperty = bType.GetProperty(aProperty.Name);
                    if (bProperty is null || aProperty.PropertyType != bProperty.PropertyType) return false;
                    object? bValue = bProperty.GetValue(bModel);
                    if (aValue != bValue) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T? GetObjectValue<T>(this object obj, string name)
        {
            object? resultObj = GetObjectValue(obj, name);
            if (resultObj is null || resultObj is not T result) return default;
            return result;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static T? GetObjectValue<T>(this object obj, params string[] names)
        {
            object? resultObj = GetObjectValue(obj, names);
            if (resultObj is null || resultObj is not T result) return default;
            return result;
        }
#if NET
        /// <summary>
        /// 模版表达式
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"\[\d+\]")]
        private static partial Regex ExpressionRegex();
#endif
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object? GetObjectValue(this object obj, string name)
        {
            ICollection<string> trueNames = name.Split('.');
            if (trueNames.Count == 1)
            {
#if NET
                Regex regex = ExpressionRegex();
#else
                Regex regex = new(@"\[\d+\]");
#endif
                string trueName = name;
                MatchCollection matchCollection = regex.Matches(trueName);
                if (matchCollection.Count > 0)
                {
                    List<string> tempNames = [];
                    foreach (object? matchItem in matchCollection)
                    {
                        if (matchItem is not Match match) continue;
                        tempNames.Add(match.Value[1..^1]);
                        trueName = trueName.Replace(match.Value, string.Empty);
                    }
                    if (!string.IsNullOrWhiteSpace(trueName))
                    {
                        tempNames.Insert(0, trueName);
                    }
                    return obj.GetObjectValue(tempNames);
                }
                if (obj is IDictionary<string, object> dicObj) return dicObj.GetObjectDictionaryValue(trueName);
                if (obj is IDictionary dic) return dic.GetObjectValue(trueName);
                if (obj is IList list) return list.GetObjectValue(trueName);
                if (obj is ICollection collection) return collection.GetObjectValue(trueName);
                if (obj is DataTable dt) return dt.GetObjectValue(trueName);
                if (obj is DataRow dr) return dr.GetObjectValue(trueName);
                PropertyInfo? propertyInfo = obj.GetType().GetRuntimeProperty(trueName);
                if (propertyInfo is not null && propertyInfo.CanRead)
                {
                    return propertyInfo.GetValue(obj);
                }
                FieldInfo? fieldInfo = obj.GetType().GetRuntimeField(trueName);
                if (fieldInfo is not null)
                {
                    return fieldInfo.GetValue(obj);
                }
                return null;
            }
            else
            {
                return obj.GetObjectValue(trueNames);
            }
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static object? GetObjectValue(this object obj, ICollection<string> names)
        {
            object? currentObj = obj;
            foreach (string name in names)
            {
                currentObj = currentObj?.GetObjectValue(name);
                if (currentObj is null) break;
            }
            return currentObj;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static DataRow? GetObjectValue(this DataTable dt, string name)
            => int.TryParse(name, out int targetIndex) && targetIndex >= 0 && targetIndex < dt.Rows.Count ? dt.Rows[targetIndex] : null;
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object? GetObjectValue(this DataRow dr, string name)
        {
            if (dr.Table.Columns.Contains(name))
            {
                return dr[name];
            }
            else if (int.TryParse(name, out int targetIndex) && targetIndex >= 0 && targetIndex < dr.ItemArray?.Length)
            {
                return dr[targetIndex];
            }
            return null;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object? GetObjectValue(this IList list, string name)
        {
            if (!int.TryParse(name, out int targetIndex)) return null;
            return targetIndex >= 0 && targetIndex < list.Count ? list[targetIndex] : null;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object? GetObjectValue(this ICollection collection, string name)
        {
            if (!int.TryParse(name, out int targetIndex)) return null;
            if (targetIndex >= 0 && targetIndex < collection.Count)
            {
                int index = 0;
                foreach (object? item in collection)
                {
                    if (index++ != targetIndex) continue;
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object? GetObjectValue(this IDictionary dic, string name)
        {
            foreach (object? item in dic.Keys)
            {
                if (item is not null && item.Equals(name)) return dic[item];
            }
            return null;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object? GetObjectDictionaryValue(this IDictionary<string, object> dic, string name) => dic.TryGetValue(name, out object? value) ? value : null;
    }
}
