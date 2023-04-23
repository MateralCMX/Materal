using Materal.Common;
using System.Security.Cryptography;
using System.Text;

namespace Materal.ConvertHelper
{
    public static class StreamExtension
    {
        /// <summary>
        /// 获得MD5加密结果(32位)
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string ToFileMd5_32Encode(this string inputString, bool isLower = false)
        {
            if (File.Exists(inputString)) throw new MateralException("文件不存在");
            using var stream = new FileStream(inputString, FileMode.Open);
            return stream.ToMd5_32Encode(isLower);
        }
        /// <summary>
        /// 获得MD5加密结果(16位)
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string ToFileMd5_16Encode(this string inputString, bool isLower = false)
        {
            if (File.Exists(inputString)) throw new MateralException("文件不存在");
            using var stream = new FileStream(inputString, FileMode.Open);
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
        public static string ToMd5_16Encode(this Stream stream, bool isLower = false)
        {
            return stream.ToMd5_32Encode(isLower).Substring(8, 16);
        }
    }
}
