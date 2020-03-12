using DotNetty.Codecs.Http;
using System.Threading.Tasks;

namespace Materal.DotNetty.ControllerBus.Filters
{
    public interface IAuthorityAsyncFilter : IFilter
    {
        /// <summary>
        /// 处理过滤器
        /// </summary>
        /// <param name="baseController"></param>
        /// <param name="actionInfo"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        Task HandlerFilterAsync(BaseController baseController, ActionInfo actionInfo, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
