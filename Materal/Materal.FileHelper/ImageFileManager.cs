using System;
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
    }
}
