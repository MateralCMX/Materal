using Materal.Utils.BarCode;
using System.Drawing;

namespace Materal.Test.UtilsTests.BarCodeTests
{
    /// <summary>
    /// 二维码测试类
    /// </summary>
    [TestClass]
    public class QRCodeTest : BaseTest
    {
        /// <summary>
        /// 二维码测试
        /// </summary>
        [TestMethod]
        public void QRCodeImageTest()
        {
            const string input = "HelloWorld";
            //创建二维码
            Bitmap qrCodeImage = QRCodeHelper.CreateQRCode(input, 300);
            //读取二维码
            string result = QRCodeHelper.ReadQRCode(qrCodeImage);
            Assert.AreEqual(input, result);
        }
    }
}
