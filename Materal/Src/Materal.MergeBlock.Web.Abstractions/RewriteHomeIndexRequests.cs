using Microsoft.AspNetCore.Rewrite;

namespace Materal.MergeBlock.Web.Abstractions
{
    /// <summary>
    /// 主页重定向(重写方式)
    /// </summary>
    public class RewriteHomeIndexRequests(string targetUrl) : IRule
    {
        /// <summary>
        /// 应用规则
        /// </summary>
        /// <param name="context"></param>
        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            if (request.Path.Value != null && (!request.Path.Value.Equals("/") || !request.Method.Equals("Get", StringComparison.OrdinalIgnoreCase))) return;
            context.Result = RuleResult.SkipRemainingRules;
            request.Path = targetUrl;
        }
    }
}
