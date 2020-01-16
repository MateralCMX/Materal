using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using Materal.DotNetty.ControllerBus;

namespace Materal.ConDep.ControllerCore
{
    public abstract class ConDepBaseController : BaseController
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            ICharSequence authorization = Request.Headers.Get(HttpHeaderNames.Authorization, null);
            return authorization?.ToString();
        }
    }
}
