using System;
using DotNetty.Codecs.Http;

namespace Materal.DotNetty.ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseActionAfterFilterAttribute : Attribute, IActionAfterFilter
    {
        public abstract void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
