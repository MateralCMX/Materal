using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace Materal.MergeBlock.AccessLog.Models
{
    /// <summary>
    /// 响应模型
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public int StatusCode { get; set; }
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
        /// <param name="exception"></param>
        public ResponseModel(Exception exception)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
            Headers = [];
            Body = exception.Message;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="bodyStream"></param>
        public ResponseModel(HttpResponse httpResponse, MemoryStream bodyStream)
        {
            StatusCode = httpResponse.StatusCode;
            Headers = [];
            foreach (KeyValuePair<string, StringValues> item in httpResponse.Headers)
            {
                Headers.TryAdd(item.Key, item.Value.ToString());
            }
            bodyStream.Position = 0;
            try
            {
                Body = new StreamReader(bodyStream).ReadToEnd();
            }
            catch (Exception ex)
            {
                Body = $"Body无法读取\r\n{ex.GetErrorMessage()}";
            }
        }
    }
}
