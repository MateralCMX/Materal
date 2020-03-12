using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Materal.DotNetty.Server.Core;
using Materal.DotNetty.Common;

namespace Materal.DotNetty.Server.CoreImpl
{
    public class FileHandler : HttpHandlerContext
    {
        /// <summary>
        /// Html页面文件夹路径
        /// </summary>
        public static string HtmlPageFolderPath { get; set; } = "HtmlPages";
        public override async Task HandlerAsync(IChannelHandlerContext ctx, IByteBufferHolder byteBufferHolder)
        {
            try
            {
                await HandlerAsync<IFullHttpRequest>(ctx, byteBufferHolder, HandlerRequestAsync);
            }
            catch (Exception exception)
            {
                ShowException?.Invoke(exception);
                Dictionary<AsciiString, object> headers = HttpResponseHelper.GetDefaultHeaders("text/plain;charset=utf-8");
                IFullHttpResponse response = HttpResponseHelper.GetHttpResponse(HttpResponseStatus.InternalServerError, exception.Message, headers);
                await SendHttpResponseAsync(ctx, byteBufferHolder, response);
            }
        }
        #region 私有方法
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task HandlerRequestAsync(IChannelHandlerContext ctx, IFullHttpRequest request)
        {
            IFullHttpResponse response = await GetFileResponseAsync(request);
            if(!CanNext || response.Status.Code != HttpResponseStatus.OK.Code)
            {
                await SendHttpResponseAsync(ctx, request, response);
                StopHandler();
            }
        }
        /// <summary>
        /// 获得文件Byte数组
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        private async Task<byte[]> GetFileBytesAsync(string filePath)
        {
            if (!File.Exists(filePath)) throw new DotNettyServerException("文件不存在");
            byte[] result = await File.ReadAllBytesAsync(filePath);
            return result;
        }
        /// <summary>
        /// 获得文件返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<IFullHttpResponse> GetFileResponseAsync(IFullHttpRequest request)
        {
            string url = request.Uri == "/" ? "/Index.html" : request.Uri;
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}{HtmlPageFolderPath}{url}";
            string extension = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(extension)) return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.NotFound);
            if (!File.Exists(filePath)) return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.NotFound);
            byte[] body = await GetFileBytesAsync(filePath);
            string contentType = MIMEManager.GetContentType(extension);
            Dictionary<AsciiString, object> headers = HttpResponseHelper.GetDefaultHeaders(contentType);
            return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, body, headers);
        }
        #endregion
    }
}
