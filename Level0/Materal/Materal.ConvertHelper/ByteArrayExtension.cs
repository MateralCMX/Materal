using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Materal.ConvertHelper
{
    /// <summary>
    /// Byte数组扩展
    /// </summary>
    public static class ByteArrayExtension
    {
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexStr(this byte[] bytes)
        {
            return bytes == null ? string.Empty : bytes.Aggregate(string.Empty, (current, item) => current + item.ToString("X2"));
        }
    }
}
