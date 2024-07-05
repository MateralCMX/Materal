using System.Text;

namespace Materal.Utils.Text
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 获得随机字符串(GUID模式)
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="UtilException"></exception>
        public static string GetRandomStringByGuid(int minLength, int maxLength)
        {
            if (minLength <= 0) throw new UtilException("长度必须大于0");
            if (minLength >= maxLength) throw new UtilException("最大长度必须大于最小长度");
            Random rd = new();
            int length = rd.Next(minLength, maxLength);
            return GetRandomStringByGuid(length);
        }
        /// <summary>
        /// 获得随机字符串(GUID模式)
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="UtilException"></exception>
        public static string GetRandomStringByGuid(int length = 32)
        {
            if (length <= 0) throw new UtilException("长度必须大于0");
            StringBuilder result = new();
            int count = length % 32 == 0 ? length / 32 : length / 32 + 1;
            for (int i = 0; i < count; i++)
            {
                result.Append(Guid.NewGuid().ToString("N"));
            }
            return result.ToString(0, length);
        }
        /// <summary>
        /// 获取随机字符串(字典模式)
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="UtilException"></exception>
        public static string GetRandomStringByDictionary(int minLength, int maxLength, string dictionary = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            if (minLength <= 0) throw new UtilException("长度必须大于0");
            if (minLength >= maxLength) throw new UtilException("最大长度必须大于最小长度");
            Random rd = new();
            int length = rd.Next(minLength, maxLength);
            return GetRandomStringByDictionary(length, dictionary);
        }
        /// <summary>
        /// 获取随机字符串(字典模式)
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="UtilException"></exception>
        public static string GetRandomStringByDictionary(int length = 32, string dictionary = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            if (length <= 0) throw new UtilException("长度必须大于0");
            Random rd = new();
            StringBuilder result = new(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(dictionary[rd.Next(0, dictionary.Length)]);
            }
            return result.ToString();
        }
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="UtilException"></exception>
        public static string GetRandomStringByTick(int length)
        {
            if (length <= 0) throw new UtilException("长度必须大于0");
            int rep = 0;
            string str = string.Empty;
            long tick = DateTime.Now.Ticks + rep++;
            Random random = new((int)((ulong)tick & 0xffffffffL) | (int)(tick >> rep));
            for (int i = 0; i < length; i++)
            {
                char ch;
                int num = random.Next();
                if (num % 2 == 0)
                {
                    ch = (char)(0x30 + (ushort)(num % 10));
                }
                else
                {
                    ch = (char)(0x41 + (ushort)(num % 0x1a));
                }
                str += ch;
            }
            return str;
        }
    }
}
