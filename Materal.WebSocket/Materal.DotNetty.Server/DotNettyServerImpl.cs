using DotNetty.Codecs.Http;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using Materal.WebSocket.Server;
using Materal.WebSocket.Server.Model;
using System;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Materal.DotNetty.Server
{
    public class DotNettyServerImpl : IWebSocketServer
    {
        public async Task RunServerAsync(ServerConfigModel configModel)
        {
            Console.WriteLine("WebSocket服务器启动");
            Console.WriteLine(
                $"系统版本：{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}\r\n" +
                $".NET版本：{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}\r\n" +
                $"CPU核心数：{Environment.ProcessorCount}");
            Console.WriteLine("传输类型：" + (configModel.UserLibuv ? "Libuv" : "Socket"));
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            }
            Console.WriteLine($"服务器垃圾回收机制：{(GCSettings.IsServerGC ? "启用" : "禁用")}");
            Console.WriteLine($"垃圾回收延迟模式：{GCSettings.LatencyMode}");
            IEventLoopGroup bossGroup;
            IEventLoopGroup workGroup;
            if (configModel.UserLibuv)
            {
                var dispatcher = new DispatcherEventLoopGroup();
                bossGroup = dispatcher;
                workGroup = new WorkerEventLoopGroup(dispatcher);
            }
            else
            {
                bossGroup = new MultithreadEventLoopGroup(1);
                workGroup = new MultithreadEventLoopGroup();
            }
            X509Certificate2 tlsCertificate = null;
            if (configModel.IsSsl)
            {
                tlsCertificate = new X509Certificate2(configModel.CertificatePath, configModel.CertificatePassword);
            }
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workGroup);
                if (configModel.UserLibuv)
                {
                    bootstrap.Channel<TcpServerChannel>();
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                        RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        bootstrap.Option(ChannelOption.SoReuseport, true)
                            .ChildOption(ChannelOption.SoReuseaddr, true);
                    }
                }
                else
                {
                    bootstrap.Channel<TcpServerSocketChannel>();
                }
                bootstrap.Option(ChannelOption.SoBacklog, 8192)
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        if (tlsCertificate != null)
                        {
                            pipeline.AddLast(TlsHandler.Server(tlsCertificate));
                        }
                        pipeline.AddLast(new HttpServerCodec());
                        pipeline.AddLast(new HttpObjectAggregator(65536));
                        if (configModel.ServerHandler is BaseDotNettyServerHandler serverHandler)
                        {
                            pipeline.AddLast(serverHandler);
                        }
                    }));
                IPAddress iPAddress = IPAddress.Parse(configModel.Host);
                IChannel bootstrapChannel = await bootstrap.BindAsync(iPAddress, configModel.Port);
                Console.WriteLine("打开你的浏览器跳转到："
                    + $"{(configModel.IsSsl ? "https" : "http")}"
                    + $"://{iPAddress}:{configModel.Port}/api");
                Console.WriteLine("监听中：" + $"{(configModel.IsSsl ? "wss" : "ws")}"
                    + $"://{iPAddress}:{configModel.Port}/websocket");
                Console.ReadLine();
                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                workGroup.ShutdownGracefullyAsync().Wait();
                bossGroup.ShutdownGracefullyAsync().Wait();
            }
        }
    }
}
