using System;
using System.IO;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;

namespace Materal.DotNetty.ServerDome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () => await RunServerAsync());
            string inputKey = string.Empty;
            while (!string.Equals(inputKey, "Exit", StringComparison.Ordinal))
            {
                inputKey = Console.ReadLine();
            }
        }

        private static async Task RunServerAsync()
        {
            Console.WriteLine("WebSocket服务器启动");
            Console.WriteLine(
                $"系统版本：{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}\r\n" +
                $".NET版本：{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}\r\n" +
                $"CPU核心数：{Environment.ProcessorCount}");
            bool useLibuv = ServerSettings.UseLibuv;
            Console.WriteLine("传输类型：" + (useLibuv ? "Libuv" : "Socket"));
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            }
            Console.WriteLine($"服务器垃圾回收机制：{(GCSettings.IsServerGC ? "启用" : "禁用")}");
            Console.WriteLine($"垃圾回收延迟模式：{GCSettings.LatencyMode}");
            IEventLoopGroup bossGroup;
            IEventLoopGroup workGroup;
            if (useLibuv)
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
            if (ServerSettings.IsSsl)
            {
                tlsCertificate = new X509Certificate2(Path.Combine(ExampleHelper.ProcessDirectory, "dotnetty.com.pfx"), "password");
            }
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workGroup);
                if (useLibuv)
                {
                    bootstrap.Channel<TcpServerChannel>();
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)|| 
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

                int port = ServerSettings.Port;
                IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
                IChannel bootstrapChannel = await bootstrap.BindAsync(iPAddress, port);
                Console.WriteLine("打开你的浏览器跳转到："
                    + $"{(ServerSettings.IsSsl ? "https" : "http")}"
                    + $"://{iPAddress}:{port}/api");
                Console.WriteLine("监听中："+ $"{(ServerSettings.IsSsl ? "wss" : "ws")}"
                    + $"://{iPAddress}:{port}/websocket");
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
