using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Materal.Gateway.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Materal.Gateway.WebAPI.Filters
{
    public class GatewayAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string path = context.HttpContext.Request.Path.Value;
            if (string.IsNullOrEmpty(path)) return;
            if (!path.StartsWith("/api")) return;
            if (HasAllowAnonymous(context)) return;
            IHeaderDictionary requestHeaders = context.HttpContext.Request.Headers;
            if (requestHeaders.ContainsKey("Authorization"))
            {
                string authorizationValue = requestHeaders["Authorization"].First();
                string authorizationToken = ApplicationConfig.AuthorizationConfig.GetAuthorizationToken();
                if (authorizationToken == authorizationValue) return;
            }
            context.Result = new StatusCodeResult(401);
        }

        private bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            IList<IFilterMetadata> filters = context.Filters;
            if (filters.OfType<IAllowAnonymousFilter>().Any())
            {
                return true;
            }
            Endpoint endpoint = context.HttpContext.GetEndpoint();
            return endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null;
        }
    }
}
