using Materal.Utils.Model;

namespace Materal.Test.UtilsTests
{
    [TestClass]
    public class KeyValueModelTest : BaseTest
    {
        public void KeyValueModelTest1() 
        {
            KeyValueModel<string, string> keyValueModel = new()
            {
                Key = "key1",
                Value = "value1"
            };
        }
    }
}
