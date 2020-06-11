using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace Materal.RabbitMQHelper.Model
{
    public class RabbitMQClientConfig : RabbitMQServerConfig
    {
        /// <summary>
        /// 自动确认
        /// </summary>
        public bool AutoAck { get; set; } = false;
        /// <summary>
        /// 最大消息数量
        /// </summary>
        public ushort MaxMessageCount { get; set; } = 1;
        /// <summary>
        /// 接收方法
        /// </summary>
        public Action<IModel, ReadOnlyMemory<byte>, object, BasicDeliverEventArgs, RabbitMQClientConfig> Received { get; set; } = null;
        /// <summary>
        /// 接收方法
        /// </summary>
        public Func<IModel, ReadOnlyMemory<byte>, object, BasicDeliverEventArgs, RabbitMQClientConfig, Task> ReceivedAsync { get; set; } = null;
    }
}
