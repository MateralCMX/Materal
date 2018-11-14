using System.IO;
using System.Linq;
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
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static object ToObject(this byte[] buff)
        {
            object obj;
            using (var ms = new MemoryStream(buff))
            {
                IFormatter iFormatter = new BinaryFormatter();
                obj = iFormatter.Deserialize(ms);
            }
            return obj;
        }
        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static T ToObject<T>(this byte[] buff)
        {
            object obj = ToObject(buff);
            return obj is T model ? model : default(T);
        }
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
