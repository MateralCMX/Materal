using Microsoft.AspNetCore.Http;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.Custom
{
    public class DefaultCustomHandler : ICustomHandler
    {
        public virtual Response<HttpResponseMessage?> AfterTransmit(HttpContext httpContext)
        {
            Response<HttpResponseMessage?> result = new OkResponse<HttpResponseMessage?>(null);
            return result;
        }
        public virtual Task<Response<HttpResponseMessage?>> AfterTransmitAsync(HttpContext httpContext) => Task.FromResult(AfterTransmit(httpContext));
        public virtual Response<HttpResponseMessage?> BeforeTransmit(HttpContext httpContext)
        {
            Response<HttpResponseMessage?> result = new OkResponse<HttpResponseMessage?>(null);
            return result;
        }
        public virtual Task<Response<HttpResponseMessage?>> BeforeTransmitAsync(HttpContext httpContext) => Task.FromResult(BeforeTransmit(httpContext));
    }
}
