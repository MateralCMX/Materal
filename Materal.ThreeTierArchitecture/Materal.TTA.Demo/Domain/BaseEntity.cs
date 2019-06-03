using Materal.TTA.Common;
using System;

namespace Domain
{
    /// <summary>
    /// 基础实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEntity<T> : IEntity<T>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected BaseEntity()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.MinValue;
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public T ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
