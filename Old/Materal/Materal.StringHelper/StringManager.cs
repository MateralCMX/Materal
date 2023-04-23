using System;

namespace Materal.StringHelper
{
    public class StringManager
    {
        /// <summary>
        /// 获得随机字符串(GUID模式)
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MateralStringHelperException"></exception>
        public static string GetRandomStrByGuid(int minLength, int maxLength)
        {
            if (minLength <= 0) throw new MateralStringHelperException("长度必须大于0");
            if (minLength >= maxLength) throw new MateralStringHelperException("最大长度必须大于最小长度");
            var rd = new Random();
            int length = rd.Next(minLength, maxLength);
            return GetRandomStrByGuid(length);
        }
        /// <summary>
        /// 获得随机字符串(GUID模式)
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MateralStringHelperException"></exception>
        public static string GetRandomStrByGuid(int length = 32)
        {
            if (length <= 0) throw new MateralStringHelperException("长度必须大于0");
            string resM = string.Empty;
            int count = length % 32 == 0 ? length / 32 : length / 32 + 1;
            for (int i = 0; i < count; i++)
            {
                resM += Guid.NewGuid().ToString().Replace("-", "");
            }
            return resM.Substring(0, length);
        }
        /// <summary>
        /// 获取随机字符串(字典模式)
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MateralStringHelperException"></exception>
        public static string GetRandomStrByDictionary(int minLength, int maxLength, string dictionary = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            if (minLength <= 0) throw new MateralStringHelperException("长度必须大于0");
            if (minLength >= maxLength) throw new MateralStringHelperException("最大长度必须大于最小长度");
            var rd = new Random();
            int length = rd.Next(minLength, maxLength);
            return GetRandomStrByDictionary(length, dictionary);
        }
        /// <summary>
        /// 获取随机字符串(字典模式)
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MateralStringHelperException"></exception>
        public static string GetRandomStrByDictionary(int length = 32, string dictionary = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            if (length <= 0) throw new MateralStringHelperException("长度必须大于0");
            string resM = string.Empty;
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                resM += dictionary[rd.Next(0, dictionary.Length)];
            }
            return resM;
        }
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        /// <exception cref="MateralStringHelperException"></exception>
        public static string GetRandomStrByTick(int length)
        {
            if (length <= 0) throw new MateralStringHelperException("长度必须大于0");
            int rep = 0;
            string str = string.Empty;
            long tick = DateTime.Now.Ticks + rep++;
            var random = new Random((int)((ulong)tick & 0xffffffffL) | (int)(tick >> rep));
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
                str = str + ch;
            }
            return str;
        }
    }
}
