using Materal.Abstractions;
using System.Drawing;
using System.Drawing.Imaging;
using ImageClass = System.Drawing.Image;

namespace Materal.Utils.Image
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ImageClass GetThumbnailImage(string filePath, int width, int height)
        {
            using ImageClass image = new Bitmap(filePath);
            ImageClass myThumbnail = GetThumbnailImage(image, width, height);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static ImageClass GetThumbnailImage(string filePath, float proportion)
        {
            using ImageClass image = new Bitmap(filePath);
            ImageClass myThumbnail = GetThumbnailImage(image, proportion);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ImageClass GetThumbnailImage(Stream stream, int width, int height)
        {
            using ImageClass image = new Bitmap(stream);
            ImageClass myThumbnail = GetThumbnailImage(image, width, height);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static ImageClass GetThumbnailImage(Stream stream, float proportion)
        {
            using ImageClass image = new Bitmap(stream);
            ImageClass myThumbnail = GetThumbnailImage(image, proportion);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        public static ImageClass GetThumbnailImage(ImageClass image, float proportion)
        {
            (int thumbnailImageWidth, int thumbnailImageHeight) = GetThumbnailImageWidthAndHeight(image.Width, image.Height, proportion);
            ImageClass myThumbnail = GetThumbnailImage(image, thumbnailImageWidth, thumbnailImageHeight);
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static ImageClass GetThumbnailImage(ImageClass image, int width, int height)
        {
#if NETSTANDARD
            ImageClass myThumbnail = image.GetThumbnailImage(width, height, () => false, (nint)0);
#else
            ImageClass myThumbnail = image.GetThumbnailImage(width, height, () => false, 0);
#endif
            return myThumbnail;
        }
        /// <summary>
        /// 获取缩略图的宽高
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="proportion"></param>
        /// <returns></returns>
        private static (int thumbnailImageWidth, int thumbnailImageHeight) GetThumbnailImageWidthAndHeight(int width, int height, float proportion)
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
        private static ImageCodecInfo? GetEncoderInfo(string mimeType)
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
        public static void Compress(ImageClass srcBitmap, Stream destStream, long level, string mimeType = "image/jpeg")
        {
            ImageCodecInfo? myImageCodecInfo = GetEncoderInfo(mimeType);
            if (myImageCodecInfo is null) return;
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
        public static void Compress(ImageClass srcBitMap, string destFile, long level, string mimeType = "image/jpeg")
        {
            using Stream stream = new FileStream(destFile, FileMode.Create);
            Compress(srcBitMap, stream, level, mimeType);
            stream.Close();
        }
    }
}
