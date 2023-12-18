using System.Text;

namespace System.Net.Http
{
    /// <summary>
    /// HttpResponseMessage扩展
    /// </summary>
    public static class HttpResponseMessageExtension
    {
        /// <summary>
        /// 获取响应消息文本
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        public static async Task<string> GetHttpResponseMessageTextAsync(this HttpResponseMessage httpResponseMessage)
        {
            using MemoryStream memoryStream = new();
            await httpResponseMessage.Content.CopyToAsync(memoryStream);
            byte[] responseContent = memoryStream.ToArray();
            Encoding encoding = httpResponseMessage.Content.Headers.ContentType.CharSet == null ?
                Encoding.Default :
                Encoding.GetEncoding(httpResponseMessage.Content.Headers.ContentType.CharSet);
            return encoding.GetString(responseContent);
        }
    }
}
