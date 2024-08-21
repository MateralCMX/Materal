namespace Materal.Utils.Http
{
    /// <summary>
    /// Http帮助类
    /// </summary>
    public interface IHttpHelper
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<HttpResponseMessage> SendRequestGetResponseMessageAsync(HttpRequestMessage httpRequestMessage);
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<byte[]> SendByBytesAsync(HttpRequestMessage httpRequestMessage);
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<byte[]> SendByBytesAsync(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage);
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendAsync(HttpRequestMessage httpRequestMessage);
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="httpContent"></param>
        /// <param name="headers"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendHttpContentAsync(string url, HttpMethod httpMethod, HttpContent? httpContent = null, Dictionary<string, string>? headers = null, Version? httpVersion = null);
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendAsync(string url, HttpMethod httpMethod, object data, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendAsync(string url, HttpMethod httpMethod, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendAsync<T>(string url, HttpMethod httpMethod, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendGetAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendPostAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendPutAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendDeleteAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Trace请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendTraceAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Options请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendOptionsAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Head请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendHeadAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendPatchAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Connect请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<string> SendConnectAsync(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendGetAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendPostAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Put请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendPutAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Delete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendDeleteAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Trace请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendTraceAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Options请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendOptionsAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Head请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendHeadAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Patch请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendPatchAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
        /// <summary>
        /// 发送Connect请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryArgs"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <param name="encoding"></param>
        /// <param name="httpVersion"></param>
        /// <returns></returns>
        /// <exception cref="MateralHttpException"></exception>
        Task<T> SendConnectAsync<T>(string url, Dictionary<string, string>? queryArgs = null, object? data = null, Dictionary<string, string>? headers = null, Encoding? encoding = null, Version? httpVersion = null);
    }
}
