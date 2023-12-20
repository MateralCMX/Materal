using Materal.Abstractions;
using System.Net.Http;
using System.Text;

namespace Materal.Utils.Http
{
    /// <summary>
    /// Http异常
    /// </summary>
    public class MateralHttpException : MateralException
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        public HttpRequestMessage? HttpRequestMessage { get; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public HttpResponseMessage? HttpResponseMessage { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage) : this(httpRequestMessage, httpResponseMessage, $"Http请求错误{Convert.ToInt32(httpResponseMessage.StatusCode)}")
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <param name="message"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage, string message) : base(message)
        {
            HttpRequestMessage = httpRequestMessage;
            HttpResponseMessage = httpResponseMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="httpResponseMessage"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage, string message, Exception innerException) : base(message, innerException)
        {
            HttpRequestMessage = httpRequestMessage;
            HttpResponseMessage = httpResponseMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="message"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, string message) : base(message)
        {
            HttpRequestMessage = httpRequestMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(HttpRequestMessage httpRequestMessage, string message, Exception innerException) : base(message, innerException)
        {
            HttpRequestMessage = httpRequestMessage;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MateralHttpException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MateralHttpException(string message) : base(message)
        {
        }
        /// <summary>
        /// 获得Http请求消息内容
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetHttpResponseContentText()
        {
            if (HttpResponseMessage is null) return null;
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string GetExceptionMessage(Func<Exception, string?>? beforFunc = null, Func<Exception, string?>? afterFunc = null)
        {
            static string? HttpExceptionBefor(Exception exception)
            {
                if (exception is not MateralHttpException httpException) return null;
                StringBuilder errorMessage = new();
                if (httpException.HttpRequestMessage is not null)
                {
                    errorMessage.AppendLine("Request:");
                    if (httpException.HttpRequestMessage.RequestUri is not null)
                    {
                        errorMessage.AppendLine($"  Url:{httpException.HttpRequestMessage.RequestUri.AbsoluteUri}");
                    }
                    if (httpException.HttpRequestMessage.Headers.Any())
                    {
                        errorMessage.AppendLine($"  Headers:");
                        foreach (KeyValuePair<string, IEnumerable<string>> header in httpException.HttpRequestMessage.Headers)
                        {
                            errorMessage.AppendLine($"      {header.Key}:{string.Join(";", header.Value)}");
                        }
                    }
                    if (httpException.HttpRequestMessage.Content is not null)
                    {
                        if (httpException.HttpRequestMessage.Content.Headers.Any())
                        {
                            errorMessage.AppendLine($"  ContentHeaders:");
                            foreach (KeyValuePair<string, IEnumerable<string>> header in httpException.HttpRequestMessage.Content.Headers)
                            {
                                errorMessage.AppendLine($"      {header.Key}:{string.Join(";", header.Value)}");
                            }
                        }
                        if (httpException.HttpRequestMessage.Content is StringContent stringContent)
                        {
                            try
                            {
                                string message = stringContent.ReadAsStringAsync().Result;
                                errorMessage.AppendLine($"  ContentBody:{message}");
                            }
                            catch (Exception ex)
                            {
                                errorMessage.AppendLine($"  获取ContentBody失败:{ex.Message}");
                            }
                        }
                    }
                }
                if (httpException.HttpResponseMessage is not null)
                {
                    errorMessage.AppendLine("Response:");
                    errorMessage.AppendLine($"  StatusCode:{httpException.HttpResponseMessage.StatusCode}[{(int)httpException.HttpResponseMessage.StatusCode}]");
                    if (httpException.HttpResponseMessage.Headers.Any())
                    {
                        errorMessage.AppendLine($"  Headers:");
                        foreach (KeyValuePair<string, IEnumerable<string>> header in httpException.HttpResponseMessage.Headers)
                        {
                            errorMessage.AppendLine($"      {header.Key}:{string.Join(";", header.Value)}");
                        }
                    }
                    if (httpException.HttpResponseMessage.Content is not null && httpException.HttpResponseMessage.Content.Headers.Any())
                    {
                        errorMessage.AppendLine($"  ContentHeaders:");
                        foreach (KeyValuePair<string, IEnumerable<string>> header in httpException.HttpResponseMessage.Content.Headers)
                        {
                            errorMessage.AppendLine($"      {header.Key}:{string.Join(";", header.Value)}");
                        }
                        if (httpException.HttpResponseMessage.Content.Headers.ContentLength is not null && httpException.HttpResponseMessage.Content.Headers.ContentLength > 0)
                        {
                            try
                            {
                                string? message = httpException.HttpRequestMessage?.Content?.ReadAsStringAsync().Result;
                                errorMessage.AppendLine($"  ContentBody:{message}");
                            }
                            catch (Exception ex)
                            {
                                errorMessage.AppendLine($"  获取ContentBody失败:{ex.Message}");
                            }
                        }
                    }
                }
                string result = errorMessage.ToString();
                return result;
            }
            if (beforFunc is null)
            {
                beforFunc = HttpExceptionBefor;
            }
            else
            {
                beforFunc = exception =>
                {
                    string? message = beforFunc.Invoke(exception);
                    string? httpMessage = HttpExceptionBefor(exception);
                    if (message is null && httpMessage is not null) return httpMessage;
                    if (message is not null && httpMessage is null) return message;
                    if (message is not null && httpMessage is not null)
                    {
                        message += "\r\n" + httpMessage;
                    }
                    return message;
                };
            }
            return base.GetExceptionMessage(beforFunc, afterFunc);
        }
    }
}
