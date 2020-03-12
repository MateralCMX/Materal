using System.Threading.Tasks;
using DotNetty.Codecs.Http;

namespace Materal.DotNetty.ControllerBus.Filters
{
    public interface IControllerBeforeAsyncFilter : IFilter
    {
        /// <summary>
        /// 处理过滤器
        /// </summary>
        /// <param name="baseController"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        Task HandlerFilterAsync(BaseController baseController, IFullHttpRequest request, ref IFullHttpResponse response);
    }
}
