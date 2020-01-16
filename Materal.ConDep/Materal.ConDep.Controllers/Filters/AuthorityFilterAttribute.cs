using DotNetty.Codecs.Http;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.ControllerBus.Filters;
using System;

namespace Materal.ConDep.Controllers.Filters
{
    public class AuthorityFilterAttribute : BaseAuthorityFilterAttribute
    {
        public override void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, IFullHttpResponse response)
        {
        }
    }
}
