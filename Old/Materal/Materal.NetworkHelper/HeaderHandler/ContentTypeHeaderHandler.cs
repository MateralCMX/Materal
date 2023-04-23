using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Materal.NetworkHelper.HeaderHandler
{
    public class ContentTypeHeaderHandler : DefaultHeaderHandler
    {
        private const string _key = "Content-Type";
        public override void Handler(HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers)
        {
            if (headers.ContainsKey(_key))
            {
                if (httpRequestMessage.Content?.Headers != null)
                {
                    httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(headers[_key]);
                }
                headers.Remove(_key);
            }
            HandlerNext(httpRequestMessage, headers);
        }
    }
}
