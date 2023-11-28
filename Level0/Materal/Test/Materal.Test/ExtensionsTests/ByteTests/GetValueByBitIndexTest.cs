namespace Materal.Test.ExtensionsTests.ByteTests
{
    [TestClass]
    public class GetValueByBitIndexTest : BaseTest
    {
        [TestMethod]
        public void Test01()
        {
            byte[] byteArray = new byte[] { 255, 170 };
            string oldBinaryString = byteArray.GetBinaryString();
            int result = byteArray.GetIntValueByBitIndex(3, 12);
            byte[] byteResult = byteArray.GetValueByBitIndex(3, 12);
            string newBinaryString = result.GetBinaryString();
        }
    }
}
