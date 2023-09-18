//using Microsoft.Extensions.Logging;
//using System.Text.RegularExpressions;

//namespace Materal.Logger
//{
//    /// <summary>
//    /// 日志域
//    /// </summary>
//    [Serializable]
//    public class LoggerScope : IDisposable
//    {
//        private readonly Logger? _logger;
//        /// <summary>
//        /// 域
//        /// </summary>
//        public string ScopeName { get; set; }
//        /// <summary>
//        /// 是否为高级域
//        /// </summary>
//        public bool IsAdvancedScope => AdvancedScope != null;
//        /// <summary>
//        /// 高级域对象
//        /// </summary>
//        public AdvancedScope? AdvancedScope { get; set; }
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="scopeName"></param>
//        public LoggerScope(string scopeName)
//        {
//            ScopeName = scopeName;
//        }
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="scope"></param>
//        public LoggerScope(AdvancedScope scope) : this(scope.ScopeName)
//        {
//            AdvancedScope = scope;
//        }
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="scopeName"></param>
//        /// <param name="logger"></param>
//        public LoggerScope(string scopeName, Logger logger) : this(scopeName)
//        {
//            _logger = logger;
//        }
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="scope"></param>
//        /// <param name="logger"></param>
//        public LoggerScope(AdvancedScope scope, Logger logger) : this(scope.ScopeName, logger)
//        {
//            AdvancedScope = scope;
//        }
//        /// <summary>
//        /// 处理文本
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public string HandlerText(string value)
//        {
//            if (AdvancedScope is null || AdvancedScope.ScopeData is null || string.IsNullOrWhiteSpace(value)) return value;
//            string result = value;
//            foreach (KeyValuePair<string, string> item in AdvancedScope.ScopeData)
//            {
//                result = Regex.Replace(result, $@"\$\{{{item.Key}\}}", item.Value);
//            }
//            return result;
//        }
//        /// <summary>
//        /// 释放
//        /// </summary>
//        public void Dispose()
//        {
//            AdvancedScope = null;
//            _logger?.ExitScope();
//            GC.SuppressFinalize(this);
//        }
//    }
//}
