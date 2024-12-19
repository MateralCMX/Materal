using Materal.Extensions;
using SkiaSharp;

namespace Materal.Utils.Image
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public static class ImageHelper
    {
        #region 图片压缩
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="proportion"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static SKBitmap Compress(string imageFilePath, uint proportion = 50, SKEncodedImageFormat? imageFormat = null, uint quality = 50)
        {
            using SKBitmap srcBitmap = SKBitmap.Decode(imageFilePath);
            imageFormat ??= GetImageFormatFromFile(imageFilePath);
            return srcBitmap.Compress(proportion, imageFormat.Value, quality);
        }
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static SKBitmap Compress(string imageFilePath, int width, int height, SKEncodedImageFormat? imageFormat = null, uint quality = 50)
        {
            using SKBitmap srcBitmap = SKBitmap.Decode(imageFilePath);
            imageFormat ??= GetImageFormatFromFile(imageFilePath);
            return srcBitmap.Compress(width, height, imageFormat.Value, quality);
        }
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static SKBitmap Compress(this SKBitmap bitmap, SKEncodedImageFormat imageFormat, uint quality = 50) => bitmap.Compress(50, imageFormat, quality);
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="proportion"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static SKBitmap Compress(this SKBitmap bitmap, uint proportion, SKEncodedImageFormat imageFormat, uint quality = 50)
        {
            SKImage image = bitmap.GetThumbnailImage(proportion);
            return image.Compress(imageFormat, quality);
        }
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static SKBitmap Compress(this SKBitmap bitmap, int width, int height, SKEncodedImageFormat imageFormat, uint quality = 50)
        {
            SKImage image = bitmap.GetThumbnailImage(width, height);
            return image.Compress(imageFormat, quality);
        }
        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static SKBitmap Compress(this SKImage image, SKEncodedImageFormat imageFormat, uint quality = 50)
        {
            MemoryStream memoryStream = new();
            image.SaveAs(memoryStream, imageFormat, quality);
            SKBitmap result = SKBitmap.Decode(memoryStream.ToArray());
            return result;
        }
        #endregion
        #region 缩略图
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static SKImage GetThumbnailImage(string imageFilePath, uint proportion = 50)
        {
            using SKBitmap srcBitmap = SKBitmap.Decode(imageFilePath);
            return srcBitmap.GetThumbnailImage(proportion);
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static SKImage GetThumbnailImage(string imageFilePath, int width, int height)
        {
            using SKBitmap srcBitmap = SKBitmap.Decode(imageFilePath);
            return srcBitmap.GetThumbnailImage(width, height);
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="proportion">比例0-100</param>
        /// <returns></returns>
        public static SKImage GetThumbnailImage(this SKBitmap image, uint proportion = 50)
        {
            (int width, int height) = GetProportionalDimensions(image.Width, image.Height, proportion);
            SKImage result = GetThumbnailImage(image, width, height);
            return result;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="width">新图片宽度</param>
        /// <param name="height">新图片高度</param>
        /// <returns></returns>
        public static SKImage GetThumbnailImage(this SKBitmap bitmap, int width, int height)
        {
            SKBitmap resizedBitmap = new(width, height);
            SKSamplingOptions samplingOptions = new();
            bitmap.ScalePixels(resizedBitmap, samplingOptions);
            SKImage result = SKImage.FromBitmap(resizedBitmap);
            return result;
        }
        #endregion
        #region 保存图片
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="saveFiltPath"></param>
        /// <param name="quality"></param>
        public static void SaveAs(string imageFilePath, string saveFiltPath, uint quality) => SaveAs(imageFilePath, saveFiltPath, null, quality);
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="saveFiltPath"></param>
        /// <param name="quality"></param>
        /// <param name="imageFormat"></param>
        public static void SaveAs(string imageFilePath, string saveFiltPath, SKEncodedImageFormat? imageFormat = null, uint quality = 100)
        {
            using SKBitmap srcBitmap = SKBitmap.Decode(imageFilePath);
            imageFormat ??= GetImageFormatFromFile(imageFilePath);
            srcBitmap.SaveAs(saveFiltPath, imageFormat, quality);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="saveFiltPath"></param>
        /// <param name="quality"></param>
        public static void SaveAs(this SKBitmap image, string saveFiltPath, uint quality) => SaveAs(image, saveFiltPath, null, quality);
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="saveFiltPath"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        public static void SaveAs(this SKBitmap image, string saveFiltPath, SKEncodedImageFormat? imageFormat = null, uint quality = 100)
        {
            int trueQuality = GetQuality(quality);
            imageFormat ??= GetImageFormatFromFile(saveFiltPath);
            using SKData data = image.Encode(imageFormat.Value, trueQuality);
            data.SaveAs(saveFiltPath);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="saveStream"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        public static void SaveAs(this SKBitmap image, Stream saveStream, SKEncodedImageFormat imageFormat, uint quality = 100)
        {
            int trueQuality = GetQuality(quality);
            using SKData data = image.Encode(imageFormat, trueQuality);
            data.SaveAs(saveStream);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="saveFiltPath"></param>
        /// <param name="quality"></param>
        public static void SaveAs(this SKImage image, string saveFiltPath, uint quality) => SaveAs(image, saveFiltPath, null, quality);
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="saveFiltPath"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        public static void SaveAs(this SKImage image, string saveFiltPath, SKEncodedImageFormat? imageFormat = null, uint quality = 100)
        {
            int trueQuality = GetQuality(quality);
            imageFormat ??= GetImageFormatFromFile(saveFiltPath);
            using SKData data = image.Encode(imageFormat.Value, trueQuality);
            data.SaveAs(saveFiltPath);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="saveStream"></param>
        /// <param name="imageFormat"></param>
        /// <param name="quality"></param>
        public static void SaveAs(this SKImage image, Stream saveStream, SKEncodedImageFormat imageFormat, uint quality = 100)
        {
            int trueQuality = GetQuality(quality);
            using SKData data = image.Encode(imageFormat, trueQuality);
            data.SaveAs(saveStream);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveFiltPath"></param>
        public static void SaveAs(this SKData data, string saveFiltPath)
        {
            FileInfo fileInfo = new(saveFiltPath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            using Stream stream = new FileStream(saveFiltPath, FileMode.Create);
            data.SaveTo(stream);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveStream"></param>
        public static void SaveAs(this SKData data, Stream saveStream) => data.SaveTo(saveStream);
        /// <summary>
        /// 获得质量
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        private static int GetQuality(uint quality)
        {
            if (quality < 0)
            {
                return 50;
            }
            if (quality > 100)
            {
                return 100;
            }
            return (int)quality;
        }
        #endregion
        /// <summary>
        /// 获取图片格式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static SKEncodedImageFormat GetImageFormatFromFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".bmp" => SKEncodedImageFormat.Bmp,
                ".gif" => SKEncodedImageFormat.Gif,
                ".ico" => SKEncodedImageFormat.Ico,
                ".jpg" or ".jpeg" => SKEncodedImageFormat.Jpeg,
                ".png" => SKEncodedImageFormat.Png,
                ".wbmp" => SKEncodedImageFormat.Wbmp,
                ".webp" => SKEncodedImageFormat.Webp,
                ".pkm" => SKEncodedImageFormat.Pkm,
                ".ktx" => SKEncodedImageFormat.Ktx,
                ".astc" => SKEncodedImageFormat.Astc,
                ".dng" => SKEncodedImageFormat.Dng,
                ".heif" => SKEncodedImageFormat.Heif,
                ".avif" => SKEncodedImageFormat.Avif,
                _ => throw new NotSupportedException($"未知的扩展类型: {extension}"),
            };
        }
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string GetBase64Image(this SKImage image, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Png)
        {
            using SKData data = image.Encode(imageFormat, 100);
            byte[] buffer = data.ToArray();
            string result = Convert.ToBase64String(buffer);
            return result;
        }
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string GetBase64Image(this SKBitmap image, SKEncodedImageFormat imageFormat = SKEncodedImageFormat.Png)
        {
            using SKData data = image.Encode(imageFormat, 100);
            byte[] buffer = data.ToArray();
            string result = Convert.ToBase64String(buffer);
            return result;
        }
        #region 私有方法
        /// <summary>
        /// 按比例获取宽高
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="proportion">比例0-100</param>
        /// <returns></returns>
        private static (int width, int height) GetProportionalDimensions(int width, int height, uint proportion = 50)
        {
            if (proportion <= 0)
            {
                proportion = 50;
            }
            int newWidth = (width * proportion).ConvertTo<int>();
            if (newWidth <= 0)
            {
                newWidth = 1;
            }
            int newHeight = (height * proportion).ConvertTo<int>();
            if (newHeight <= 0)
            {
                newHeight = 1;
            }
            return (newWidth, newHeight);
        }
        #endregion
    }
}
