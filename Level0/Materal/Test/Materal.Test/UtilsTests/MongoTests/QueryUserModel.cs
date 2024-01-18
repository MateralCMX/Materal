namespace Materal.Test.UtilsTests.MongoTests
{
    public class QueryUserModel : PageRequestModel
    {
        [Equal]
        public Guid? ID { get; set; }
        [Contains]
        public string? Name { get; set; }
        [Equal]
        public int? Age { get; set; }
        [GreaterThanOrEqual(nameof(Person.CreateTime))]
        public DateTime? MinCreateTime { get; set; }
        [LessThanOrEqual(nameof(Person.CreateTime))]
        public DateTime? MaxCreateTime { get; set; }
    }
}