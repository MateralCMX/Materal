using Materal.TTA.Common;
using System;

namespace Materal.ApplicationUpdate.Domain
{
    /// <summary>
    /// 实体模型
    /// </summary>
    public class EntityModel : IEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public EntityModel()
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
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
