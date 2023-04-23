using System;

namespace Materal.Model.Example
{
    public class ExampleByFilterModel
    {
        public void Example()
        {
            var testModel = new TestModel
            {
                Name = "Materal",
                Age = 20,
                CreateTime = new DateTime(1993, 4, 20),
                Value = 20,
                IsDelete = false,
                MemberID = Guid.NewGuid(),
                TargetID = Guid.NewGuid()
            };
            var filterModel = new TestFilterModel
            {
                Name = "M",
                Age = 20,
                StartTime = new DateTime(1993, 4, 20),
                EndTime = new DateTime(2019, 7, 11),
                MinValue = 10,
                MaxValue = 40,
                IsDelete = true,
                MemberID = testModel.MemberID,
                TargetID = testModel.TargetID
            };
            Func<TestModel, bool> searchDelegate = filterModel.GetSearchDelegate<TestModel>();
            bool? result = searchDelegate?.Invoke(testModel);
        }
    }
    public class TestFilterModel : FilterModel
    {
        [Contains]
        public string Name { get; set; }
        [GreaterThanOrEqual("CreateTime")]
        public DateTime? StartTime { get; set; }
        [LessThanOrEqual("CreateTime")]
        public DateTime? EndTime { get; set; }
        [Equal]
        public int? Age { get; set; }
        [NotEqual]
        public bool? IsDelete { get; set; }
        [GreaterThan("Value")]
        public float? MinValue { get; set; }
        [LessThan("Value")]
        public float? MaxValue { get; set; }
        [Equal]
        public Guid? TargetID { get; set; }
        [Equal]
        public Guid? MemberID { get; set; }
    }
    public class TestModel
    {
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public int Age { get; set; }
        public bool IsDelete { get; set; }
        public float? Value { get; set; }
        public Guid? TargetID { get; set; }
        public Guid MemberID { get; set; }
    }
}
