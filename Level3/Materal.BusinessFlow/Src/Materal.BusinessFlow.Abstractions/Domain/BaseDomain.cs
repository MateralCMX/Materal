using Materal.TTA.Common;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    public abstract class BaseDomain : IBaseDomain, IEntity<Guid>
    {
        [Required]
        public Guid ID { get; set; } = Guid.NewGuid();
        [Required]
        public DateTime CreateTime { get; set;} = DateTime.Now;
    }
}
