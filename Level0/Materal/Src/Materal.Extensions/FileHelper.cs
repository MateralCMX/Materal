using System.Drawing;
using System.Drawing.Imaging;

namespace Materal.Extensions
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetBase64Image(string filePath)
        {
            using FileStream fileStream = new(filePath, FileMode.Open);
            return GetBase64Image(fileStream);
        }
        /// <summary>
        /// 获得Base64的图片
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public static string GetBase64Image(FileStream fileStream)
        {
            byte[] imageArray = new byte[fileStream.Length];
            fileStream.Position = 0;
            fileStream.Read(imageArray, 0, (int)fileStream.Length);
            return "data:image/png;base64," + Convert.ToBase64String(imageArray);
        }
    }
}
