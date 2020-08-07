using Materal.Common;
using Materal.ConvertHelper;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        /// 获得Base64的图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetBase64Image(this Image image)
        {
            return GetBase64Image(image, ImageFormat.Jpeg);
        }
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string GetBase64Image(this Image image, ImageFormat imageFormat)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, imageFormat);
                var imageArray = new byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(imageArray, 0, (int)memoryStream.Length);
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
            using (Image image = new Bitmap(filePath))
            {
                Image myThumbnail = GetThumbnailImage(image, width, height);
                return myThumbnail;
            }
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static Image GetThumbnailImage(string filePath, float proportion)
        {
            using (Image image = new Bitmap(filePath))
            {
                Image myThumbnail = GetThumbnailImage(image, proportion);
                return myThumbnail;
            }
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
            using (Image image = new Bitmap(stream))
            {
                Image myThumbnail = GetThumbnailImage(image, width, height);
                return myThumbnail;
            }
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static Image GetThumbnailImage(Stream stream, float proportion)
        {
            using (Image image = new Bitmap(stream))
            {
                Image myThumbnail = GetThumbnailImage(image, proportion);
                return myThumbnail;
            }
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
        /// <summary>
        /// 获取编码信息
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType) return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// 图片压缩(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="srcBitmap">传入的Bitmap对象</param>
        /// <param name="destStream">压缩后的Stream对象</param>
        /// <param name="level">压缩等级，0到100，0 最差质量，100 最佳</param>
        /// <param name="mimeType">image/jpeg image/png</param>
        public static void Compress(Image srcBitmap, Stream destStream, long level, string mimeType = "image/jpeg")
        {
            ImageCodecInfo myImageCodecInfo = GetEncoderInfo(mimeType);
            Encoder myEncoder = Encoder.Quality;
            var myEncoderParameters = new EncoderParameters(1);
            var myEncoderParameter = new EncoderParameter(myEncoder, level);
            myEncoderParameters.Param[0] = myEncoderParameter;
            srcBitmap.Save(destStream, myImageCodecInfo, myEncoderParameters);
        }

        /// <summary>
        /// 图片压缩(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="srcBitMap">传入的Bitmap对象</param>
        /// <param name="destFile">压缩后的图片保存路径</param>
        /// <param name="level">压缩等级，0到100，0 最差质量，100 最佳</param>
        /// <param name="mimeType"></param>
        public static void Compress(Image srcBitMap, string destFile, long level, string mimeType = "image/jpeg")
        {
            using (Stream stream = new FileStream(destFile, FileMode.Create))
            {
                Compress(srcBitMap, stream, level, mimeType);
                stream.Close();
            }
        }
    }
}
