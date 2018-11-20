using Materal.ConvertHelper;
using Materal.StringHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Materal.NetworkHelper
{
    public class HttpHelper
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="type"></param>
        /// <param name="data">参数字典</param>
        /// <param name="heads">heads</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        private static string Send(string url, HttpMethodType type = HttpMethodType.Get, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url), "Url地址不能为空");
            if (!url.IsUrl()) throw new ArgumentException("Url地址错误", nameof(url));
            if (encoding == null) encoding = Encoding.UTF8;
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            var ms = new MemoryStream();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(type.ToString()),
                RequestUri = new Uri(url)
            };
            if (data != null)
            {
                httpRequestMessage.Content = GetHttpContentByFormDataBytes(ms, data);
            }
            if (heads != null)
            {
                foreach (KeyValuePair<string, string> item in heads)
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation(item.Key, item.Value);
                    httpRequestMessage.Content?.Headers.TryAddWithoutValidation(item.Key, item.Value);
                }
            }
            HttpResponseMessage httpResponseMessage = client.SendAsync(httpRequestMessage).Result;
            byte[] resultBytes = httpResponseMessage.Content.ReadAsByteArrayAsync().Result;
            string result = encoding.GetString(resultBytes);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                throw new MateralHttpException(httpResponseMessage.StatusCode, result);
            return result;
        }
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数字典</param>
        /// <param name="heads">heads</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static string SendGet(string url, Dictionary<string, string> data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            url = SpliceUrlParams(url, data);
            return Send(url, HttpMethodType.Get, null, heads, encoding);
        }
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数字典</param>
        /// <param name="heads">heads</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static T SendGet<T>(string url, Dictionary<string, string> data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            string result = SendGet(url, data, heads, encoding);
            return result.JsonToObject<T>();
        }
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string SendPost(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            return Send(url, HttpMethodType.Post, data, heads, encoding);
        }
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T SendPost<T>(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            string result = SendPost(url, data, heads, encoding);
            return result.JsonToObject<T>();
        }
        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string SendPut(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            return Send(url, HttpMethodType.Put, data, heads, encoding);
        }
        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T SendPut<T>(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            string result = SendPut(url, data, heads, encoding);
            return result.JsonToObject<T>();
        }
        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string SendDelete(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            return Send(url, HttpMethodType.Delete, data, heads, encoding);
        }
        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T SendDelete<T>(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            string result = SendDelete(url, data, heads, encoding);
            return result.JsonToObject<T>();
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="type"></param>
        /// <param name="data">参数字典</param>
        /// <param name="heads">heads</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MateralHttpException"></exception>
        private static async Task<string> SendAsync(string url, HttpMethodType type = HttpMethodType.Get, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url), "Url地址不能为空");
            if (!url.IsUrl()) throw new ArgumentException("Url地址错误", nameof(url));
            if (encoding == null) encoding = Encoding.UTF8;
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            var ms = new MemoryStream();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod(type.ToString()),
                RequestUri = new Uri(url),
            };
            if (data != null)
            {
                httpRequestMessage.Content = GetHttpContentByFormDataBytes(ms, data);
            }
            if (heads != null)
            {
                foreach (KeyValuePair<string, string> item in heads)
                {
                    httpRequestMessage.Headers.TryAddWithoutValidation(item.Key, item.Value);
                    httpRequestMessage.Content?.Headers.TryAddWithoutValidation(item.Key, item.Value);
                }
            }
            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
            byte[] resultBytes = await httpResponseMessage.Content.ReadAsByteArrayAsync();
            string result = encoding.GetString(resultBytes);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                throw new MateralHttpException(httpResponseMessage.StatusCode, result);
            return result;
        }
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数字典</param>
        /// <param name="heads">heads</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static async Task<string> SendGetAsync(string url, Dictionary<string, string> data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            url = SpliceUrlParams(url, data);
            return await SendAsync(url, HttpMethodType.Get, null, heads, encoding);
        }
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数字典</param>
        /// <param name="heads">heads</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static async Task<T> SendGetAsync<T>(string url, Dictionary<string, string> data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            string result = await SendGetAsync(url, data, heads, encoding);
            return result.JsonToObject<T>();
        }
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> SendPostAsync(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            return await SendAsync(url, HttpMethodType.Post, data, heads, encoding);
        }
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<T> SendPostAsync<T>(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            string result = await SendPostAsync(url, data, heads, encoding);
            return result.JsonToObject<T>();
        }
        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> SendPutAsync(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            return await SendAsync(url, HttpMethodType.Put, data, heads, encoding);
        }
        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<T> SendPutAsync<T>(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            string result = await SendPutAsync(url, data, heads, encoding);
            return result.JsonToObject<T>();
        }
        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> SendDeleteAsync(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            return await SendAsync(url, HttpMethodType.Delete, data, heads, encoding);
        }
        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="heads"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<T> SendDeleteAsync<T>(string url, object data = null, Dictionary<string, string> heads = null, Encoding encoding = null)
        {
            string result = await SendDeleteAsync(url, data, heads, encoding);
            return result.JsonToObject<T>();
        }
        /// <summary>
        /// 拼接URL参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">参数字典</param>
        /// <returns>带参数的Url地址</returns>
        private static string SpliceUrlParams(string url, Dictionary<string, string> data)
        {
            if (string.IsNullOrEmpty(url) || data == null) return url;
            var urlParamsStrs = new List<string>();
            foreach (KeyValuePair<string, string> param in data)
            {
                urlParamsStrs.Add($"{param.Key}={param.Value}");
            }

            url += $"?{string.Join("&", urlParamsStrs)}";
            return url;
        }
        /// <summary>
        /// 获得FormBytes数据
        /// </summary>
        /// <param name="ms">记忆流</param>
        /// <param name="data">数据字符</param>
        /// <returns>数据流</returns>
        private static HttpContent GetHttpContentByFormDataBytes(Stream ms, object data)
        {
            string dataStr;
            if (data is string str)
            {
                dataStr = str;
            }
            else
            {
                try
                {
                    dataStr = data.ToJson();
                }
                catch (Exception)
                {
                    dataStr = data.ToString();
                }
            }
            byte[] formDataBytes = Encoding.UTF8.GetBytes(dataStr);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);
            var content = new StreamContent(ms);
            return content;
        }

        /// <summary>
        /// Http下载
        /// </summary>
        /// <param name="downloadUrl">下载地址</param>
        /// <param name="saveFilePath">保存文件地址</param>
        /// <param name="bufferSize">流大小</param>
        /// <param name="fileSumSize">文件总大小</param>
        /// <param name="progress">下载进度(当前下载大小,总大小)</param>
        /// <returns></returns>
        public static async Task<bool> HttpDownload(string downloadUrl, string saveFilePath, int bufferSize = 1024, Action<long> fileSumSize = null, Action<long> progress = null)
        {
            string filePath = GetSaveFilePath(saveFilePath);
            using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                WebRequest webRequest = WebRequest.Create(downloadUrl);
                WebResponse webResponse = webRequest.GetResponse();
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    if (responseStream == null) return false;
                    fileSumSize?.Invoke(webResponse.ContentLength);
                    var buffer = new byte[bufferSize];
                    var sum = 0L;
                    while (true)
                    {
                        int size = await responseStream.ReadAsync(buffer, 0, buffer.Length);
                        if (size == 0) break;
                        await fileStream.WriteAsync(buffer, 0, size);
                        sum += size;
                        progress?.Invoke(sum);
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 获取保存文件路径
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <returns></returns>
        private static string GetSaveFilePath(string saveFilePath)
        {
            if (saveFilePath == null) throw new ArgumentNullException(nameof(saveFilePath));
            string directoryName = Path.GetDirectoryName(saveFilePath);
            if (string.IsNullOrEmpty(directoryName)) throw new ArgumentNullException(nameof(saveFilePath));
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            var fileIndex = 1;
            string filePath = saveFilePath;
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

            return filePath;
        }
    }
}
