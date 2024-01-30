namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 事件总线异常
    /// </summary>
    public class EventBusException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public EventBusException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public EventBusException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public EventBusException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
