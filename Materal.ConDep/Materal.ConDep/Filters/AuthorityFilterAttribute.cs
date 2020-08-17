using System;
using DotNetty.Codecs.Http;
using Materal.DotNetty.Common;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.ControllerBus.Filters;

namespace Materal.ConDep.Filters
{
    public class AuthorityFilterAttribute : BaseAuthorityFilterAttribute
    {
        public override void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response)
        {
            if (!(baseController is ConDepBaseController configCenterBaseController)) return;
            try
            {
                Guid loginUserID = configCenterBaseController.GetLoginUserID();
                if (loginUserID == Guid.Empty)
                {
                    response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.Unauthorized);
                }
            }
            catch (MateralConDepException)
            {
                response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.Unauthorized);
            }
        }
    }
}
