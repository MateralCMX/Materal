using Materal.StringHelper;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Materal.ConvertHelper
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Json转换为XML文档对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>XML文档对象</returns>
        public static XmlDocument JsonToXml(this string jsonStr)
        {
            return JsonConvert.DeserializeXmlNode(jsonStr);
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static object JsonToObject(this string jsonStr)
        {
            try
            {
                var model = new object();
                JsonConvert.PopulateObject(jsonStr, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new MateralConvertException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static T JsonToObject<T>(this string jsonStr)
        {
            try
            {
                var model = ConvertManager.GetDefultObject<T>();
                JsonConvert.PopulateObject(jsonStr, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new MateralConvertException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToHexByte(this string hexString)
        {
            if (!hexString.IsHexNumber())throw new MateralConvertException("16进制字符串有误");
            try
            {
                hexString = hexString.Replace(" ", "");
                if ((hexString.Length % 2) != 0)
                {
                    hexString += " ";
                }
                var returnBytes = new byte[hexString.Length / 2];
                for (var i = 0; i < returnBytes.Length; i++)
                {
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }
                return returnBytes;
            }
            catch (Exception ex)
            {
                throw new MateralConvertException("16进制字符串有误", ex);
            }
        }
        /// <summary>
        /// 文本转换为二进制字符
        /// </summary>
        /// <param name="inputStr">文本</param>
        /// <param name="digit">位数</param>
        /// <returns>二进制字符串</returns>
        public static string ToBinaryStr(this string inputStr, int digit = 8)
        {
            byte[] data = Encoding.UTF8.GetBytes(inputStr);
            var resStr = new StringBuilder(data.Length * digit);
            foreach (byte item in data)
            {
                resStr.Append(Convert.ToString(item, 2).PadLeft(digit, '0'));
            }
            return resStr.ToString();
        }
        /// <summary>
        /// 二进制字符转换为文本
        /// </summary>
        /// <param name="inputStr">二进制字符串</param>
        /// <param name="digit">位数</param>
        /// <returns>文本</returns>
        public static string BinaryToStr(this string inputStr, int digit = 8)
        {
            int numOfBytes = inputStr.Length / digit;
            var bytes = new byte[numOfBytes];
            for (var i = 0; i < numOfBytes; i++)
            {
                bytes[i] = Convert.ToByte(inputStr.Substring(digit * i, digit), 2);
            }
            return Encoding.UTF8.GetString(bytes);
        }
        /// <summary>
        /// 转换为32位Md5加密字符串
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="isLower">小写</param>
        /// <returns></returns>
        public static string ToMd5_32(this string inputStr, bool isLower = false)
        {
            if (inputStr == null) throw new ArgumentNullException(nameof(inputStr));
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(inputStr));
            string outputStr = BitConverter.ToString(output).Replace("-", "");
            outputStr = isLower ? outputStr.ToLower() : outputStr.ToUpper();
            return outputStr;
        }
        /// <summary>
        /// 转换为16位Md5加密字符串
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="isLower">小写</param>
        /// <returns></returns>
        public static string ToMd5_16(this string inputStr, bool isLower = false)
        {
            return ToMd5_32(inputStr, isLower).Substring(8, 16);
        }
        /// <summary>
        /// 转换为Base64字符串
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns></returns>
        public static string ToBase64(this string inputStr)
        {
            byte[] input = Encoding.ASCII.GetBytes(inputStr);
            return Convert.ToBase64String(input);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static string Base64Decode(this string inputStr)
        {
            try
            {
                byte[] input = Convert.FromBase64String(inputStr);
                return Encoding.Default.GetString(input);
            }
            catch (Exception ex)
            {
                throw new MateralConvertException("解密错误", ex);
            }
        }
    }
}
