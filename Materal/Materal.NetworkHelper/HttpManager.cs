using Materal.ConvertHelper;
using Materal.NetworkHelper.HeaderHandler;
using Materal.StringHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Materal.NetworkHelper
{
    public static class HttpManager
    {
        public static HttpClient HttpClient { get; set; } = new HttpClient();

        public static DefaultHeaderHandler HeaderHandler { get; set; }

        static HttpManager()
        {
            HeaderHandler = new AuthorizationHeaderHandler();
            var contentTypeHeaderHandler = new ContentTypeHeaderHandler();
            HeaderHandler.SetNext(contentTypeHeaderHandler);
            var defaultHeaderHandler = new DefaultHeaderHandler();
            contentTypeHeaderHandler.SetNext(defaultHeaderHandler);
        }

        /// <summary>
        /// 拼接URL参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="queryParams">参数字典</param>
        /// <returns>带参数的Url地址</returns>
        public static string SpliceUrlParams(string url, Dictionary<string, string> queryParams)
        {
            if (string.IsNullOrEmpty(url) || queryParams == null) return url;
            string[] urlParamsStringList = queryParams.Select(param => $"{param.Key}={param.Value}").ToArray();
            url += $"?{string.Join("&", urlParamsStringList)}";
            return url;
        }

        /// <summary>
        /// 填充Http头部
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static void FillHttpHeaders(HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers)
        {
            HeaderHandler.Handler(httpRequestMessage, headers);
        }

        /// <summary>
        /// 获得FormBytes数据
        /// </summary>
        /// <param name="ms">记忆流</param>
        /// <param name="dataString">数据字符串</param>
        /// <param name="encoding"></param>
        /// <returns>数据流</returns>
        private static HttpContent GetHttpContentByFormDataBytes(Stream ms, string dataString, Encoding encoding)
        {
            byte[] formDataBytes = encoding.GetBytes(dataString);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);
            var content = new StreamContent(ms);
            return content;
        }

        /// <summary>
        /// 默认处理数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string DefaultHandlerData(object data)
        {
            try
            {
                return data.ToJson();
            }
            catch (Exception)
            {
                return data.ToString();
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpRequestMessage"></param>
        /// <param name="headers">headers</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<byte[]> SendByteByHttpRequestMessageAsync(string url, HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers = null)
        {
            using HttpResponseMessage httpResponseMessage = await HttpClient.SendAsync(httpRequestMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new MateralHttpException(url, httpResponseMessage.StatusCode, $"Http请求错误{Convert.ToInt32(httpResponseMessage.StatusCode)}", headers);
            }
            byte[] resultBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();
            return resultBytes;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpMethod"></param>
        /// <param name="httpContent"></param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<byte[]> SendByteByHttpContentAsync(string url, HttpMethod httpMethod, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url), "Url地址不能为空");
            if (!url.IsUrl()) throw new ArgumentException("Url地址错误", nameof(url));
            if (queryParams?.Count > 0)
            {
                url = SpliceUrlParams(url, queryParams);
            }
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(url),
                Content = httpContent
            };
            if (headers != null)
            {
                FillHttpHeaders(httpRequestMessage, headers);
            }
            byte[] resultBytes = await SendByteByHttpRequestMessageAsync(url, httpRequestMessage);
            return resultBytes;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpMethod"></param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<byte[]> SendByteAsync(string url, HttpMethod httpMethod, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url), "Url地址不能为空");
            if (!url.IsUrl()) throw new ArgumentException("Url地址错误", nameof(url));
            encoding ??= Encoding.UTF8;
            if (queryParams?.Count > 0)
            {
                url = SpliceUrlParams(url, queryParams);
            }
            using var httpRequestMessage = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(url),
            };
            if (data != null)
            {
                if (data is string dataString)
                {
                    httpRequestMessage.Content = new StringContent(dataString, encoding);
                }
                else
                {
                    var memoryStream = new MemoryStream();
                    string temp = dataHandler == null ? DefaultHandlerData(data) : await dataHandler(data);
                    httpRequestMessage.Content = GetHttpContentByFormDataBytes(memoryStream, temp, encoding);
                }
            }
            if (headers != null)
            {
                FillHttpHeaders(httpRequestMessage, headers);
            }
            byte[] resultBytes = await SendByteByHttpRequestMessageAsync(url, httpRequestMessage);
            return resultBytes;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpMethod"></param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">参数字典</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendByHttpContentAsync(string url, HttpMethod httpMethod, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            byte[] resultBytes = await SendByteByHttpContentAsync(url, httpMethod, httpContent, queryParams, headers);
            string result = encoding.GetString(resultBytes);
            return result;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpMethod"></param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">参数字典</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendAsync(string url, HttpMethod httpMethod, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            encoding ??= Encoding.UTF8;
            byte[] resultBytes = await SendByteAsync(url, httpMethod, data, queryParams, headers, encoding, dataHandler);
            string result = encoding.GetString(resultBytes);
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">参数字典</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendPostAsync(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            string result = await SendByHttpContentAsync(url, HttpMethod.Post, httpContent, queryParams, headers, encoding);
            return result;
        }

        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">参数字典</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendPutAsync(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            string result = await SendByHttpContentAsync(url, HttpMethod.Put, httpContent, queryParams, headers, encoding);
            return result;
        }

        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">参数字典</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendDeleteAsync(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            string result = await SendByHttpContentAsync(url, HttpMethod.Delete, httpContent, queryParams, headers, encoding);
            return result;
        }

        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">参数字典</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendPatchAsync(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            var httpMethod = new HttpMethod("PATCH");
            string result = await SendByHttpContentAsync(url, httpMethod, httpContent, queryParams, headers, encoding);
            return result;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendGetAsync(string url, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            string result = await SendAsync(url, HttpMethod.Get, null, queryParams, headers, encoding);
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendPostAsync(string url, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            string result = await SendAsync(url, HttpMethod.Post, data, queryParams, headers, encoding, dataHandler);
            return result;
        }

        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendPutAsync(string url, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            string result = await SendAsync(url, HttpMethod.Put, data, queryParams, headers, encoding, dataHandler);
            return result;
        }

        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendDeleteAsync(string url, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            string result = await SendAsync(url, HttpMethod.Delete, data, queryParams, headers, encoding, dataHandler);
            return result;
        }

        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static async Task<string> SendPatchAsync(string url, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            var httpMethod = new HttpMethod("PATCH");
            string result = await SendAsync(url, httpMethod, data, queryParams, headers, encoding, dataHandler);
            return result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendPost(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            Task<string> resultTask = SendByHttpContentAsync(url, HttpMethod.Post, httpContent, queryParams, headers, encoding);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendPut(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            Task<string> resultTask = SendByHttpContentAsync(url, HttpMethod.Put, httpContent, queryParams, headers, encoding);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendDelete(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            Task<string> resultTask = SendByHttpContentAsync(url, HttpMethod.Delete, httpContent, queryParams, headers, encoding);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="httpContent">Http内容</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendPatch(string url, HttpContent httpContent, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            var httpMethod = new HttpMethod("PATCH");
            Task<string> resultTask = SendByHttpContentAsync(url, httpMethod, httpContent, queryParams, headers, encoding);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendGet(string url, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null)
        {
            Task<string> resultTask = SendAsync(url, HttpMethod.Get, null, queryParams, headers, encoding);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendPost(string url, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            Task<string> resultTask = SendAsync(url, HttpMethod.Post, data, queryParams, headers, encoding, dataHandler);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendPut(string url, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            Task<string> resultTask = SendAsync(url, HttpMethod.Put, data, queryParams, headers, encoding, dataHandler);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendDelete(string url, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            Task<string> resultTask = SendAsync(url, HttpMethod.Delete, data, queryParams, headers, encoding, dataHandler);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">数据</param>
        /// <param name="queryParams">字典参数</param>
        /// <param name="headers">headers</param>
        /// <param name="encoding">字符集</param>
        /// <param name="dataHandler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        public static string SendPatch(string url, object data = null, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null, Encoding encoding = null, Func<object, Task<string>> dataHandler = null)
        {
            var httpMethod = new HttpMethod("PATCH");
            Task<string> resultTask = SendAsync(url, httpMethod, data, queryParams, headers, encoding, dataHandler);
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }

        /// <summary>
        /// Http下载
        /// </summary>
        /// <param name="downloadUrl">下载地址</param>
        /// <param name="saveFilePath">保存文件地址</param>
        /// <param name="cover">覆盖</param>
        /// <returns></returns>
        public static void HttpDownload(string downloadUrl, string saveFilePath, bool cover = false)
        {
            string filePath = GetSaveFilePath(saveFilePath, cover);
            var uri = new Uri(downloadUrl);
            new WebClient().DownloadFile(uri, filePath);
        }

        /// <summary>
        /// Http下载
        /// </summary>
        /// <param name="downloadUrl">下载地址</param>
        /// <param name="saveFilePath">保存文件地址</param>
        /// <param name="cover">覆盖</param>
        /// <returns></returns>
        public static void HttpDownloadAsync(string downloadUrl, string saveFilePath, bool cover = false)
        {
            string filePath = GetSaveFilePath(saveFilePath, cover);
            var uri = new Uri(downloadUrl);
            new WebClient().DownloadFileAsync(uri, filePath);
        }

        /// <summary>
        /// 获取保存文件路径
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <param name="cover">覆盖</param>
        /// <returns></returns>
        private static string GetSaveFilePath(string saveFilePath, bool cover)
        {
            if (saveFilePath == null) throw new ArgumentNullException(nameof(saveFilePath));
            string directoryName = Path.GetDirectoryName(saveFilePath);
            if (string.IsNullOrEmpty(directoryName)) throw new ArgumentNullException(nameof(saveFilePath));
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            string filePath = saveFilePath;
            if (cover)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            else
            {
                var fileIndex = 1;
                while (File.Exists(filePath))
                {
                    string fileName = Path.GetFileNameWithoutExtension(saveFilePath);
                    string extension = Path.GetExtension(saveFilePath);
                    if (string.IsNullOrEmpty(extension))
                    {
                        filePath = directoryName + @"\" + fileName + "(" + fileIndex++ + ")";
                    }
                    else
                    {
                        filePath = directoryName + @"\" + fileName + "(" + fileIndex++ + ")" + extension;
                    }
                }
            }
            return filePath;
        }
    }
}
