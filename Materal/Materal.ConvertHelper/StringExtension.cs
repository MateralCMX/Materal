using Materal.StringHelper;
using Newtonsoft.Json;
using System;
using System.IO;
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
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static object JsonToDeserializeObject(this string jsonStr)
        {
            try
            {
                object model = JsonConvert.DeserializeObject(jsonStr);
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
        public static T JsonToDeserializeObject<T>(this string jsonStr)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<T>(jsonStr);
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
        public static string ToMd5_32Encode(this string inputStr, bool isLower = false)
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
        public static string ToMd5_16Encode(this string inputStr, bool isLower = false)
        {
            return ToMd5_32Encode(inputStr, isLower).Substring(8, 16);
        }

        /// <summary>
        /// 转换为Base64字符串
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns></returns>
        public static string ToBase64Encode(this string inputStr)
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

        /// <summary>
        /// 栅栏加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>加密后字符串</returns>
        public static string ToFenceEncode(this string inputStr)
        {
            var outPutStr = "";
            var outPutStr2 = "";
            int count = inputStr.Length;
            for (var i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    outPutStr += inputStr[i];
                }
                else
                {
                    outPutStr2 += inputStr[i];
                }
            }
            return outPutStr + outPutStr2;
        }
        /// <summary>
        /// 栅栏解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <returns>解密后字符串</returns>
        public static string FenceDecode(this string inputStr)
        {
            int count = inputStr.Length;
            var outPutStr = "";
            string outPutStr1;
            string outPutStr2;
            var num1 = 0;
            var num2 = 0;
            if (count % 2 == 0)
            {
                outPutStr1 = inputStr.Substring(0, count / 2);
                outPutStr2 = inputStr.Substring(count / 2);
            }
            else
            {
                outPutStr1 = inputStr.Substring(0, (count / 2) + 1);
                outPutStr2 = inputStr.Substring((count / 2) + 1);
            }
            for (var i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    outPutStr += outPutStr1[num1++];
                }
                else
                {
                    outPutStr += outPutStr2[num2++];
                }
            }
            return outPutStr;
        }

        /// <summary>
        /// 移位加密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string ToDisplacementEncode(this string inputStr, int key = 3)
        {
            var outputStr = "";
            if (!inputStr.IsLetter()) throw new MateralConvertException("只能包含英文字母");
            inputStr = inputStr.ToUpper();
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int aCount = alphabet.Length;
            int count = inputStr.Length;
            for (var i = 0; i < count; i++)
            {
                if (inputStr[i] != ' ')
                {
                    for (var j = 0; j < aCount; j++)
                    {
                        if (inputStr[i] != alphabet[j]) continue;
                        int eIndex = j + key;
                        if (eIndex < 0)
                        {
                            eIndex = aCount + eIndex;
                        }
                        while (eIndex >= aCount)
                        {
                            eIndex -= aCount;
                        }
                        outputStr += alphabet[eIndex];
                        break;
                    }
                }
                else
                {
                    outputStr += " ";
                }
            }
            return outputStr;
        }
        /// <summary>
        /// 移位解密
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DisplacementDecode(string inputStr, int key = 3)
        {
            return ToDisplacementEncode(inputStr, -key);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="inputStr">需要加密的字符串</param>
        /// <param name="inputKey">密钥,必须为8位字符串</param>
        /// <param name="inputIv">向量,必须为8位字符串</param>
        /// <param name="ed">编码格式</param>
        /// <returns>加密后的字符串</returns>
        public static string ToDesEncode(this string inputStr, string inputKey, string inputIv, Encoding ed = null)
        {
            var resM = "";
            if (inputKey.Length != 8 || inputIv.Length != 8) return resM;
            if (ed == null)
            {
                ed = Encoding.UTF8;
            }
            byte[] str = ed.GetBytes(inputStr);
            byte[] key = ed.GetBytes(inputKey);
            byte[] iv = ed.GetBytes(inputIv);
            var dCsp = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            try
            {
                cStream.Write(str, 0, str.Length);
                cStream.FlushFinalBlock();
                resM = Convert.ToBase64String(mStream.ToArray());
            }
            finally
            {
                cStream.Close();
                mStream.Close();
            }
            return resM;
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="inputStr">需要解密的字符串</param>
        /// <param name="inputKey">密钥,必须为8位字符串</param>
        /// <param name="inputIv">向量,必须为8位字符串</param>
        /// <param name="ed">编码格式</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecode(this string inputStr, string inputKey, string inputIv, Encoding ed = null)
        {
            var resM = "";
            //if (inputKey.Length != 8 || inputIv.Length != 8) return resM;
            if (ed == null)
            {
                ed = Encoding.UTF8;
            }
            byte[] str = Convert.FromBase64String(inputStr);
            byte[] key = ed.GetBytes(inputKey);
            byte[] iv = ed.GetBytes(inputIv);
            var dCsp = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, dCsp.CreateDecryptor(key, iv), CryptoStreamMode.Write);
            try
            {
                cStream.Write(str, 0, str.Length);
                cStream.FlushFinalBlock();
                resM = ed.GetString(mStream.ToArray());
            }
            finally
            {
                cStream.Close();
                mStream.Close();
            }
            return resM;
        }
    }
}
