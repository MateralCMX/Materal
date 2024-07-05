using Materal.Extensions;
using System.Drawing.Imaging;
using ImageClass = System.Drawing.Image;

namespace Materal.Utils.Image
{
    /// <summary>
    /// Image扩展
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetBase64Image(this ImageClass image) => image.GetBase64Image(ImageFormat.Png);
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string GetBase64Image(this ImageClass image, ImageFormat imageFormat)
        {
            using MemoryStream memoryStream = new();
            image.Save(memoryStream, imageFormat);
            return memoryStream.ToBase64Image();
        }
    }
}
