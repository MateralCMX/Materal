using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Common;
using System.Threading.Tasks;
using DotNetty.Common.Utilities;
using Materal.DotNetty.Server.Core;

namespace Materal.DotNetty.Server.CoreImpl
{
    public abstract class HttpHandlerContext : HandlerContext
    {
        /// <summary>
        /// 发送Http返回
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="byteBufferHolder"></param>
        /// <param name="response"></param>
        protected virtual async Task SendHttpResponseAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder, IFullHttpResponse response)
        {
            byteBufferHolder.Retain(0);
            await ctx.Channel.WriteAndFlushAsync(response);
            await ctx.CloseAsync();
        }
        /// <summary>
        /// 获得Options返回
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        protected virtual IFullHttpResponse GetOptionsResponse(string methodName)
        {
            Dictionary<AsciiString, object> headers = HttpResponseHelper.GetDefaultHeaders();
            headers.Add(HttpHeaderNames.AccessControlAllowHeaders, "authorization,access-control-allow-origin,content-type");
            headers.Add(HttpHeaderNames.AccessControlAllowMethods, $"{methodName.ToUpper()}");
            IFullHttpResponse response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, headers);
            return response;
        }
    }
}
