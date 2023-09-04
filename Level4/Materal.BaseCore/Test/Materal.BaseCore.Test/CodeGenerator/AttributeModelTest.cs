using Materal.BaseCore.CodeGenerator.Models;

namespace Materal.BaseCore.Test.CodeGenerator
{
    [TestClass]
    public class AttributeModelTest : BaseTest
    {
        /// <summary>
        /// 无参数特性测试
        /// </summary>
        [TestMethod]
        public void NoArgsAttributeTest()
        {
            string code = "Test";
            AttributeModel attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(0, attributeModel.AttributeArguments.Count);
        }
        /// <summary>
        /// 无指定目标参数特性测试
        /// </summary>
        [TestMethod]
        public void NoTargetArgAttributeTest()
        {
            string code = "Test(\"arg1\", \"arg2\", \"arg3\")";
            AttributeModel attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(3, attributeModel.AttributeArguments.Count);
            Assert.AreEqual(null, attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
            code = "Test(nameof(arg1), nameof(arg2), nameof(arg3))";
            attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(3, attributeModel.AttributeArguments.Count);
            Assert.AreEqual(null, attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
            code = "Test(\"arg1\", nameof(arg2), \"arg3\")";
            attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(3, attributeModel.AttributeArguments.Count);
            Assert.AreEqual(null, attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
        }
        /// <summary>
        /// 指定目标参数特性测试
        /// </summary>
        [TestMethod]
        public void TargetArgAttributeTest()
        {
            string code = "Test(Arg1 = \"arg1\", Arg2 = \"arg2\", Arg3 = \"arg3\")";
            AttributeModel attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(3, attributeModel.AttributeArguments.Count);
            Assert.AreEqual("Arg1", attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual("Arg2", attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual("Arg3", attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
            code = "Test(Arg1 = nameof(arg1), Arg2 = nameof(arg2), Arg3 = nameof(arg3))";
            attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(3, attributeModel.AttributeArguments.Count);
            Assert.AreEqual("Arg1", attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual("Arg2", attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual("Arg3", attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
            code = "Test(Arg1 = \"arg1\", Arg2 = nameof(arg2), Arg3 = \"arg3\")";
            attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(3, attributeModel.AttributeArguments.Count);
            Assert.AreEqual("Arg1", attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual("Arg2", attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual("Arg3", attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
        }
        /// <summary>
        /// 混合参数特性测试
        /// </summary>
        [TestMethod]
        public void MixtureArgAttributeTest()
        {
            string code = "Test(\"arg1\", \"arg2\", Arg3 = \"arg3\", Arg4 = \"arg4\")";
            AttributeModel attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(4, attributeModel.AttributeArguments.Count);
            Assert.AreEqual(null, attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual("Arg3", attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
            Assert.AreEqual("Arg4", attributeModel.AttributeArguments[3].Target);
            Assert.AreEqual("\"arg4\"", attributeModel.AttributeArguments[3].Value);
            code = "Test(nameof(arg1), nameof(arg2), Arg3 = nameof(arg3), Arg4 = nameof(arg4))";
            attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(4, attributeModel.AttributeArguments.Count);
            Assert.AreEqual(null, attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual("Arg3", attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
            Assert.AreEqual("Arg4", attributeModel.AttributeArguments[3].Target);
            Assert.AreEqual("\"arg4\"", attributeModel.AttributeArguments[3].Value);
            code = "Test(\"arg1\", nameof(arg2), Arg3 = \"arg3\", Arg4 = nameof(arg4))";
            attributeModel = new(code);
            Assert.AreEqual("Test", attributeModel.Name);
            Assert.AreEqual(4, attributeModel.AttributeArguments.Count);
            Assert.AreEqual(null, attributeModel.AttributeArguments[0].Target);
            Assert.AreEqual("\"arg1\"", attributeModel.AttributeArguments[0].Value);
            Assert.AreEqual(null, attributeModel.AttributeArguments[1].Target);
            Assert.AreEqual("\"arg2\"", attributeModel.AttributeArguments[1].Value);
            Assert.AreEqual("Arg3", attributeModel.AttributeArguments[2].Target);
            Assert.AreEqual("\"arg3\"", attributeModel.AttributeArguments[2].Value);
            Assert.AreEqual("Arg4", attributeModel.AttributeArguments[3].Target);
            Assert.AreEqual("\"arg4\"", attributeModel.AttributeArguments[3].Value);
        }
    }
}
