using System.Drawing.Imaging;

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
        public static string GetBase64Image(this Image image) => image.GetBase64Image(ImageFormat.Jpeg);
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string GetBase64Image(this Image image, ImageFormat imageFormat)
        {
            using MemoryStream memoryStream = new();
            image.Save(memoryStream, imageFormat);
            byte[] imageArray = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(imageArray, 0, (int)memoryStream.Length);
            return "data:image/png;base64," + Convert.ToBase64String(imageArray);
        }
    }
}
