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

namespace Materal.DotNetty.ClientDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () => await RunClientAsync());
            string inputKey = string.Empty;
            while (!string.Equals(inputKey, "Exit", StringComparison.Ordinal))
            {
                inputKey = Console.ReadLine();
            }
        }

        private static async Task RunClientAsync()
        {
            var builder = new UriBuilder
            {
                Scheme = ClientSettings.IsSsl ? "wss" : "ws",
                Host = ClientSettings.Host.ToString(),
                Port = ClientSettings.Port
            };
            string path = ExampleHelper.Configuration["path"];
            if (!string.IsNullOrEmpty(path))
            {
                builder.Path = path;
            }
            Uri uri = builder.Uri;
            bool useLibuv = ClientSettings.UseLibuv;
            Console.WriteLine("传输方式：" + (useLibuv ? "Libuv" : "Socket"));
            IEventLoopGroup group;
            if (useLibuv)
            {
                group = new EventLoopGroup();
            }
            else
            {
                group = new MultithreadEventLoopGroup();
            }
            X509Certificate2 cert = null;
            string targetHost = null;
            if (ClientSettings.IsSsl)
            {
                cert = new X509Certificate2(Path.Combine(ExampleHelper.ProcessDirectory, "dotnetty.com.pfx"), "password");
                targetHost = cert.GetNameInfo(X509NameType.DnsName, false);
            }
            try
            {
                var bootstrap = new Bootstrap();
                bootstrap.Group(group).Option(ChannelOption.TcpNodelay, true);
                if (useLibuv)
                {
                    bootstrap.Channel<TcpChannel>();
                }
                else
                {
                    bootstrap.Channel<TcpSocketChannel>();
                }
                var handler = new WebSocketClientHandler(WebSocketClientHandshakerFactory.NewHandshaker(uri, WebSocketVersion.V13, null, true, new DefaultHttpHeaders()));
                bootstrap.Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    if (cert != null)
                    {
                        pipeline.AddLast("tls", new TlsHandler(stream => new SslStream(stream, true, (sender, certificate, chain, errors) => true), new ClientTlsSettings(targetHost)));
                    }
                    pipeline.AddLast(
                        new HttpClientCodec(),
                        new HttpObjectAggregator(8192),
                        WebSocketClientCompressionHandler.Instance,
                        handler);
                }));
                IChannel ch = await bootstrap.ConnectAsync(new IPEndPoint(ClientSettings.Host, ClientSettings.Port));
                await handler.HandshakeCompletion;
                Console.WriteLine("WebSocket握手完毕");
                while (true)
                {
                    string msg = Console.ReadLine();
                    if (msg == null)
                    {
                        break;
                    }
                    else if ("bye".Equals(msg.ToLower()))
                    {
                        await ch.WriteAndFlushAsync(new CloseWebSocketFrame());
                        break;
                    }
                    else if ("ping".Equals(msg.ToLower()))
                    {
                        var frame = new PingWebSocketFrame(Unpooled.WrappedBuffer(new byte[] { 8, 1, 8, 1 }));
                        await ch.WriteAndFlushAsync(frame);
                    }
                    else
                    {
                        WebSocketFrame frame = new TextWebSocketFrame(msg);
                        await ch.WriteAndFlushAsync(frame);
                    }
                }

                await ch.CloseAsync();
            }
            finally
            {
                await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
        }
    }
}
