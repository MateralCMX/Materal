using Materal.DotNetty.Client.Model;
using System;

namespace TestClient.WebSocketClient.DotNetty
{
    public class DotNettyTestWebSocketClientConfig : DotNettyClientConfig, ITestWebSocketClientConfig
    {
        public DotNettyTestWebSocketClientConfig()
        {
            UriBuilder = new UriBuilder
            {
                Scheme = "ws",
                Host = "127.0.0.1",
                Port = 8080,
                //Host = "220.165.9.44",
                //Port = 26075,
                Path = string.Empty
            };
            SSLConfig = new DotNettyClientSSLConfig
            {
                UseSSL = false,
                PfxFilePath = "",
                PfxPassword = ""
            };
            UseLibuv = true;
        }
    }
}
