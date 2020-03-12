using DotNetty.Codecs.Http;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class BaseControllerAfterAsyncFilterAttribute : Attribute, IControllerAfterAsyncFilter
    {
        public abstract Task HandlerFilterAsync(BaseController baseController, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
