using Materal.Test.TestData;
using Microsoft.Extensions.Configuration;

namespace Materal.Test.ExtensionsTests
{
    [TestClass]
    public class ConfigurationTest : MateralTestBase
    {
        public override void AddConfig(IConfigurationBuilder builder)
        {
            builder.AddJsonFile(".\\ExtensionsTests\\ConfigurationTest.json", optional: true, reloadOnChange: true);
        }
        [TestMethod]
        public void GetValueTest()
        {
            dynamic? value = Configuration.GetConfigItemToString("StringValue");
            Assert.AreEqual("Materal", value);
            value = Configuration.GetConfigItem<int>("IntValue");
            Assert.AreEqual(123, value);
            value = Configuration.GetConfigItem<int?>("IntValue");
            Assert.AreEqual(123, value);
            value = Configuration.GetConfigItem<TestEnum>("EnumValue");
            Assert.AreEqual(TestEnum.Enum2, value);
            value = Configuration.GetConfigItem<TestEnum?>("EnumValue");
            Assert.AreEqual(TestEnum.Enum2, value);
            value = Configuration.GetConfigItem<TestEnum>("EnumValue2");
            Assert.AreEqual(TestEnum.Enum1, value);
            value = Configuration.GetConfigItem<TestEnum?>("EnumValue2");
            Assert.AreEqual(TestEnum.Enum1, value);
        }
    }
}
