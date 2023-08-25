using Microsoft.Extensions.Configuration;

namespace Materal.Test.ExtensionsTests
{
    [TestClass]
    public class ConfigurationTest
    {
        private readonly IConfiguration _configuration;
        public ConfigurationTest()
        {
            _configuration = new ConfigurationBuilder()
                        .AddJsonFile(".\\ExtensionsTests\\ConfigurationTest.json", optional: true, reloadOnChange: true)
                        .Build();
        }
        [TestMethod]
        public void GetValueTest()
        {
            dynamic? value = _configuration.GetValue("StringValue");
            Assert.AreEqual("Materal", value);
            value = _configuration.GetValueObject<int>("IntValue");
            Assert.AreEqual(123, value);
        }
    }
}
