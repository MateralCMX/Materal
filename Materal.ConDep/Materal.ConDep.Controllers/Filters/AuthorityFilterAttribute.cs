using DotNetty.Codecs.Http;
using Materal.ConDep.Common;
using Materal.ConDep.ControllerCore;
using Materal.ConDep.Services;
using Materal.DotNetty.Common;
using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.ControllerBus.Filters;

namespace Materal.ConDep.Controllers.Filters
{
    public class AuthorityFilterAttribute : BaseAuthorityFilterAttribute
    {
        public override void HandlerFilter(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response)
        {
            if (baseController is ConDepBaseController conDepBaseController)
            {
                var authorityService = ApplicationService.GetService<IAuthorityService>();
                if (authorityService.IsLogin(conDepBaseController.GetToken()))
                {
                    return;
                }
            }
            response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.Unauthorized);
        }
    }
}
