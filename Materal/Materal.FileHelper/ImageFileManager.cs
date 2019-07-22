using Materal.Common;
using Materal.ConvertHelper;
using System;
using System.Drawing;
using System.IO;

namespace Materal.FileHelper
{
    public static class ImageFileManager
    {
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetBase64Image(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var imageArray = new byte[fileStream.Length];
                fileStream.Position = 0;
                fileStream.Read(imageArray, 0, (int)fileStream.Length);
                return "data:image/png;base64," + Convert.ToBase64String(imageArray);
            }
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetThumbnailImage(string filePath, int width, int height)
        {
            Image image = new Bitmap(filePath);
            Image myThumbnail = GetThumbnailImage(image, width, height);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static Image GetThumbnailImage(string filePath, float proportion)
        {
            Image image = new Bitmap(filePath);
            Image myThumbnail = GetThumbnailImage(image, proportion);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetThumbnailImage(Stream stream, int width, int height)
        {
            Image image = new Bitmap(stream);
            Image myThumbnail = GetThumbnailImage(image, width, height);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static Image GetThumbnailImage(Stream stream, float proportion)
        {
            Image image = new Bitmap(stream);
            Image myThumbnail = GetThumbnailImage(image, proportion);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static Image GetThumbnailImage(Image image, float proportion)
        {
            (int thumbnailImageWidth, int thumbnailImageHeight) = GetThumbnailImageWidthAndHeight(image.Width, image.Height, proportion);
            Image myThumbnail = GetThumbnailImage(image, thumbnailImageWidth, thumbnailImageHeight);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetThumbnailImage(Image image, int width, int height)
        {
            bool MyCallback() => false;
            Image myThumbnail = image.GetThumbnailImage(width, height, MyCallback, IntPtr.Zero);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图的宽高
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        private static (int thumbnailImageWidth, int thumbnailImageHeight) GetThumbnailImageWidthAndHeight(int width, int height,float proportion)
        {
            if (proportion <= 0) throw new MateralException("比例必须大于0");
            var thumbnailImageWidth = (width * proportion).ConvertTo<int>();
            var thumbnailImageHeight = (height * proportion).ConvertTo<int>();
            return (thumbnailImageWidth, thumbnailImageHeight);
        }
    }
}
