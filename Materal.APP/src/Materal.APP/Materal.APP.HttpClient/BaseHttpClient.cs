using Materal.ConvertHelper;
using Materal.Model;
using Materal.NetworkHelper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Materal.APP.HttpClient
{
    public abstract class BaseHttpClient
    {
        private readonly IAuthorityManage _authorityManage; 

        protected BaseHttpClient(IAuthorityManage authorityManage)
        {
            _authorityManage = authorityManage;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendGetAsync<T>(string url, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendGetAsync(GetUrl(url), queryParams, httpHeaders), contentType);
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendPostAsync<T>(string url, object data = null, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendPostAsync(GetUrl(url), data, queryParams, httpHeaders), contentType);
        }

        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendPutAsync<T>(string url, object data = null, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendPutAsync(GetUrl(url), data, queryParams, httpHeaders), contentType);
        }

        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendDeleteAsync<T>(string url, object data = null, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendDeleteAsync(GetUrl(url), data, queryParams, httpHeaders), contentType);
        }

        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendPatchAsync<T>(string url, object data = null, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendPatchAsync(GetUrl(url), data, queryParams, httpHeaders), contentType);
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpContent"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendPostAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendPostAsync(GetUrl(url), httpContent, queryParams, httpHeaders), contentType);
        }

        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpContent"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendPutAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendPutAsync(GetUrl(url), httpContent, queryParams, httpHeaders), contentType);
        }

        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpContent"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendDeleteAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendDeleteAsync(GetUrl(url), httpContent, queryParams, httpHeaders), contentType);
        }

        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpContent"></param>
        /// <param name="queryParams"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        protected async Task<T> SendPatchAsync<T>(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, string contentType = "application/json") where T : ResultModel
        {
            return await SendAsync<T>(async httpHeaders => await HttpManager.SendPatchAsync(GetUrl(url), httpContent, queryParams, httpHeaders), contentType);
        }
        /// <summary>
        /// 获得Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected abstract string GetUrl(string url);
        /// <summary>
        /// 获得Http头
        /// </summary>
        /// <returns></returns>
        protected async Task<Dictionary<string, string>> GetHttpHeadersAsync(string contentType)
        {
            var httpHeaders = new Dictionary<string, string>
            {
                ["Accept"] = "*"
            };
            if (!string.IsNullOrWhiteSpace(contentType))
            {
                httpHeaders["Content-Type"] = contentType;
            }
            if (!await _authorityManage.IsLoginAsync()) return httpHeaders;
            string token = await _authorityManage.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
            {
                httpHeaders.Add("Authorization", $"Bearer {token}");
            }
            return httpHeaders;
        }
        #region 私有方法
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private async Task<T> SendAsync<T>(Func<Dictionary<string, string>, Task<string>> action, string contentType) where T: ResultModel
        {
            Dictionary<string, string> httpHeaders = await GetHttpHeadersAsync(contentType);
            string resultJson = await action(httpHeaders);
            return resultJson.JsonToObject<T>();
        }
        #endregion
    }
}
