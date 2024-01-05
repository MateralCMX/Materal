using System.Collections;
using System.Data;
using System.Text.RegularExpressions;

namespace Materal.Logger
{
    /// <summary>
    /// 日志域
    /// </summary>
    public class LoggerScope(string scopeName) : IDisposable
    {
        private readonly Logger? _logger;
        /// <summary>
        /// 域
        /// </summary>
        public string ScopeName { get; set; } = scopeName;
        /// <summary>
        /// 是否为高级域
        /// </summary>
        public bool IsAdvancedScope => AdvancedScope != null;
        /// <summary>
        /// 高级域对象
        /// </summary>
        public AdvancedScope? AdvancedScope { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scope"></param>
        public LoggerScope(AdvancedScope scope) : this(scope.ScopeName) => AdvancedScope = scope;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scopeName"></param>
        /// <param name="logger"></param>
        public LoggerScope(string scopeName, Logger logger) : this(scopeName) => _logger = logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="logger"></param>
        public LoggerScope(AdvancedScope scope, Logger logger) : this(scope.ScopeName, logger)
        {
            AdvancedScope = scope;
        }
        /// <summary>
        /// 处理文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string HandlerText(string text)
        {
            if (AdvancedScope is null || AdvancedScope.ScopeData is null || string.IsNullOrWhiteSpace(text)) return text;
            string result = text;
            foreach (KeyValuePair<string, object?> item in AdvancedScope.ScopeData)
            {
                result = HandlerText(result, item.Key, item.Value);
            }
            return result;
        }
        /// <summary>
        /// 处理文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string HandlerText(string text, string key, object? obj)
        {
            if (obj is null) return text;
            if (obj is string stringValue) return HandlerText(text, key, stringValue);
            if (obj is IEnumerable enumerable) return HandlerText(text, key, enumerable);
            if (obj is DataTable dataTable) return HandlerText(text, key, dataTable);
            if (!obj.GetType().IsClass) return HandlerText(text, key, obj.ToString());
            string result = text;
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (!propertyInfo.CanRead) continue;
                object? propertyValue = propertyInfo.GetValue(obj);
                result = HandlerText(result, $"{key}.{propertyInfo.Name}", propertyValue);
            }
            return result;
        }
        /// <summary>
        /// 处理文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        private static string HandlerText(string text, string key, string? stringValue)
        {
            if(stringValue is null) return text;
            string result = Regex.Replace(text, $@"\${{{key}}}", stringValue);
            return result;
        }
        /// <summary>
        /// 处理文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        private static string HandlerText(string text, string key, IEnumerable enumerable)
        {
            if (enumerable is null) return text;
            if (enumerable is IDictionary dictionary) return HandlerText(text, key, dictionary);
            if (enumerable is byte[] bytes && bytes.Length == 16)
            {
                try
                {
                    return HandlerText(text, key, new Guid(bytes).ToString());
                }
                catch { }
            }
            string result = text;
            int index = 0;
            foreach (object item in enumerable)
            {
                result = HandlerText(result, $"{key}\\[{index}\\]", item);
                index++;
            }
            return result;
        }
        /// <summary>
        /// 处理文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private static string HandlerText(string text, string key, IDictionary dictionary)
        {
            List<string> keys = [];
            foreach (object? keyObj in dictionary.Keys)
            {
                string? dicKey = keyObj is string keyValue ? keyValue : keyObj.ToString();
                if (key is null) continue;
                if (keys.Contains(key)) continue;
            }
            string result = text;
            foreach (object? item in dictionary)
            {
                if (item is null || item is not DictionaryEntry dictionaryEntry) continue;
                object keyObj = dictionaryEntry.Key;
                string? dicKey = keyObj is string keyValue ? keyValue : keyObj.ToString();
                if (key is null) continue;
                result = HandlerText(result, $"{key}.{dicKey}", dictionaryEntry.Value);
            }
            return result;
        }
        /// <summary>
        /// 处理文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private static string HandlerText(string text, string key, DataTable dataTable)
        {
            string result = text;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    DataColumn dataColumn = dataTable.Columns[j];
                    result = HandlerText(result, $"{key}.\\[{i}\\]\\[{j}\\]", dataRow[dataColumn]);
                    result = HandlerText(result, $"{key}.\\[{i}\\].{dataColumn.ColumnName}", dataRow[dataColumn]);
                }
            }
            return result;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            AdvancedScope = null;
            _logger?.ExitScope();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 克隆域
        /// </summary>
        /// <returns></returns>
        public LoggerScope CloneScope()
        {
            AdvancedScope? advancedScope = null;
            if (AdvancedScope is not null)
            {
                advancedScope = new(AdvancedScope.ScopeName);
                foreach (KeyValuePair<string, object?> item in AdvancedScope.ScopeData)
                {
                    advancedScope.ScopeData.Add(item.Key, item.Value);
                }
            }
            return advancedScope is null ? new LoggerScope(ScopeName) : new LoggerScope(advancedScope);
        }
    }
}
