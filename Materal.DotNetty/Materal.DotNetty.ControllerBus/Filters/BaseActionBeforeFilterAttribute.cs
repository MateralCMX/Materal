using DotNetty.Codecs.Http;
using System;

namespace Materal.DotNetty.ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseActionBeforeFilterAttribute : Attribute, IActionBeforeFilter
    {

        public abstract void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
