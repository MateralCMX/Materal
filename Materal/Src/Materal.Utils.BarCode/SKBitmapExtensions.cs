using SkiaSharp;

namespace Materal.Utils.BarCode
{
    /// <summary>
    /// SKBitmap扩展
    /// </summary>
    public static class SKBitmapExtensions
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="image"></param>
        /// <param name="savePath"></param>
        /// <param name="format"></param>
        public static void SaveAs(this SKBitmap image, string savePath, SKEncodedImageFormat format = SKEncodedImageFormat.Png)
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            using SKData encodedData = image.Encode(format, 100);
            using FileStream fileStream = new(savePath, FileMode.Create, FileAccess.Write);
            encodedData.SaveTo(fileStream);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileInfo"></param>
        /// <param name="format"></param>
        public static void SaveAs(this SKBitmap image, FileInfo fileInfo, SKEncodedImageFormat format = SKEncodedImageFormat.Png) => image.SaveAs(fileInfo.FullName, format);
    }
}
