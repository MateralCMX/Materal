using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Materal.DotNetty.Common;
using Materal.DotNetty.Server.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Materal.CacheHelper;

namespace Materal.DotNetty.Server.CoreImpl
{
    public class FileHandler : HttpHandlerContext
    {
        private readonly ICacheManager _cacheManager;
        private const string _cacheKey = "FileCacheKey";

        public FileHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void ClearCache()
        {
            List<string> cacheKeys = _cacheManager.GetCacheKeys();
            foreach (string cacheKey in cacheKeys)
            {
                if (cacheKey.StartsWith(_cacheKey))
                {
                    _cacheManager.Remove(cacheKey);
                }
            }
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCache(string key)
        {
            string cacheKey = $"{_cacheKey}{key}";
            if (_cacheManager.GetCacheKeys().Contains(cacheKey))
            {
                _cacheManager.Remove(cacheKey);
            }
        }
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
                OnException?.Invoke(exception);
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
        protected virtual async Task HandlerRequestAsync(IChannelHandlerContext ctx, IFullHttpRequest request)
        {
            IFullHttpResponse response = await GetFileResponseAsync(request);
            if (!CanNext || response.Status.Code == HttpResponseStatus.OK.Code)
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
        protected virtual async Task<byte[]> GetFileBytesAsync(string filePath)
        {
            var result = _cacheManager.Get<byte[]>($"{_cacheKey}{filePath}");
            if (result != null) return result;
            if (!File.Exists(filePath)) throw new DotNettyServerException("文件不存在");
            result = await File.ReadAllBytesAsync(filePath);
            _cacheManager.SetByAbsolute($"{_cacheKey}{filePath}", result, 1);
            return result;
        }
        /// <summary>
        /// 获得文件返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual async Task<IFullHttpResponse> GetFileResponseAsync(IFullHttpRequest request)
        {
            string url = string.IsNullOrEmpty(Path.GetExtension(request.Uri)) ? Path.Combine(request.Uri, "Index.html") : request.Uri;
            return await GetFileResponseAsync(url);
        }
        /// <summary>
        /// 获得文件返回
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected virtual async Task<IFullHttpResponse> GetFileResponseAsync(string url)
        {
            if (url.StartsWith("/") || url.StartsWith(@"\"))
            {
                url = url.Substring(1);
            }
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HtmlPageFolderPath, url);
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
