using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Materal.MergeBlock.AccessLog.Models
{
    /// <summary>
    /// 请求模型
    /// </summary>
    public class RequestModel
    {
        /// <summary>
        /// Method
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Scheme
        /// </summary>
        public string Scheme { get; set; }
        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// QueryString
        /// </summary>
        public string? QueryString { get; set; }
        /// <summary>
        /// Headers
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }
        /// <summary>
        /// Body
        /// </summary>
        public string? Body { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpRequest"></param>
        public RequestModel(HttpRequest httpRequest)
        {
            Method = httpRequest.Method;
            Scheme = httpRequest.Scheme;
            Host = httpRequest.Host.Host;
            Port = httpRequest.Host.Port ?? -1;
            Path = httpRequest.Path.Value;
            QueryString = httpRequest.QueryString.Value;
            Headers = [];
            foreach (KeyValuePair<string, StringValues> item in httpRequest.Headers)
            {
                Headers.TryAdd(item.Key, item.Value.ToString());
            }
            httpRequest.EnableBuffering();
            StreamReader streamReader = new(httpRequest.Body);
            Body = streamReader.ReadToEndAsync().Result;
            httpRequest.Body.Seek(0, SeekOrigin.Begin);
        }
    }
}
