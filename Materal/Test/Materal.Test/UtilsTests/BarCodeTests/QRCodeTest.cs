using Materal.Utils.BarCode;
using SkiaSharp;

namespace Materal.Test.UtilsTests.BarCodeTests
{
    /// <summary>
    /// 二维码测试类
    /// </summary>
    [TestClass]
    public class QRCodeTest : MateralTestBase
    {
        /// <summary>
        /// 二维码测试
        /// </summary>
        [TestMethod]
        public void QRCodeImageTest()
        {
            const string input = "HelloWorld";
            //创建二维码
            string savePath = @"D:\QRCodeImage.png";
            SKBitmap qrCodeImage = QRCodeHelper.CreateQRCode(input, 300);
            qrCodeImage.SaveAs(savePath);
            //读取二维码
            SKBitmap diskQRCodeImage = SKBitmap.Decode(savePath);
            string result = QRCodeHelper.ReadQRCode(diskQRCodeImage);
            Assert.AreEqual(input, result);
        }
    }
}
