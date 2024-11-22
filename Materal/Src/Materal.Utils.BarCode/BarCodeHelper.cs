using SkiaSharp;
using ZXing;
using ZXing.SkiaSharp;

namespace Materal.Utils.BarCode
{
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
