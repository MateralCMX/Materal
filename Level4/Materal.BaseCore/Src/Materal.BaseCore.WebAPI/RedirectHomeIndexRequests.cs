using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace Materal.BaseCore.WebAPI
{
    /// <summary>
    /// 主页重定向(重定向方式)
    /// </summary>
    public class RedirectHomeIndexRequests : IRule
    {
        private readonly string _targetUrl;
        private readonly bool _isPermanentlyMoved;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="targetUrl"></param>
        /// <param name="isPermanentlyMoved"></param>
        public RedirectHomeIndexRequests(string targetUrl, bool isPermanentlyMoved = false)
        {
            _targetUrl = targetUrl;
            _isPermanentlyMoved = isPermanentlyMoved;
        }
        /// <summary>
        /// 应用规则
        /// </summary>
        /// <param name="context"></param>
        public void ApplyRule(RewriteContext context)
        {
            HttpRequest request = context.HttpContext.Request;
            if (request.Path.Value != null && (!request.Path.Value.Equals("/") || !request.Method.Equals("Get", StringComparison.OrdinalIgnoreCase))) return;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = _isPermanentlyMoved ? StatusCodes.Status301MovedPermanently : StatusCodes.Status302Found;
            context.Result = RuleResult.EndResponse;
            response.Headers[HeaderNames.Location] = _targetUrl;
        }
    }
}
