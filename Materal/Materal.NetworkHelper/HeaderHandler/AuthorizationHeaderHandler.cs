using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Materal.NetworkHelper.HeaderHandler
{
    public class AuthorizationHeaderHandler : DefaultHeaderHandler
    {
        private const string _key = "Authorization";
        public override void Handler(HttpRequestMessage httpRequestMessage, Dictionary<string, string> headers)
        {
            if (headers.ContainsKey(_key))
            {
                if (httpRequestMessage.Headers != null)
                {
                    string[] values = headers[_key].Split(' ');
                    if (values.Length != 2) throw new MateralNetworkException("Authentication错误");
                    httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(values[0], values[1]);
                }
                headers.Remove(_key);
            }
            HandlerNext(httpRequestMessage, headers);
        }
    }
}
