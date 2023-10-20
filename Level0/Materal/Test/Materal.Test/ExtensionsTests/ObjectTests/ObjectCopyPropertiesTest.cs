namespace Materal.Test.ExtensionsTests.ObjectTests
{
    [TestClass]
    public class ObjectCopyPropertiesTest
    {
        [TestMethod]
        public void CopyPropertiesTest1()
        {
            TestModel1 testModel1 = new();
            BaseTestModel testModel2 = new TestModel2();
            testModel1.CopyProperties(testModel2);
        }
        public abstract class BaseTestModel
        {
            public TestModel3 TestModel2Value { get; set; } = new();
            public List<TestModel3> TestModel2ListValue { get; set; } = new() { new TestModel3(), new TestModel3() };
        }
        public class TestModel1 : BaseTestModel
        {
            public Guid GuidValue { get; set; } = Guid.NewGuid();
            public string StringValue { get; set; } = "Materal";
            public int IntValue { get; set; } = 1;
            public DateTime DateTimeValue { get; set; } = DateTime.Now;
            public bool BoolValue { get; set; } = true;
        }
        public class TestModel2 : BaseTestModel
        {
            public Guid? GuidValue { get; set; } = Guid.NewGuid();
            public string? StringValue { get; set; } = "Materal";
            public int? IntValue { get; set; } = 1;
            public DateTime? DateTimeValue { get; set; } = DateTime.Now;
            public bool? BoolValue { get; set; } = true;
        }
        public class TestModel3
        {
            public Guid GuidValue { get; set; } = Guid.NewGuid();
            public string StringValue { get; set; } = "Materal";
            public int IntValue { get; set; } = 1;
            public DateTime DateTimeValue { get; set; } = DateTime.Now;
            public bool BoolValue { get; set; } = true;
        }
    }
}
