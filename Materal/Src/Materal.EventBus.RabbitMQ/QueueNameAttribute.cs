namespace Materal.EventBus.RabbitMQ
{
    /// <summary>
    /// 队列名称特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class QueueNameAttribute(string name) : Attribute
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string Name { get; } = name;
    }
}
