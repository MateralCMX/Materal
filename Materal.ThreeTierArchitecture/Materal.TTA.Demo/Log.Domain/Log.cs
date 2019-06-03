using Domain;

namespace Log.Domain
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log : BaseEntity<int>
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Application { get; set;}
        /// <summary>
        /// 日志等级
        /// </summary>
        public string Level { get; set;}
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set;}
        /// <summary>
        /// 日志
        /// </summary>
        public string Logger { get; set;}
        /// <summary>
        /// 调用点
        /// </summary>
        public string Callsite { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get; set; }
    }
}
