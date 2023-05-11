using Materal.TTA.Common;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    public abstract class BaseDomain : IDomain, IEntity<Guid>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set;} = DateTime.Now;
    }
}
