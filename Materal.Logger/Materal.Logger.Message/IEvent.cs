namespace Materal.Logger.Message
{
    public interface IEvent
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string EventName { get; set; }
    }
}
