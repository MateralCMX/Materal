using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace Materal.MergeBlock.Web.Abstractions
{
    /// <summary>
    /// 主页重定向(重定向方式)
    /// </summary>
    public class RedirectHomeIndexRequests(string targetUrl, bool isPermanentlyMoved = false) : IRule
    {
        /// <summary>
        /// 应用规则
        /// </summary>
        /// <param name="context"></param>
        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            if (request.Path.Value != null && (!request.Path.Value.Equals("/") || !request.Method.Equals("Get", StringComparison.OrdinalIgnoreCase))) return;
            var response = context.HttpContext.Response;
            response.StatusCode = isPermanentlyMoved ? StatusCodes.Status301MovedPermanently : StatusCodes.Status302Found;
            context.Result = RuleResult.EndResponse;
            response.Headers[HeaderNames.Location] = targetUrl;
        }
    }
}
