namespace Materal.Test.ExtensionsTests.ObjectTests
{
    [TestClass]
    public class ObjectCopyPropertiesTest
    {
        [TestMethod]
        public void CopyPropertiesTest1()
        {
            TestModel1 testModel1 = new();
            TestModel2 testModel21 = testModel1.CopyProperties<TestModel2>();
            Assert.AreEqual(testModel1.GuidValue, testModel21.GuidValue);
            TestModel2 testModel22 = testModel1.CopyProperties<TestModel2>();
            Assert.AreEqual(testModel1.GuidValue, testModel22.GuidValue);
            TestModel3 testModel31 = testModel1.CopyProperties<TestModel3>();
            Assert.IsNotNull(testModel31.GuidValue);
            Assert.AreEqual(testModel1.GuidValue, testModel31.GuidValue.Value);
            TestModel3 testModel32 = testModel1.CopyProperties<TestModel3>();
            Assert.IsNotNull(testModel32.GuidValue);
            Assert.AreEqual(testModel1.GuidValue, testModel32.GuidValue.Value);
            TestModel1 testModel41 = testModel31.CopyProperties<TestModel1>();
            Assert.AreEqual(testModel41.GuidValue, testModel31.GuidValue.Value);
            TestModel1 testModel42 = testModel31.CopyProperties<TestModel1>();
            Assert.AreEqual(testModel42.GuidValue, testModel31.GuidValue.Value);
        }
        public class TestModel1
        {
            public Guid GuidValue { get; set; } = Guid.NewGuid();
            public string StringValue { get; set; } = "Hello World!";
            public int IntValue { get; set; } = 1;
            public DateTime DateTimeValue { get; set; } = DateTime.Now;
            public bool BoolValue { get; set; } = true;
            public SubClass SubClassValue { get; set; } = new();
        }
        public class TestModel2
        {
            public Guid GuidValue { get; set; } = Guid.NewGuid();
            public string StringValue { get; set; } = "Hello World!2";
            public int IntValue { get; set; } = 2;
            public DateTime DateTimeValue { get; set; } = DateTime.Now;
            public bool BoolValue { get; set; } = true;
            public SubClass SubClassValue { get; set; } = new();
        }
        public class TestModel3
        {
            public Guid? GuidValue { get; set; }
            public string? StringValue { get; set; }
            public int? IntValue { get; set; }
            public DateTime? DateTimeValue { get; set; }
            public bool? BoolValue { get; set; }
            public SubClass? SubClassValue { get; set; }
        }
        public class SubClass
        {
            public Guid GuidValue { get; set; } = Guid.NewGuid();
        }
    }
}
