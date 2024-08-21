using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace Materal.Test.ExtensionsTests.ObjectTests
{
    [TestClass]
    public class ObjectGetDescriptionTest
    {
        [TestMethod]
        public void GetDescriptionTest()
        {
            ClassA classA = new();
            string description = classA.GetDescription();
            Assert.IsTrue(description == "这是ClassA特性的描述");
            description = classA.GetDescription(nameof(classA.Name));
            Assert.IsTrue(description == "这是ClassA.Name特性的描述");

            description = new ClassB().GetDescription();
            Assert.IsTrue(description == "这是ClassB.Description特性的描述");

            description = new ClassC().GetDescription();
            Assert.IsTrue(description == "这是ClassC.Description属性的描述");

            description = new ClassD().GetDescription();
            Assert.IsTrue(description == "这是ClassD.Description特性的描述");

            description = TestEnum.Value1.GetDescription();
            Assert.IsTrue(description == "这是TestEnum.Value1特性的描述");
            description = TestEnum.Value2.GetDescription();
            Assert.IsTrue(description == "Value2");
        }
        //[Serializable]
        [Description("这是ClassA特性的描述")]
        public class ClassA
        {
            [Description("这是ClassA.Name特性的描述")]
            public string Name { get; set; } = string.Empty;
        }
        [Serializable]
        public class ClassB
        {
            [Description("这是ClassB.Description特性的描述")]
            public string Description { get; set; } = "这是ClassB.Description属性的描述";
        }
        [Serializable]
        public class ClassC
        {
            public string Description { get; set; } = "这是ClassC.Description属性的描述";
        }
        [Serializable]
        public class ClassD
        {
            [Description("这是ClassD.Description特性的描述")]
            public int Description { get; set; }
        }
        public enum TestEnum
        {
            [Description("这是TestEnum.Value1特性的描述")]
            Value1,
            Value2
        }
        [TestMethod]
        public void CloneTest()
        {
            ClassA @class = new();
            ClassA? temp = @class.CloneByXml();
            Assert.IsNotNull(temp);
        }
    }
}
