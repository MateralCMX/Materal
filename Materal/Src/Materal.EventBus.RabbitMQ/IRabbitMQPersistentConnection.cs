namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// RabbitMQ持久连接
    /// </summary>
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        bool IsConnected { get; }
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        bool TryConnect();
        /// <summary>
        /// 创建通道
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
        /// <summary>
        /// 创建通道
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <returns></returns>
        IModel CreateModel(string exchangeName);
        /// <summary>
        /// 创建通道
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        IModel CreateModel(string exchangeName, string queueName);
    }
}
