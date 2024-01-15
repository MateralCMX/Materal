namespace Materal.Test.ExtensionsTests.ByteTests
{
    [TestClass]
    public class GetValueByBitIndexTest : BaseTest
    {
        [TestMethod]
        public void Test01()
        {
            byte[] byteArray = [255, 170];
            string oldBinaryString = byteArray.GetBinaryString();
            Assert.IsNotNull(oldBinaryString);
            int result = byteArray.GetIntValueByBitIndex(3, 12);
            byte[] byteResult = byteArray.GetValueByBitIndex(3, 12);
            Assert.IsNotNull(byteResult);
            string newBinaryString = result.GetBinaryString();
            Assert.IsNotNull(newBinaryString);
        }
    }
}
