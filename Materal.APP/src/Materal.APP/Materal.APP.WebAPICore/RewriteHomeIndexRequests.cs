using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using System;

namespace Materal.APP.WebAPICore
{
    /// <summary>
    /// 主页重定向(重写方式)
    /// </summary>
    public class RewriteHomeIndexRequests : IRule
    {
        private readonly string _targetUrl;

        public RewriteHomeIndexRequests(string targetUrl)
        {
            _targetUrl = targetUrl;
        }
        public void ApplyRule(RewriteContext context)
        {
            HttpRequest request = context.HttpContext.Request;
            if (!request.Path.Value.Equals("/") || !request.Method.Equals("Get", StringComparison.OrdinalIgnoreCase)) return;
            context.Result = RuleResult.SkipRemainingRules;
            request.Path = _targetUrl;
        }
    }
}
