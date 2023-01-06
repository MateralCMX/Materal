using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;

namespace RC.Core.WebAPI
{
    /// <summary>
    /// 主页重定向(重写方式)
    /// </summary>
    public class RewriteHomeIndexRequests : IRule
    {
        private readonly string _targetUrl;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="targetUrl"></param>
        public RewriteHomeIndexRequests(string targetUrl)
        {
            _targetUrl = targetUrl;
        }
        /// <summary>
        /// 应用规则
        /// </summary>
        /// <param name="context"></param>
        public void ApplyRule(RewriteContext context)
        {
            HttpRequest request = context.HttpContext.Request;
            if (request.Path.Value != null && (!request.Path.Value.Equals("/") || !request.Method.Equals("Get", StringComparison.OrdinalIgnoreCase))) return;
            context.Result = RuleResult.SkipRemainingRules;
            request.Path = _targetUrl;
        }
    }
}
