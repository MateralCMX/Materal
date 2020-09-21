using System.Collections.Generic;
using System.Net.Http;

namespace Materal.NetworkHelper.HeaderHandler
{
    public class DefaultHeaderHandler
    {
        private DefaultHeaderHandler _next;

        public void SetNext(DefaultHeaderHandler defaultHeader)
        {
            _next = defaultHeader;
        }

        public virtual void Handler(HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                httpRequestMessage.Headers?.TryAddWithoutValidation(header.Key, header.Value);
                httpRequestMessage.Content?.Headers?.TryAddWithoutValidation(header.Key, header.Value);
            }
            HandlerNext(httpRequestMessage, headers);
        }

        protected void HandlerNext(HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers)
        {
            _next?.Handler(httpRequestMessage, headers);
        }
    }
}
