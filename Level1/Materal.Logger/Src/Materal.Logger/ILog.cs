namespace Materal.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; }
        /// <summary>
        /// 日志等级
        /// </summary>
        LogLevel Level { get; }
        /// <summary>
        /// 事件ID
        /// </summary>
        EventId EventID { get; }
        /// <summary>
        /// 分类名称
        /// </summary>
        string CategoryName { get; }
        /// <summary>
        /// 状态
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        Exception? Exception { get; }
    }
}
