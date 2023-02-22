namespace Materal.Logger.Message
{
    /// <summary>
    /// 事件
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string EventName { get; set; }
    }
}
