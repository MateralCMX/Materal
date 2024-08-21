using Materal.Utils.Model;

namespace Materal.TTA.Demo.Domain
{
    public class QueryTestModel : PageRequestModel
    {
        [Equal]
        public Guid? ID { get; set; }
        [Contains]
        public string? StringType { get; set; }
        [Equal]
        public int? IntType { get; set; }
        [GreaterThanOrEqual("IntType")]
        public int? MinIntType { get; set; }
        [LessThanOrEqual("IntType")]
        public int? MaxIntType { get; set; }
        [Equal]
        public byte? ByteType { get; set; }
        [GreaterThanOrEqual("ByteType")]
        public byte? MinByteType { get; set; }
        [LessThanOrEqual("ByteType")]
        public byte? MaxByteType { get; set; }
        [Equal]
        public decimal? DecimalType { get; set; }
        [GreaterThanOrEqual("DecimalType")]
        public decimal? MinDecimalType { get; set; }
        [LessThanOrEqual("DecimalType")]
        public decimal? MaxDecimalType { get; set; }
        [Equal]
        public TestEnum? EnumType { get; set; }
        [Equal]
        public DateTime? DateTimeType { get; set; }
        [GreaterThanOrEqual("DateTimeType")]
        public DateTime? MinDateTimeType { get; set; }
        [LessThanOrEqual("DateTimeType")]
        public DateTime? MaxDateTimeType { get; set; }
    }
}
