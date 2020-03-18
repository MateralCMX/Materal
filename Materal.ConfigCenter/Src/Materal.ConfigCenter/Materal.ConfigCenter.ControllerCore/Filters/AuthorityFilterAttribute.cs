using System;
using DotNetty.Codecs.Http;
using Materal.DotNetty.Common;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.ControllerBus.Filters;

namespace Materal.ConfigCenter.ControllerCore.Filters
{
    public class AuthorityFilterAttribute : BaseAuthorityFilterAttribute
    {
        public override void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response)
        {
            if (!(baseController is ConfigCenterBaseController configCenterBaseController)) return;
            try
            {
                Guid loginUserID = configCenterBaseController.GetLoginUserID();
                if (loginUserID == Guid.Empty)
                {
                    response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.Unauthorized);
                }
            }
            catch (MateralConfigCenterException)
            {
                response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.Unauthorized);
            }
        }
    }
}
