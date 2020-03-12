using DotNetty.Codecs.Http;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseActionAfterAsyncFilterAttribute : Attribute, IActionAfterAsyncFilter
    {
        public abstract Task HandlerFilterAsync(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
