using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Codecs.Http.WebSockets.Extensions.Compression;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using Materal.ConvertHelper;
using Materal.DotNetty.Client.Model;
using Materal.WebSocket;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Materal.DotNetty.Client
{
    public class DotNettyClientImpl : IWebSocketClient
    {
        public DotNettyClientConfig ClientConfig { get; set; }

        public IChannel Channel { get; set; }

        private IEventLoopGroup _eventLoopGroup;
        private string _targetHost = null;
        private X509Certificate2 _x509Certificate2 = null;
        public void SetConfig(DotNettyClientConfig clientConfig)
        {
            ClientConfig = clientConfig;
            if (ClientConfig.UseLibuv)
            {
                _eventLoopGroup = new EventLoopGroup();
            }
            else
            {
                _eventLoopGroup = new MultithreadEventLoopGroup();
            }
            if (ClientConfig.SSLConfig != null && ClientConfig.SSLConfig.UseSSL)
            {
                _x509Certificate2 = new X509Certificate2(ClientConfig.SSLConfig.PfxFilePath, ClientConfig.SSLConfig.PfxPassword);
                _targetHost = _x509Certificate2.GetNameInfo(X509NameType.DnsName, false);
            }
        }
        public async Task RunClientAsync<T>() where T: DotNettyClientHandler
        {
            var bootstrap = new Bootstrap();
            if (ClientConfig.UseLibuv)
            {
                bootstrap.Channel<TcpChannel>();
            }
            else
            {
                bootstrap.Channel<TcpSocketChannel>();
            }
            bootstrap.Group(_eventLoopGroup).Option(ChannelOption.TcpNodelay, true);
            WebSocketClientHandshaker webSocketClientHandshaker = WebSocketClientHandshakerFactory.NewHandshaker(ClientConfig.UriBuilder.Uri, WebSocketVersion.V13, null, true, new DefaultHttpHeaders());
            var handler = ConvertManager.GetDefultObject<T>(webSocketClientHandshaker);
            bootstrap.Handler(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                if (_x509Certificate2 != null)
                {
                    pipeline.AddLast("tls", new TlsHandler(stream => new SslStream(stream, true, (sender, certificate, chain, errors) => true), new ClientTlsSettings(_targetHost)));
                }
                pipeline.AddLast(
                    new HttpClientCodec(),
                    new HttpObjectAggregator(8192),
                    WebSocketClientCompressionHandler.Instance,
                    handler);
            }));
            Channel = await bootstrap.ConnectAsync(new IPEndPoint(ClientConfig.IPAddress, ClientConfig.UriBuilder.Port));
            await handler.HandshakeCompletion;
        }

        public async Task SendMessageAsync(string message)
        {
            WebSocketFrame frame = new TextWebSocketFrame(message);
            await SendMessageAsync(frame);
        }

        public async Task SendMessageAsync(byte[] bytes)
        {
            WebSocketFrame frame = new BinaryWebSocketFrame();
            frame.Content.SetBytes(0, bytes);
            await SendMessageAsync(frame);
        }

        public async Task SendMessageAsync(WebSocketFrame frame)
        {
            await Channel.WriteAndFlushAsync(frame);
        }

        public async Task StopClientAsync()
        {
            await Channel.CloseAsync();
            await _eventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
        }
    }
}
