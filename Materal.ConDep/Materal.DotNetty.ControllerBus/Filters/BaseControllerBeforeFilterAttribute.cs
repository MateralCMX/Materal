using System;
using DotNetty.Codecs.Http;

namespace Materal.DotNetty.ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class BaseControllerBeforeFilterAttribute : Attribute, IControllerBeforeFilter
    {
        public abstract void HandlerFilter(BaseController baseController, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
