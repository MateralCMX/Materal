using System.Net;

namespace Materal.Utils.Http
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class HttpHelper : IHttpHelper
    {
        private readonly HttpClient _httpClient;
        /// <summary>
        /// 构造方法
        /// </summary>
        public HttpHelper(HttpClient? httpClient = null)
        {
            httpClient ??= new HttpClient();
            _httpClient = httpClient;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        public async Task<HttpResponseMessage> SendRequestGetResponseMessageAsync(HttpRequestMessage httpRequestMessage)
        {
            try
            {
                HttpResponseMessage result = await _httpClient.SendAsync(httpRequestMessage);
                if (!result.IsSuccessStatusCode) throw new MateralHttpException(httpRequestMessage, result);
                return result;
            }
            catch (MateralHttpException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MateralHttpException(httpRequestMessage, "Http请求错误", ex);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        public async Task<byte[]> SendByBytesAsync(HttpRequestMessage httpRequestMessage)
        {
            using HttpResponseMessage httpResponseMessage = await SendRequestGetResponseMessageAsync(httpRequestMessage);
            return await SendByBytesAsync(httpRequestMessage, httpResponseMessage);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        public async Task<byte[]> SendByBytesAsync(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage)
        {
            try
            {
                using MemoryStream memoryStream = new();
                await httpResponseMessage.Content.CopyToAsync(memoryStream);
                byte[] result = memoryStream.ToArray();
                return result;
            }
            catch (Exception ex)
            {
                throw new MateralHttpException(httpRequestMessage, httpResponseMessage, "处理Response失败", ex);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        public async Task<string> SendAsync(HttpRequestMessage httpRequestMessage)
        {
            using HttpResponseMessage httpResponseMessage = await SendRequestGetResponseMessageAsync(httpRequestMessage);
            byte[] responseContent = await SendByBytesAsync(httpRequestMessage, httpResponseMessage);
            try
            {
                string result = await httpResponseMessage.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new MateralHttpException(httpRequestMessage, httpResponseMessage, "处理Response失败", ex);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="httpContent"></param>
        /// <param name="headers"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        public async Task<string> SendHttpContentAsync(string url, HttpMethod httpMethod, HttpContent? httpContent = null, Dictionary<string, string>? headers = null, Version? httpVersion = null)
        {
            HttpRequestMessage httpRequestMessage;
            try
            {
                httpRequestMessage = new()
                {
                    RequestUri = new Uri(url),
                    Method = httpMethod,
                    Version = httpVersion ?? HttpVersion.Version11
                };
                if (headers is not null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        httpRequestMessage.Headers.Add(header.Key, header.Value);
                    }
                }
                if (httpContent is not null)
                {
                    httpRequestMessage.Content = httpContent;
                }
            }
            catch (Exception ex)
            {
                throw new MateralHttpException("处理Request失败", ex);
            }
            return await SendAsync(httpRequestMessage);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        public async Task<string> SendAsync(string url, HttpMethod httpMethod, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
        {
            HttpContent httpContent;
            encoding ??= Encoding.UTF8;
            try
            {
                if (data is null) return await SendHttpContentAsync(url, httpMethod, null, headers, httpVersion);
                else if (data is HttpContent content) return await SendHttpContentAsync(url, httpMethod, content, headers, httpVersion);
                else if (data is byte[] buffer)
                {
                    MemoryStream memoryStream = new(buffer);
                    httpContent = new StreamContent(memoryStream);
                    httpContent.Headers.Add("Content-Type", "application/octet-stream");
                }
                else if (data is FileInfo fileInfo)
                {
                    fileInfo.Refresh();
                    if (fileInfo.Exists) throw new MateralHttpException("文件不存在");
                    httpContent = new StreamContent(fileInfo.OpenRead());
                    httpContent.Headers.Add("Content-Type", "application/octet-stream");
                }
                else if (data is string stringData)
                {
                    string contentType = "text/plain";
                    if (stringData.IsJson())
                    {
                        contentType = "application/json";
                    }
                    else if (stringData.IsXml())
                    {
                        contentType = "text/xml";
                    }
                    httpContent = new StringContent(stringData, encoding, contentType);
                }
                else
                {
                    httpContent = new StringContent(data.ToJson(), encoding, "application/json");
                }
            }
            catch (MateralHttpException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MateralHttpException("组合HttpContent失败", ex);
            }
            return await SendHttpContentAsync(url, httpMethod, httpContent, headers, httpVersion);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendAsync(string url, HttpMethod httpMethod, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
        {
            string trueUrl = url;
            if (queryArgs is not null && queryArgs.Count > 0)
            {
                try
                {
                    string[] args = queryArgs.Select(m => $"{m.Key}={m.Value}").ToArray();
                    string queryString = "?" + string.Join("&", args);
                    trueUrl += queryString;
                }
                catch (Exception ex)
                {
                    throw new MateralHttpException("组合Url失败", ex);
                }
            }
            return await SendAsync(trueUrl, httpMethod, data, headers, encoding, httpVersion);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendAsync<T>(string url, HttpMethod httpMethod, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
        {
            string trueUrl = url;
            if (queryArgs is not null && queryArgs.Count > 0)
            {
                try
                {
                    string[] args = queryArgs.Select(m => $"{m.Key}={m.Value}").ToArray();
                    string queryString = "?" + string.Join("&", args);
                    trueUrl += queryString;
                }
                catch (Exception ex)
                {
                    throw new MateralHttpException("组合Url失败", ex);
                }
            }
            string httpResult = await SendAsync(trueUrl, httpMethod, data, headers, encoding, httpVersion);
            return GetResultObject<T>(httpResult);
        }
        #region 请求拆分
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendGetAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, HttpMethod.Get, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendPostAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, HttpMethod.Post, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendPutAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, HttpMethod.Put, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendDeleteAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, HttpMethod.Delete, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendTraceAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, HttpMethod.Trace, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendOptionsAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, HttpMethod.Options, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendHeadAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, HttpMethod.Head, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendPatchAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, new HttpMethod("PATCH"), queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<string> SendConnectAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync(url, new HttpMethod("CONNECT"), queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendGetAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, HttpMethod.Get, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendPostAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, HttpMethod.Post, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendPutAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, HttpMethod.Put, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendDeleteAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, HttpMethod.Delete, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendTraceAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, HttpMethod.Trace, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendOptionsAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, HttpMethod.Options, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendHeadAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, HttpMethod.Head, queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendPatchAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, new HttpMethod("PATCH"), queryArgs, data, headers, encoding, httpVersion);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        public async Task<T> SendConnectAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null)
            => await SendAsync<T>(url, new HttpMethod("CONNECT"), queryArgs, data, headers, encoding, httpVersion);
        #endregion
        #region 私有方法
        /// <summary>
        /// 获得返回结果对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpResult"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        private static T GetResultObject<T>(string httpResult)
        {
            if (httpResult.IsJson()) return httpResult.JsonToObject<T>();
            throw new MateralHttpException("暂不支持非Json返回转换");
        }
        #endregion
    }
}
