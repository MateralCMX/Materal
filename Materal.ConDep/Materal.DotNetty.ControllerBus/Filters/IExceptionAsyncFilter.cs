using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.ControllerBus.Filters
{
    public interface IExceptionAsyncFilter : IFilter
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="byteBufferHolder"></param>
        /// <param name="exception"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        Task HandlerExceptionAsync(IByteBufferHolder byteBufferHolder, Exception exception, ref IFullHttpResponse response);
    }
}
