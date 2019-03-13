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
using Materal.WebSocket.Client;
using Materal.WebSocket.Client.Model;
using Materal.WebSocket.Commands;
using Materal.WebSocket.Events;
using Materal.WebSocket.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Materal.DotNetty.Client
{
    public abstract class DotNettyClientImpl : IWebSocketClient
    {
        public IChannel Channel { get; set; }
        private IEventLoopGroup _eventLoopGroup;
        private string _targetHost;
        private X509Certificate2 _x509Certificate2;
        public abstract Task HandleEventAsync(IEvent eventM);
        public abstract void HandleEvent(IEvent eventM);
        public IWebSocketClientConfig Config { get; private set; }
        public WebSocketState WebSocketState { get; set; } = WebSocketState.None;
        private DotNettyClientHandler _clientHandler;
        public Bootstrap Bootstrap;

        public async Task SendMessageAsync(WebSocketFrame frame)
        {
            await Channel.WriteAndFlushAsync(frame);
        }

        public async Task StopAsync()
        {
            await Channel.CloseAsync();
            await _eventLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            WebSocketState = WebSocketState.Closed;
        }

        public async Task SendCommandByStringAsync(ICommand command)
        {
            string commandJson = command.ToJson();
            WebSocketFrame frame = new TextWebSocketFrame(commandJson);
            await SendMessageAsync(frame);
            _clientHandler.OnSendMessage(commandJson);
        }

        public async Task SendCommandByBytesAsync(ICommand command)
        {
            WebSocketFrame frame = new BinaryWebSocketFrame();
            frame.Content.SetBytes(0, command.ToBytes());
            await SendMessageAsync(frame);
            _clientHandler.OnSendMessage(command.ToJson());
        }

        public void SetConfig(IWebSocketClientConfig config)
        {
            if (config is DotNettyClientConfig webSocketClientConfig)
            {
                if (webSocketClientConfig.Verification(out List<string> messages))
                {
                    Config = webSocketClientConfig;
                    if (webSocketClientConfig.UseLibuv)
                    {
                        _eventLoopGroup = new EventLoopGroup();
                    }
                    else
                    {
                        _eventLoopGroup = new MultithreadEventLoopGroup();
                    }
                    if (webSocketClientConfig.SSLConfig != null && webSocketClientConfig.SSLConfig.UseSSL)
                    {
                        _x509Certificate2 = new X509Certificate2(webSocketClientConfig.SSLConfig.PfxFilePath, webSocketClientConfig.SSLConfig.PfxPassword);
                        _targetHost = _x509Certificate2.GetNameInfo(X509NameType.DnsName, false);
                    }
                }
                else
                {
                    throw new MateralWebSocketClientException(string.Join(",", messages));
                }
            }
            else
            {
                throw new MateralWebSocketClientException("Config类型必须派生于DotNettyClientConfig");
            }
        }

        public async Task StartAsync<T>() where T : IWebSocketClientHandler
        {
            if (!(Config is DotNettyClientConfig webSocketClientConfig)) throw new MateralWebSocketClientException("Config类型必须派生于DotNettyClientConfig");
            WebSocketClientHandshaker webSocketClientHandshaker = WebSocketClientHandshakerFactory.NewHandshaker(webSocketClientConfig.UriBuilder.Uri, WebSocketVersion.V13, null, true, new DefaultHttpHeaders());
            var handler = ConvertManager.GetDefaultObject<T>(webSocketClientHandshaker);
            if (!(handler is DotNettyClientHandler clientHandler)) throw new MateralWebSocketClientException("Handler类型必须派生于DotNettyClientHandler");
            _clientHandler = clientHandler;
            _clientHandler.SetClient(this);
            await StartAsync();
        }

        public async Task StartAsync()
        {
            if (_clientHandler == null) throw new MateralWebSocketClientException("未设置Handler");
            if (!(Config is DotNettyClientConfig webSocketClientConfig)) throw new MateralWebSocketClientException("Config类型必须派生于DotNettyClientConfig");
            Bootstrap = new Bootstrap();
            if (webSocketClientConfig.UseLibuv)
            {
                Bootstrap.Channel<TcpChannel>();
            }
            else
            {
                Bootstrap.Channel<TcpSocketChannel>();
            }
            Bootstrap.Group(_eventLoopGroup).Option(ChannelOption.TcpNodelay, true);
            Bootstrap.Handler(new ActionChannelInitializer<IChannel>(channel =>
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
                    _clientHandler);
            }));
            _clientHandler.ChannelStart(Channel);
            Channel = await Bootstrap.ConnectAsync(new IPEndPoint(webSocketClientConfig.IPAddress, webSocketClientConfig.UriBuilder.Port));
            await _clientHandler.HandshakeCompletion;
            WebSocketState = WebSocketState.Open;
        }
    }
}
