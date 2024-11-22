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
        private string _input = string.Empty;
        public QRCodeTest()
        {
            for (int i = 0; i < 20; i++)
            {
                _input += "Poi!";
            }
        }
        /// <summary>
        /// 二维码测试
        /// </summary>
        [TestMethod]
        public void QRCodeImageTest()
        {
            //创建二维码
            string savePath = @"D:\QRCodeImage.png";
            SKBitmap qrCodeImage = QRCodeHelper.CreateQRCode(_input, 900);
            qrCodeImage.SaveAs(savePath);
            //读取二维码
            SKBitmap diskQRCodeImage = SKBitmap.Decode(savePath);
            string result = QRCodeHelper.ReadQRCode(diskQRCodeImage);
            Assert.AreEqual(_input, result);
        }
        /// <summary>
        /// 彩色二维码测试
        /// </summary>
        [TestMethod]
        public void QRCodeColorImageTest()
        {
            //创建二维码
            string savePath = @"D:\QRCodeColorImage.png";
            SKBitmap qrCodeImage = QRCodeHelper.CreateQRCode(_input, 900, SKColors.LightBlue, SKColors.White);
            qrCodeImage.SaveAs(savePath);
            //读取二维码
            SKBitmap diskQRCodeImage = SKBitmap.Decode(savePath);
            string result = QRCodeHelper.ReadQRCode(diskQRCodeImage);
            Assert.AreEqual(_input, result);
        }
        /// <summary>
        /// 自定义前景图片二维码测试
        /// </summary>
        [TestMethod]
        public void QRCodeCustomForegroundImageTest()
        {
            //创建二维码
            string savePath = @"D:\QRCodeCustomForegroundImage.png";
            SKBitmap foregroundImage = SKBitmap.Decode(@"C:\Users\cloom\OneDrive\图片\表情包\PT头像.jpg");
            SKBitmap qrCodeImage = QRCodeHelper.CreateQRCode(_input, foregroundImage, SKColors.Red);
            qrCodeImage.SaveAs(savePath);
            //读取二维码
            SKBitmap diskQRCodeImage = SKBitmap.Decode(savePath);
            string result = QRCodeHelper.ReadQRCode(diskQRCodeImage);
            Assert.AreEqual(_input, result);
        }
        /// <summary>
        /// 自定义背景图片二维码测试
        /// </summary>
        [TestMethod]
        public void QRCodeCustomBackgroundImageTest()
        {
            //创建二维码
            string savePath = @"D:\QRCodeCustomBackgroundImage.png";
            SKBitmap backgroundImage = SKBitmap.Decode(@"C:\Users\cloom\OneDrive\图片\表情包\PT头像.jpg");
            SKBitmap qrCodeImage = QRCodeHelper.CreateQRCode(_input, SKColors.Red, backgroundImage);
            qrCodeImage.SaveAs(savePath);
        }
    }
}
