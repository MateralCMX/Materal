namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// 通的扩展
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        /// 创建交换机
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="exchangeName"></param>
        /// <param name="type"></param>
        public static void TryCreateExchange(this IModel channel, string exchangeName, string type = "direct") => channel.ExchangeDeclare(exchangeName, type);
        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public static QueueDeclareOk TryCreateQueue(this IModel channel, string queueName) => channel.QueueDeclare(queueName, true, false, false, null);
    }
}
