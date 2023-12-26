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
        /// <param name="value"></param>
        /// <returns></returns>
        public string HandlerText(string value)
        {
            if (AdvancedScope is null || AdvancedScope.ScopeData is null || string.IsNullOrWhiteSpace(value)) return value;
            string result = value;
            foreach (KeyValuePair<string, object?> item in AdvancedScope.ScopeData)
            {
                string scopeDataValue = string.Empty;
                if (item.Value is not null && item.Value.IsNullOrWhiteSpaceString())
                {
                    scopeDataValue = item.Value is string stringValue ? stringValue : item.Value.ToString() ?? string.Empty;
                }
                result = Regex.Replace(result, $@"\$\{{{item.Key}\}}", scopeDataValue);
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
