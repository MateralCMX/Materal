using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Codecs.Http.WebSockets.Extensions.Compression;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using Materal.DotNetty.Client;
using Materal.DotNetty.Client.Model;

namespace Materal.DotNetty.ClientDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var clientConfig = new DotNettyClientConfig
            //{
            //    UriBuilder = new UriBuilder
            //    {
            //        Scheme = "ws",
            //        //Host = "127.0.0.1",
            //        //Port = 10001,
            //        Host = "220.165.9.44",
            //        Port = 26075,
            //        Path = string.Empty
            //    },
            //    SSLConfig = new DotNettyClientSSLConfig
            //    {
            //        UseSSL = false,
            //        PfxFilePath = "",
            //        PfxPassword = ""
            //    },
            //    UseLibuv = true
            //};
            //var dotNettyClientImpl = new DotNettyClientImpl();
            //dotNettyClientImpl.SetConfig(clientConfig);
            //Console.WriteLine("配置完毕");
            //dotNettyClientImpl.RunClientAsync<WebSocketClientHandler>().Wait();
            //Console.WriteLine("握手成功");
            //string inputKey = string.Empty;
            //while (!string.Equals(inputKey, "Exit", StringComparison.Ordinal))
            //{
            //    inputKey = Console.ReadLine();
            //    if (string.Equals(inputKey, "Exit", StringComparison.Ordinal))
            //    {
            //        break;
            //    }
            //    dotNettyClientImpl.SendMessageAsync(inputKey).Wait();
            //}
        }
    }
}
