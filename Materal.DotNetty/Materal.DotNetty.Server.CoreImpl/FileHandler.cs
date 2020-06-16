using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Materal.CacheHelper;
using Materal.DateTimeHelper;
using Materal.DotNetty.Common;
using Materal.DotNetty.Server.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Materal.DotNetty.Server.CoreImpl
{
    public class FileHandler : HttpHandlerContext
    {
        private static readonly ConcurrentDictionary<string, object> _readingFiles = new ConcurrentDictionary<string, object>();
        private readonly ICacheManager _cacheManager;
        private const string _cacheKey = "FileCacheKey";
        public static int CacheSecond = 3600;
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
        /// <param name="ifModifiedSince"></param>
        /// <returns></returns>
        protected virtual async Task<(FileCacheModel fileCacheModel, bool isCache)> GetFileBytesAsync(string filePath, ICharSequence ifModifiedSince)
        {
            while (_readingFiles.Any(m => m.Key.Equals(filePath)))
            {
                await Task.Delay(1000);
            }
            if (ifModifiedSince != null)
            {
                return await GetFileBytesFromCacheAsync(filePath, ifModifiedSince);
            }
            FileCacheModel fileCacheModel = await GetFileBytesFromFileAsync(filePath);
            return (fileCacheModel, false);
        }

        /// <summary>
        /// 从缓存获取
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ifModifiedSince"></param>
        /// <returns></returns>
        protected virtual async Task<(FileCacheModel fileCacheModel, bool isCache)> GetFileBytesFromCacheAsync(string filePath, ICharSequence ifModifiedSince)
        {
            var fileCacheModel = _cacheManager.Get<FileCacheModel>($"{_cacheKey}{filePath}");
            if (fileCacheModel == null) return (await GetFileBytesFromFileAsync(filePath), false);
            DateTime ifModifiedSinceValue = ifModifiedSince == null ? fileCacheModel.CacheTime : DateTime.Parse(ifModifiedSince.ToString());
            return fileCacheModel.CacheTime > ifModifiedSinceValue ? (await GetFileBytesFromFileAsync(filePath), false) : (fileCacheModel, true);
        }
        /// <summary>
        /// 从文件获取
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected virtual async Task<FileCacheModel> GetFileBytesFromFileAsync(string filePath)
        {
            _cacheManager.Remove($"{_cacheKey}{filePath}");
            if (!File.Exists(filePath)) throw new DotNettyServerException("文件不存在");
            _readingFiles.TryAdd(filePath, null);
            var fileCacheModel = new FileCacheModel
            {
                CacheTime = DateTime.Now,
                Body = await File.ReadAllBytesAsync(filePath)
            };
            _cacheManager.SetByAbsolute($"{_cacheKey}{filePath}", fileCacheModel, CacheSecond, DateTimeTypeEnum.Second);
            _readingFiles.Remove(filePath, out _);
            return fileCacheModel;
        }
        /// <summary>
        /// 获得文件返回
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual async Task<IFullHttpResponse> GetFileResponseAsync(IFullHttpRequest request)
        {
            string url = HttpUtility.UrlDecode(request.Uri, System.Text.Encoding.UTF8);
            url = string.IsNullOrEmpty(Path.GetExtension(url)) ? Path.Combine(url ?? throw new DotNettyException("Url为空"), "Index.html") : url;
            ICharSequence IfModifiedSince = request.Headers.Get(HttpHeaderNames.IfModifiedSince, null);
            return await GetFileResponseAsync(url, IfModifiedSince);
        }
        /// <summary>
        /// 获得文件返回
        /// </summary>
        /// <param name="url"></param>
        /// <param name="IfModifiedSince"></param>
        /// <returns></returns>
        protected virtual async Task<IFullHttpResponse> GetFileResponseAsync(string url, ICharSequence IfModifiedSince)
        {
            if (url.StartsWith("/") || url.StartsWith(@"\"))
            {
                url = url.Substring(1);
            }
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? throw new DotNettyException("路径为空"), HtmlPageFolderPath, url);
            string extension = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(extension)) return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.NotFound);
            if (!File.Exists(filePath)) return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.NotFound);
            (FileCacheModel fileCacheModel, bool isCache) = await GetFileBytesAsync(filePath, IfModifiedSince);
            string contentType = MIMEManager.GetContentType(extension);
            Dictionary<AsciiString, object> headers = HttpResponseHelper.GetDefaultHeaders(contentType);
            headers.Add(HttpHeaderNames.LastModified, fileCacheModel.CacheTime);
            if (isCache)
            {
                return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.NotModified, headers);
            }
            headers.Add(HttpHeaderNames.Expires, DateTime.Now.AddSeconds(CacheSecond));
            headers.Add(HttpHeaderNames.CacheControl, $"max-age={CacheSecond}");
            return HttpResponseHelper.GetHttpResponse(HttpResponseStatus.OK, fileCacheModel.Body, headers);
        }
        #endregion
    }

    public class FileCacheModel
    {
        /// <summary>
        /// 文件内容
        /// </summary>
        public byte[] Body { get; set; }
        /// <summary>
        /// 缓存时间
        /// </summary>
        public DateTime CacheTime { get; set; }
    }
}
