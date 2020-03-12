using Materal.TTA.Common;
using System;

namespace Materal.ConDep.Common
{
    /// <summary>
    /// 基础实体
    /// </summary>
    [Serializable]
    public abstract class BaseEntity : IEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected BaseEntity()
        {
            ID = Guid.NewGuid();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.MinValue;
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
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
