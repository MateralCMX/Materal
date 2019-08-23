using DotNetty.Codecs.Http;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using System;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TestWebSocket.Common;

namespace TestServer.UI
{
    public class TestServerImpl : ITestServer
    {
        public async Task RunServerAsync()
        {
            ConsoleHelper.TestWriteLine("WebSocket服务器启动");
            ConsoleHelper.TestWriteLine($"系统版本：{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}");
            ConsoleHelper.TestWriteLine($".NET版本：{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}");
            ConsoleHelper.TestWriteLine($"CPU核心数：{Environment.ProcessorCount}");
            ConsoleHelper.TestWriteLine("传输类型：" + (ApplicationConfig.TestWebSocket.UserLibuv ? "Libuv" : "Socket"));
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            }
            ConsoleHelper.TestWriteLine($"服务器垃圾回收机制：{(GCSettings.IsServerGC ? "启用" : "禁用")}");
            ConsoleHelper.TestWriteLine($"垃圾回收延迟模式：{GCSettings.LatencyMode}");
            IEventLoopGroup bossGroup;
            IEventLoopGroup workGroup;
            if (ApplicationConfig.TestWebSocket.UserLibuv)
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
            if (ApplicationConfig.TestWebSocket.IsSsl)
            {
                tlsCertificate = new X509Certificate2(ApplicationConfig.TestWebSocket.CertificatePath, ApplicationConfig.TestWebSocket.CertificatePassword);
            }
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workGroup);
                if (ApplicationConfig.TestWebSocket.UserLibuv)
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
                        pipeline.AddLast(new WebSocketServerHandler());
                    }));
                IPAddress iPAddress = IPAddress.Parse(ApplicationConfig.TestWebSocket.Host);
                IChannel bootstrapChannel = await bootstrap.BindAsync(iPAddress, ApplicationConfig.TestWebSocket.Port);
                ConsoleHelper.TestWriteLine("打开你的浏览器跳转到："
                    + $"{(ApplicationConfig.TestWebSocket.IsSsl ? "https" : "http")}"
                    + $"://{iPAddress}:{ApplicationConfig.TestWebSocket.Port}/api");
                ConsoleHelper.TestWriteLine("监听中：" + $"{(ApplicationConfig.TestWebSocket.IsSsl ? "wss" : "ws")}"
                    + $"://{iPAddress}:{ApplicationConfig.TestWebSocket.Port}/websocket");
                ConsoleHelper.TestWriteLine("输入Stop停止服务器");
                string inputKey = string.Empty;
                while (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                {
                    inputKey = Console.ReadLine();
                }
                ConsoleHelper.TestWriteLine("正在停止服务器.....");
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
