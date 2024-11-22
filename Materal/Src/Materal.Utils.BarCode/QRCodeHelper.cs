using SkiaSharp;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp;

namespace Materal.Utils.BarCode
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public static class QRCodeHelper
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, int size = 300) => CreateQRCode(content, size, size);
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="heigth"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, int heigth, int width) => CreateQRCode(content, new EncodingOptions
        {
            Height = heigth,
            Width = width
        });
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encodingOptions"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, EncodingOptions encodingOptions)
        {
            BarcodeWriter writer = new()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = encodingOptions
            };
            SKBitmap result = writer.Write(content);
            return result;
        }
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="size"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, int size, SKColor foregroundColor, SKColor backgroundColor) => CreateQRCode(content, size, size, foregroundColor, backgroundColor);
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="heigth"></param>
        /// <param name="width"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, int heigth, int width, SKColor foregroundColor, SKColor backgroundColor) => CreateQRCode(content, new EncodingOptions
        {
            Height = heigth,
            Width = width
        }, foregroundColor, backgroundColor);
        /// <summary>
        /// 创建带颜色的二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encodingOptions"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public static SKBitmap CreateQRCode(string content, EncodingOptions encodingOptions, SKColor foregroundColor, SKColor backgroundColor)
        {
            SKBitmap qrCode = CreateQRCode(content, encodingOptions);
            SKBitmap result = ColorReplace(qrCode, foregroundColor, backgroundColor);
            return result;
        }
        /// <summary>
        /// 替换颜色
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        /// <returns></returns>
        public static SKBitmap ColorReplace(SKBitmap qrCode, SKColor foregroundColor, SKColor backgroundColor)
        {
            SKBitmap coloredQRCode = new(qrCode.Width, qrCode.Height);
            using SKCanvas canvas = new(coloredQRCode);
            canvas.Clear(backgroundColor);
            using SKPaint paint = new();
            SKColor? oldBackgroundColor = null;
            for (int x = 0; x < qrCode.Width; x++)
            {
                for (int y = 0; y < qrCode.Height; y++)
                {
                    oldBackgroundColor ??= qrCode.GetPixel(x, y);
                    if (qrCode.GetPixel(x, y) == oldBackgroundColor) continue;
                    paint.Color = foregroundColor;
                    canvas.DrawPoint(x, y, paint);
                }
            }
            return coloredQRCode;
        }
        /// <summary>
        /// 读取二维码
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        /// <exception cref="UtilException"></exception>
        public static string ReadQRCode(SKBitmap bitmap)
        {
            try
            {
                string result = BarCodeHelper.ReadBarCode(bitmap, out BarcodeFormat barcodeFormat);
                if (barcodeFormat != BarcodeFormat.QR_CODE) throw new UtilException("图片不是二维码");
                return result;
            }
            catch
            {
                SKBitmap blackWhiteQRCode = ColorReplace(bitmap, SKColors.Black, SKColors.White);
                string result = BarCodeHelper.ReadBarCode(blackWhiteQRCode, out BarcodeFormat barcodeFormat);
                if (barcodeFormat != BarcodeFormat.QR_CODE) throw new UtilException("图片不是二维码");
                return result;
            }
        }
    }
}
