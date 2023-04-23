using System;
using System.Threading.Tasks;
using Materal.RabbitMQHelper.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Materal.RabbitMQHelper.ClientExample
{
    /// <summary>
    /// 简单接收者
    /// </summary>
    public class SimplestConsuming
    {
        private readonly ConsumingService _consumingService;

        public SimplestConsuming()
        {
            _consumingService = new ServiceFactory().GetSimplestConsumingService("MateralDemoSimplestExchange", "MateralDemoSimplestQueue01");
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Run()
        {
            var consumingRunConfig = new ConsumingRunConfig
            {
                Received = Received,
                Registered = Registered,
                Unregistered = Unregistered,
                Shutdown = Shutdown
            };
            _consumingService.RunServer(consumingRunConfig);
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void RunAsync()
        {
            var consumingRunConfig = new ConsumingRunAsyncConfig
            {
                ReceivedAsync = ReceivedAsync,
                RegisteredAsync = RegisteredAsync,
                UnregisteredAsync = UnregisteredAsync,
                ShutdownAsync = ShutdownAsync
            };
            _consumingService.RunServerAsync(consumingRunConfig);
        }
        #region 同步方法
        /// <summary>
        /// 接收到消息
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="body"></param>
        /// <param name="send">对象</param>
        /// <param name="e">参数</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private void Received(IModel channel, ReadOnlyMemory<byte> body, object send, BasicDeliverEventArgs e, ConsumingConfig config)
        {
            var message = body.ToString();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}]:{message}");
            channel.BasicAck(e.DeliveryTag, false);
        }
        /// <summary>
        /// 通道注册
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="send">对象</param>
        /// <param name="e">参数</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private void Registered(IModel channel, object send, ConsumerEventArgs e, ConsumingConfig config)
        {
            Console.WriteLine($"通道注册:{e.ConsumerTags}");
        }
        /// <summary>
        /// 通道反注册
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="send">对象</param>
        /// <param name="e">参数</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private void Unregistered(IModel channel, object send, ConsumerEventArgs e, ConsumingConfig config)
        {
            Console.WriteLine($"通道反注册:{e.ConsumerTags}");
        }
        /// <summary>
        /// 通道关闭
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="send">对象</param>
        /// <param name="e">参数</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private void Shutdown(IModel channel, object send, ShutdownEventArgs e, ConsumingConfig config)
        {
            Console.WriteLine($"通道关闭:{e.ReplyText}");
        }
        #endregion
        #region 异步方法
        /// <summary>
        /// 接收到消息
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="body"></param>
        /// <param name="send">对象</param>
        /// <param name="e">参数</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private async Task ReceivedAsync(IModel channel, ReadOnlyMemory<byte> body, object send, BasicDeliverEventArgs e, ConsumingConfig config)
        {
            await Task.Run(() =>
            {
                var message = body.ToString();
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}]:{message}");
                channel.BasicAck(e.DeliveryTag, false);
            });
        }
        /// <summary>
        /// 通道注册
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="send">对象</param>
        /// <param name="e">参数</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private async Task RegisteredAsync(IModel channel, object send, ConsumerEventArgs e, ConsumingConfig config)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"通道注册:{e.ConsumerTags}");
            });
        }
        /// <summary>
        /// 通道反注册
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="send">对象</param>
        /// <param name="e">参数</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private async Task UnregisteredAsync(IModel channel, object send, ConsumerEventArgs e, ConsumingConfig config)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"通道反注册:{e.ConsumerTags}");
            });
        }
        /// <summary>
        /// 通道关闭
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="send">对象</param>
        /// <param name="e">参数</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        private async Task ShutdownAsync(IModel channel, object send, ShutdownEventArgs e, ConsumingConfig config)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"通道关闭:{e.ReplyText}");
            });
        }
        #endregion
    }
}
