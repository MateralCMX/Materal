using System;
using Materal.TTA.Common;

namespace Materal.ApplicationUpdate.Domain
{
    /// <summary>
    /// 应用程序日志
    /// </summary>
    public class ApplicationLog : IEntity<int>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志触发点
        /// </summary>
        public string Logger { get; set; }
        /// <summary>
        /// 触发位置
        /// </summary>
        public string Callsite { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get; set; }
    }
}
