using DotNetty.Codecs.Http;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Materal.DotNetty.Server.Core;
using Materal.StringHelper;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.DotNetty.Server.CoreImpl
{
    public class DotNettyServerImpl : IDotNettyServer
    {
        private readonly IServiceProvider _service;
        private ServerConfig _serverConfig;

        public DotNettyServerImpl(IServiceProvider service)
        {
            _service = service;
        }
        public event Action<IMateralChannelHandler> OnConfigHandler;
        public event Action<string> OnMessage;
        public event Action<string, string> OnSubMessage;
        public event Action<Exception> OnException;
        public event Func<string> OnGetCommand;
        public async Task RunServerAsync(ServerConfig serverConfig)
        {
            _serverConfig = serverConfig;
            OnSubMessage?.Invoke("服务启动中......", "重要");
            //第一步：创建ServerBootstrap实例
            var bootstrap = new ServerBootstrap();
            //第二步：绑定事件组
            IEventLoopGroup mainGroup = new MultithreadEventLoopGroup(1);
            IEventLoopGroup workGroup = new MultithreadEventLoopGroup();
            bootstrap.Group(mainGroup, workGroup);
            //第三步：绑定服务端的通道
            bootstrap.Channel<TcpServerSocketChannel>();
            //第四步：配置处理器
            bootstrap.Option(ChannelOption.SoBacklog, 8192);
            bootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
            {
                try
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(new HttpServerCodec());
                    pipeline.AddLast(new HttpObjectAggregator(655300000));
                    var channelHandler = _service.GetService<MateralChannelHandler>();
                    OnConfigHandler?.Invoke(channelHandler);
                    if (OnException != null)
                    {
                        channelHandler.OnException += OnException;
                    }
                    pipeline.AddLast(channelHandler);
                }
                catch (Exception exception)
                {
                    OnException?.Invoke(exception);
                }
            }));
            //第五步：配置主机和端口号
            IPAddress ipAddress = GetTrueIPAddress();
            IChannel bootstrapChannel = await bootstrap.BindAsync(ipAddress, _serverConfig.Port);
            OnSubMessage?.Invoke("服务启动成功", "重要");
            OnMessage?.Invoke($"已监听http://{ipAddress}:{_serverConfig.Port}");
            OnMessage?.Invoke($"已监听ws://{ipAddress}:{_serverConfig.Port}/websocket");
            //第六步：停止服务
            WaitServerStop();
            OnSubMessage?.Invoke("正在停止服务......", "重要");
            await bootstrapChannel.CloseAsync();
            OnSubMessage?.Invoke("服务已停止", "重要");
        }
        #region 私有方法
        /// <summary>
        /// 等待服务停止
        /// </summary>
        private void WaitServerStop()
        {
            OnMessage?.Invoke("输入Stop停止服务");
            string inputKey = string.Empty;
            while (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
            {
                inputKey = OnGetCommand?.Invoke();
                if (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                {
                    OnException?.Invoke(new DotNettyServerException("未识别命令请重新输入"));
                }
            }
        }
        /// <summary>
        /// 获得真实IP地址
        /// </summary>
        /// <returns></returns>
        private IPAddress GetTrueIPAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
            ipAddresses = ipAddresses.Where(m => m.ToString().IsIPv4()).ToArray();
            bool trueAddress = ipAddresses.Any(m => _serverConfig.Host.Equals(m.ToString()));
            IPAddress result = trueAddress ? IPAddress.Parse(_serverConfig.Host) : ipAddresses[0];
            return result;
        }
        #endregion
    }
}
