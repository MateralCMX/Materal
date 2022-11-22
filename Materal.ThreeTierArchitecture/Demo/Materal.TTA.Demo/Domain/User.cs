using Materal.TTA.Common;

namespace Materal.TTA.Demo.Domain
{
    public class User : IEntity<Guid>
    {
        public Guid ID { get; set; }
    }
}
