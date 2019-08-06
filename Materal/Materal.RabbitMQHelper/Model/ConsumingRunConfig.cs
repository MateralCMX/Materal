using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Materal.RabbitMQHelper.Model
{
    /// <summary>
    /// 消费启动配置
    /// </summary>
    public class ConsumingRunConfig
    {
        public Action<IModel, byte[], object, BasicDeliverEventArgs, ConsumingConfig> Received { get; set; }
        public Action<IModel, object, ConsumerEventArgs, ConsumingConfig> Registered { get; set; }
        public Action<IModel, object, ConsumerEventArgs, ConsumingConfig> Unregistered { get; set; }
        public Action<IModel, object, ShutdownEventArgs, ConsumingConfig> Shutdown { get; set; }
    }
}
