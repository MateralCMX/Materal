using DotNetty.Codecs.Http;
using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Common.Utilities;

namespace Materal.DotNetty.ControllerBus.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public abstract class BaseAuthorityFilterAttribute : Attribute, IAuthorityFilter
    {
        public abstract void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
