using Materal.TTA.Common;
using System;

namespace Materal.ApplicationUpdate.Domain
{
    public class EntityModel : IEntity<Guid>
    {
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
