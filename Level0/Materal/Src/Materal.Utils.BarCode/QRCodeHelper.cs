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
        /// 读取二维码
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        /// <exception cref="UtilException"></exception>
        public static string ReadQRCode(SKBitmap bitmap)
        {
            string result = BarCodeHelper.ReadBarCode(bitmap, out BarcodeFormat barcodeFormat);
            if (barcodeFormat != BarcodeFormat.QR_CODE) throw new UtilException("图片不是二维码");
            return result;
        }
    }
    /// <summary>
    /// 条码帮助类
    /// </summary>
    public static class BarCodeHelper
    {
        /// <summary>
        /// 读取二维码
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="barcodeFormat"></param>
        /// <returns></returns>
        /// <exception cref="UtilException"></exception>
        public static string ReadBarCode(SKBitmap bitmap, out BarcodeFormat barcodeFormat)
        {
            BarcodeReader reader = new();
            Result result = reader.Decode(bitmap) ?? throw new UtilException("读取条码失败");
            string text = result.Text;
            barcodeFormat = result.BarcodeFormat;
            return text;
        }
    }
}
