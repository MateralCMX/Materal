using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Materal.RabbitMQHelper.Model
{
    /// <summary>
    /// 消费启动配置
    /// </summary>
    public class ConsumingRunConfig
    {
        public Action<IModel, ReadOnlyMemory<byte>, object, BasicDeliverEventArgs, ConsumingConfig> Received { get; set; }
        public Action<IModel, object, ConsumerEventArgs, ConsumingConfig> Registered { get; set; }
        public Action<IModel, object, ConsumerEventArgs, ConsumingConfig> Unregistered { get; set; }
        public Action<IModel, object, ShutdownEventArgs, ConsumingConfig> Shutdown { get; set; }
    }
}
