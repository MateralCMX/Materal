using System.Security.Cryptography;

namespace Materal.Extensions
{
    /// <summary>
    /// 流扩展
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 获得MD5加密结果(32位)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string ToFileMd5_32Encode(this string filePath, bool isLower = false)
        {
            if (File.Exists(filePath)) throw new ExtensionException("文件不存在");
            using var stream = new FileStream(filePath, FileMode.Open);
            return stream.ToMd5_32Encode(isLower);
        }
        /// <summary>
        /// 获得MD5加密结果(16位)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string ToFileMd5_16Encode(this string filePath, bool isLower = false)
        {
            if (File.Exists(filePath)) throw new ExtensionException("文件不存在");
            using var stream = new FileStream(filePath, FileMode.Open);
            return stream.ToMd5_16Encode(isLower);
        }
        /// <summary>
        /// 获得MD5加密结果(32位)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string ToMd5_32Encode(this Stream stream, bool isLower = false)
        {
            MD5 md5 = MD5.Create();
            byte[] resultValue = md5.ComputeHash(stream);
            var stringBuilder = new StringBuilder();
            foreach (byte value in resultValue)
            {
                stringBuilder.Append(value.ToString("x2"));
            }
            string result = stringBuilder.ToString();
            result = isLower ? result.ToLower() : result.ToUpper();
            return result;
        }
        /// <summary>
        /// 获得MD5加密结果(16位)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string ToMd5_16Encode(this Stream stream, bool isLower = false) => stream.ToMd5_32Encode(isLower).Substring(8, 16);
    }
}
