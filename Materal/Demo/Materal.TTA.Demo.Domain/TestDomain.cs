using Materal.TTA.Common;

namespace Materal.TTA.Demo.Domain
{
    public class TestDomain : IEntity<Guid>
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string StringType { get; set; } = string.Empty;
        public int IntType { get; set; }
        public byte ByteType { get; set; }
        public decimal DecimalType { get; set; }
        public TestEnum EnumType { get; set; }
        public DateTime DateTimeType { get; set; } = DateTime.Now;
    }
}
