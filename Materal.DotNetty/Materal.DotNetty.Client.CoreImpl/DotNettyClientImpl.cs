using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Codecs.Http.WebSockets.Extensions.Compression;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Materal.DotNetty.Client.Core;
using System;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;

namespace Materal.DotNetty.Client.CoreImpl
{
    public class DotNettyClientImpl : IDotNettyClient
    {
        protected readonly IServiceProvider _service;
        protected ClientConfig _clientConfig;
        private IChannel bootstrapChannel;
        private IEventLoopGroup workGroup;
        public DotNettyClientImpl(IServiceProvider service)
        {
            _service = service;
        }
        public event Action<string> OnMessage;
        public event Action<string, string> OnSubMessage;
        public event Action<Exception> OnException;
        public event Func<string> OnGetCommand;
        public event Action<IClientChannelHandler> OnConfigHandler;
        public async Task RunAsync(ClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
            OnSubMessage?.Invoke("连接服务中......", "重要");
            //第一步：创建ServerBootstrap实例
            var bootstrap = new Bootstrap();
            //第二步：绑定事件组
            workGroup = new MultithreadEventLoopGroup();
            bootstrap.Group(workGroup);
            //第三部：绑定通道
            bootstrap.Channel<TcpSocketChannel>();
            //第四步：配置处理器
            bootstrap.Option(ChannelOption.TcpNodelay, true);
            var builder = new UriBuilder
            {
                Scheme = "ws",
                Host = _clientConfig.Host,
                Port = _clientConfig.Port
            };
            WebSocketClientHandshaker clientHandshaker = WebSocketClientHandshakerFactory.NewHandshaker(builder.Uri, WebSocketVersion.V13, null, true, new DefaultHttpHeaders());
            var handler = new ClientChannelHandler(clientHandshaker);
            bootstrap.Handler(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                pipeline.AddLast("tls", new TlsHandler(stream => new SslStream(stream, true, (sender, certificate, chain, errors) => true), new ClientTlsSettings(null)));
                pipeline.AddLast(
                    new HttpClientCodec(),
                    new HttpObjectAggregator(8192),
                    WebSocketClientCompressionHandler.Instance,
                    handler);
            }));
            bootstrapChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(_clientConfig.Host), _clientConfig.Port));
            await handler.HandshakeCompletion;
        }

        public async Task StopAsync()
        {
            await bootstrapChannel.CloseAsync();
            await workGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
        }
    }
}
