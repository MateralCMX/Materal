using System;
using System.Linq;
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
using Materal.MicroFront.Common;
using Materal.StringHelper;

namespace Materal.MicroFront
{
    public class MicroFrontServerImpl : IMicroFrontServer
    {
        public async Task RunServerAsync()
        {
            ConsoleHelper.MicroFrontServerWriteLine("服务启动中......");
            ConsoleHelper.MicroFrontServerWriteLine($"电脑名称：{Environment.MachineName}");
            ConsoleHelper.MicroFrontServerWriteLine($"系统版本：{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}");
            ConsoleHelper.MicroFrontServerWriteLine($".NET版本：{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}");
            ConsoleHelper.MicroFrontServerWriteLine($"CPU核心数：{Environment.ProcessorCount}");
            ConsoleHelper.MicroFrontServerWriteLine("传输类型：" + (ApplicationConfig.WebSocketConfig.UserLibuv ? "Libuv" : "Socket"));
            ConsoleHelper.MicroFrontServerWriteLine($"服务垃圾回收机制：{(GCSettings.IsServerGC ? "服务" : "工作站")}");
            ConsoleHelper.MicroFrontServerWriteLine($"垃圾回收延迟模式：{GCSettings.LatencyMode}");
            IEventLoopGroup bossGroup;
            IEventLoopGroup workGroup;
            if (ApplicationConfig.WebSocketConfig.UserLibuv)
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
            if (ApplicationConfig.WebSocketConfig.IsSsl)
            {
                tlsCertificate = new X509Certificate2(ApplicationConfig.WebSocketConfig.CertificatePath, ApplicationConfig.WebSocketConfig.CertificatePassword);
            }
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workGroup);
                if (ApplicationConfig.WebSocketConfig.UserLibuv)
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
                        pipeline.AddLast(new HttpObjectAggregator(ApplicationConfig.WebSocketConfig.MaxMessageLength));
                        pipeline.AddLast(ApplicationData.GetService<WebSocketServerHandler>());
                    }));
                string hostName = Dns.GetHostName();
                IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
                ipAddresses = ipAddresses.Where(m => m.ToString().IsIPv4()).ToArray();
                bool trueAddress = ipAddresses.Any(m => ApplicationConfig.WebSocketConfig.Host.Equals(m.ToString()));
                IPAddress ipAddress = trueAddress ?
                    IPAddress.Parse(ApplicationConfig.WebSocketConfig.Host) :
                    ipAddresses[0];
                IChannel bootstrapChannel = await bootstrap.BindAsync(ipAddress, ApplicationConfig.WebSocketConfig.Port);
                ConsoleHelper.MicroFrontServerWriteLine("打开浏览器跳转到："
                                                       + $"{(ApplicationConfig.WebSocketConfig.IsSsl ? "https" : "http")}"
                                                       + $"://{ipAddress}:{ApplicationConfig.WebSocketConfig.Port}");
                ConsoleHelper.MicroFrontServerWriteLine("打开浏览器跳转到："
                                                        + $"{(ApplicationConfig.WebSocketConfig.IsSsl ? "https" : "http")}"
                                                        + $"://{ipAddress}:{ApplicationConfig.WebSocketConfig.Port}/Manager/Login");
                ConsoleHelper.MicroFrontServerWriteLine("服务启动完毕");
                ConsoleHelper.MicroFrontServerWriteLine("输入Stop停止服务");
                string inputKey = string.Empty;
                while (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                {
                    inputKey = Console.ReadLine();
                }
                ConsoleHelper.MicroFrontServerWriteLine("正在停止服务.....");
                await bootstrapChannel.CloseAsync();
            }
            finally
            {
                workGroup.ShutdownGracefullyAsync().Wait();
                bossGroup.ShutdownGracefullyAsync().Wait();
                ConsoleHelper.MicroFrontServerWriteLine("服务已停止");
            }
        }
    }
}
