using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Common;
using Materal.DotNetty.Server.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.DotNetty.Server.CoreImpl
{
    public abstract class HttpHandlerContext : ServerHandlerContext
    {
        /// <summary>
        /// 发送Http返回
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="byteBufferHolder"></param>
        /// <param name="response"></param>
        protected virtual async Task SendHttpResponseAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder, IFullHttpResponse response)
        {
            try
            {
                byteBufferHolder.Retain(0);
                if (ctx.Channel.Open)
                {
                    await ctx.Channel.WriteAndFlushAsync(response);
                    await ctx.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DotNettyException("发送HttpResponse失败", ex);
            }
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
