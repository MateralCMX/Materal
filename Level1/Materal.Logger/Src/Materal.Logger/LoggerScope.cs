using System.Text.RegularExpressions;

namespace Materal.Logger
{
    /// <summary>
    /// 日志域
    /// </summary>
    public class LoggerScope : IDisposable
    {
        private readonly Logger _logger;
        /// <summary>
        /// 域
        /// </summary>
        public string ScopeName { get; set; }
        /// <summary>
        /// 是否为高级域
        /// </summary>
        public bool IsAdvancedScope => _advancedScope != null;
        /// <summary>
        /// 高级域对象
        /// </summary>
        private AdvancedScope? _advancedScope;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scopeName"></param>
        /// <param name="logger"></param>
        public LoggerScope(string scopeName, Logger logger)
        {
            ScopeName = scopeName;
            _logger = logger;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="logger"></param>
        public LoggerScope(AdvancedScope scope, Logger logger) : this(scope.ScopeName, logger)
        {
            _advancedScope = scope;
        }
        /// <summary>
        /// 处理文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string HandlerText(string value)
        {
            if (_advancedScope is null || _advancedScope.ScopeData is null || string.IsNullOrWhiteSpace(value)) return value;
            string result = value;
            foreach (KeyValuePair<string, string> item in _advancedScope.ScopeData)
            {
                result = Regex.Replace(result, $@"\$\{{{item.Key}\}}", item.Value);
            }
            return result;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _advancedScope = null;
            _logger.ExitScope();
            GC.SuppressFinalize(this);
        }
    }
}
