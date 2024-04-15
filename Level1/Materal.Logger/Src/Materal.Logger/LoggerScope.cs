using System.Text.RegularExpressions;

namespace Materal.Logger
{
    /// <summary>
    /// 日志作用域
    /// </summary>
    public partial class LoggerScope
    {
        /// <summary>
        /// 域名称
        /// </summary>
        public string ScopeName { get; } = "PublicScope";
        /// <summary>
        /// 域数据
        /// </summary>
        public Dictionary<string, object?> ScopeData { get; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerScope(string name, Dictionary<string, object?> scopeData)
        {
            ScopeName = name;
            ScopeData = scopeData;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerScope(object? scope)
        {
            if (scope is null) return;
            switch (scope)
            {
                case LoggerScope self:
                    ScopeName = self.ScopeName;
                    ScopeData = self.ScopeData;
                    break;
                case string stringScope:
                    ScopeName = stringScope;
                    break;
                case Dictionary<string, object?> scopeData:
                    ScopeData = scopeData;
                    break;
                default:
                    ScopeName = scope.GetType().Name;
                    break;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerScope(IExternalScopeProvider externalScopeProvider)
        {
            List<string> scopeNames = [];
            externalScopeProvider.ForEachScope((m, scope) =>
            {
                if(m is not LoggerScope loggerScope) return;
                scopeNames.Add(loggerScope.ScopeName);
                foreach (KeyValuePair<string, object?> item in loggerScope.ScopeData)
                {
                    if (ScopeData.ContainsKey(item.Key))
                    {
                        ScopeData[item.Key] = item.Value;
                    }
                    else
                    {
                        ScopeData.Add(item.Key, item.Value);
                    }
                }
            }, this);
            scopeNames = scopeNames.Distinct().ToList();
            if(scopeNames.Count > 0)
            {
                ScopeName = string.Join(".", scopeNames);
            }
        }
#if NET8_0_OR_GREATER
        /// <summary>
        /// 模版表达式
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"\$\{[^\}]+\}")]
        private static partial Regex ExpressionRegex();
#endif
        /// <summary>
        /// 应用文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string ApplyText(string text)
        {
            if (ScopeData.Count <= 0) return text;
            string result = text;
            result = Regex.Replace(result, @"\$\{Scope\}", ScopeName);
#if NETSTANDARD
            Regex regex = new(@"\$\{[^\}]+\}");
#else
            Regex regex = ExpressionRegex();
#endif
            MatchCollection matchCollection = regex.Matches(result);
            foreach (object? matchItem in matchCollection)
            {
                if (matchItem is not Match match) continue;
                string valueName = match.Value[2..^1];
                object? value = ScopeData.GetObjectValue(valueName);
                if (value is null) continue;
                else if (value is string strinvValue)
                {
                    result = result.Replace(match.Value, strinvValue);
                }
                else
                {
                    result = result.Replace(match.Value, value?.ToJson());
                }
            }
            return result;
        }
    }
}
