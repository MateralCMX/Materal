using Materal.Utils.BarCode;
using SkiaSharp;
using System.Drawing;

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
        public void ChangeQRCodeTest()
        {
            string input = string.Empty;
            for (int i = 0; i < 40; i++)
            {
                input += "Poi!";
            }
            int[] size = [
                300,
                600,
                900
            ];
            foreach (int item in size)
            {
                //创建二维码
                string savePath = @$"D:\Test\[{item}]QRCodeImage.png";
                SKBitmap qrCodeImage = QRCodeHelper.CreateQRCode(input, item);
                qrCodeImage.SaveAs(savePath);
                SKColor GetPaintColor(Point centerPoint)
                {
                    if (centerPoint.Y > item / 3 * 2)
                    {
                        return SKColors.LightBlue;
                    }
                    else if (centerPoint.Y > item / 3)
                    {
                        return SKColors.Orange;
                    }
                    else
                    {
                        return SKColors.LightGreen;
                    }
                }
                //创建圆点二维码
                savePath = @$"D:\Test\[{item}]QRCodeImage-圆点.png";
                SKBitmap newQrCodeImage = QRCodeHelper.ChangeQRCodeImage(qrCodeImage, (canvas, paint, centerPoint, pointSize) =>
                {
                    float radius = pointSize.Width / 2;
                    paint.Color = GetPaintColor(centerPoint);
                    canvas.DrawCircle(centerPoint.X, centerPoint.Y, radius, paint);
                }, (paint, centerPoint) => paint.Color = GetPaintColor(centerPoint), SKColors.White);
                newQrCodeImage.SaveAs(savePath);
                //创建小方块二维码
                savePath = @$"D:\Test\[{item}]QRCodeImage-方块.png";
                newQrCodeImage = QRCodeHelper.ChangeQRCodeImage(qrCodeImage, (canvas, paint, centerPoint, pointSize) =>
                {
                    paint.Color = GetPaintColor(centerPoint);
                    canvas.DrawRect(centerPoint.X - pointSize.Width / 2, centerPoint.Y - pointSize.Height / 2, pointSize.Width, pointSize.Height, paint);
                }, (paint, centerPoint) => paint.Color = GetPaintColor(centerPoint), SKColors.White);
                newQrCodeImage.SaveAs(savePath);
                //创建图片二维码
                savePath = @$"D:\Test\[{item}]QRCodeImage-图片.png";
                string imagePath = @$"D:\Test\PT头像[{item}].png";
                SKBitmap image = SKBitmap.Decode(imagePath);
                newQrCodeImage = QRCodeHelper.ChangeQRCodeImage(qrCodeImage, (canvas, paint, centerPoint, pointSize) =>
                {
                    int startX = centerPoint.X - pointSize.Width / 2;
                    int startY = centerPoint.Y - pointSize.Height / 2;
                    for (int i = 0; i < pointSize.Width; i++)
                    {
                        for (int j = 0; j < pointSize.Height; j++)
                        {
                            int x = startX + i;
                            int y = startY + j;
                            paint.Color = image.GetPixel(x, y);
                            canvas.DrawRect(x, y, 1, 1, paint);
                        }
                    }
                }, (paint, centerPoint) => paint.Color = GetPaintColor(centerPoint), SKColors.White);
                newQrCodeImage.SaveAs(savePath);
            }
        }
    }
}
