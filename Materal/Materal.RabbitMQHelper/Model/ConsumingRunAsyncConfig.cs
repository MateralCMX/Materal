using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace Materal.RabbitMQHelper.Model
{
    /// <summary>
    /// 消费启动异步配置
    /// </summary>
    public class ConsumingRunAsyncConfig
    {
        public Func<IModel, ReadOnlyMemory<byte>, object, BasicDeliverEventArgs, ConsumingConfig, Task> ReceivedAsync { get; set; }
        public Func<IModel, object, ConsumerEventArgs, ConsumingConfig, Task> RegisteredAsync { get; set; }
        public Func<IModel, object, ConsumerEventArgs, ConsumingConfig, Task> UnregisteredAsync { get; set; }
        public Func<IModel, object, ShutdownEventArgs, ConsumingConfig, Task> ShutdownAsync { get; set; }
    }
}
