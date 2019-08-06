using System;
using System.Threading.Tasks;
using Materal.RabbitMQHelper.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Materal.RabbitMQHelper.ClientExample
{
    /// <summary>
    /// 简单生产者
    /// </summary>
    public class SimplestProducing
    {
        private readonly ConsumingService _consumingService;

        public SimplestProducing()
        {
            _consumingService = new ServiceFactory().GetSimplestConsumingService("MateralDemoSimplestExchange", "MateralDemoSimplestQueue01");
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
        private async Task ReceivedAsync(IModel channel, byte[] body, object send, BasicDeliverEventArgs e, ConsumingConfig config)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion
    }
}
