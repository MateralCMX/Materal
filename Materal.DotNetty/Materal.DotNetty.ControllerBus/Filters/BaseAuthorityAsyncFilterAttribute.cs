using DotNetty.Codecs.Http;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public abstract class BaseAuthorityAsyncFilterAttribute : Attribute, IAuthorityAsyncFilter
    {
        public abstract Task HandlerFilterAsync(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
