﻿using Materal.StringHelper;
using Newtonsoft.Json;
using System.Reflection;
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
        private readonly static Dictionary<string, Type> _cacheTypes = new();
        private readonly static object _operationCacheObjectLock = new();
        /// <summary>
        /// Json转换为XML文档对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>XML文档对象</returns>
        public static XmlDocument? JsonToXml(this string jsonStr) => JsonConvert.DeserializeXmlNode(jsonStr);
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
        /// <param name="jsonStr"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static object JsonToObject(this string jsonStr, string typeName)
        {
            Type? triggerDataType = GetTypeByTypeName(typeName, Array.Empty<object>());
            if (triggerDataType == null) throw new MateralConvertException("转换失败");
            object? result = jsonStr.JsonToObject(triggerDataType);
            if (result == null) throw new MateralConvertException("转换失败");
            return result;
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object JsonToObject(this string jsonStr, Type type)
        {
            try
            {
                object? model = Activator.CreateInstance(type);
                if (model == null) throw new MateralConvertException("创建实体失败");
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
                T? model = ConvertManager.GetDefaultObject<T>();
                if (model == null) return model;
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
        public static object? JsonToDeserializeObject(this string jsonStr)
        {
            try
            {
                object? model = JsonConvert.DeserializeObject(jsonStr);
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
        public static T? JsonToDeserializeObject<T>(this string jsonStr)
        {
            try
            {
                T? model = JsonConvert.DeserializeObject<T>(jsonStr);
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
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="jsonStr"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        /// <exception cref="MateralConvertException"></exception>
        public static T? JsonToInterface<T>(this string jsonStr, string typeName)
        {
            Type? triggerDataType = typeName.GetTypeByTypeName<T>();
            return triggerDataType == null ? default : (T)jsonStr.JsonToObject(triggerDataType);
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="filter">过滤器</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Func<Type, bool> filter, params Type[] args)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? triggerDataType = null;
            if (_cacheTypes.ContainsKey(typeName))
            {
                triggerDataType = _cacheTypes[typeName];
            }
            else
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Type[] argTypes = args.Select(m => m.GetType()).ToArray();
                foreach (Assembly assembly in assemblies)
                {
                    Type? targetType = assembly.GetTypes().Where(m => filter(m)).FirstOrDefault();
                    if (targetType == null) continue;
                    ConstructorInfo? constructorInfo = targetType.GetConstructor(argTypes);
                    if (constructorInfo == null) continue;
                    triggerDataType = targetType;
                    break;
                }
            }
            if (triggerDataType == null) return null;
            if (!_cacheTypes.ContainsKey(typeName))
            {
                lock (_operationCacheObjectLock)
                {

                    if (!_cacheTypes.ContainsKey(typeName))
                    {
                        _cacheTypes.Add(typeName, triggerDataType);
                    }
                }
            }
            return triggerDataType;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, params Type[] args) => typeName.GetTypeByTypeName(m => m.Name == typeName && m.IsClass && !m.IsAbstract, args);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="MateralConvertException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, params object[] args)
        {
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            return GetTypeByTypeName(typeName, m => m.Name == typeName && m.IsClass && !m.IsAbstract, argTypes);
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="MateralConvertException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? triggerDataType = GetTypeByTypeName(typeName, args);
            if (triggerDataType == null || !triggerDataType.IsAssignableTo(typeof(T))) return null;
            return triggerDataType;
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, params object[] args)
        {
            Type? type = GetTypeByTypeName(typeName, args);
            if (type == null) return null;
            return type.Instantiation(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeName<T>(this string typeName, params object[] args)
        {
            Type? type = GetTypeByTypeName<T>(typeName, args);
            if (type == null) return default;
            object? typeObject = type.Instantiation(args);
            if (typeObject == null || typeObject is not T result) return default;
            return result;
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType(this string typeName, Type parentType, params Type[] args) => typeName.GetTypeByTypeName((m => m.Name == typeName && m.IsClass && !m.IsAbstract && TypeExtension.IsNameAssignableTo(m, parentType)), args);
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <typeparam name="T">父级类型</typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType<T>(this string typeName, params Type[] args) => typeName.GetTypeByParentType(parentType: typeof(T), args);
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByParentType(this string typeName, Type parentType, params object[] args)
        {
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            Type? type = typeName.GetTypeByParentType(parentType, argTypes);
            if (type == null) return null;
            return type.Instantiation(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? GetObjectByParentType<T>(this string typeName, params object[] args)
        {
            object? obj = typeName.GetObjectByParentType(parentType: typeof(T), args);
            if (obj == null || obj is not T result) return default;
            return result;
        }
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToHexByte(this string hexString)
        {
            if (!hexString.IsHexNumber()) throw new MateralConvertException("16进制字符串有误");
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
        /// 转换为64位SHA256加密字符串
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="isLower">小写</param>
        /// <returns></returns>
        public static string ToSHA256_64Encode(this string inputStr, bool isLower = false)
        {
            if (inputStr == null) throw new ArgumentNullException(nameof(inputStr));
            using SHA256 sha256 = SHA256.Create();
            byte[] output = sha256.ComputeHash(Encoding.Default.GetBytes(inputStr));
            string outputStr = BitConverter.ToString(output).Replace("-", "");
            outputStr = isLower ? outputStr.ToLower() : outputStr.ToUpper();
            return outputStr;
        }
        /// <summary>
        /// 转换为40位SHA256加密字符串
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="isLower">小写</param>
        /// <returns></returns>
        public static string ToSHA1_40Encode(this string inputStr, bool isLower = false)
        {
            if (inputStr == null) throw new ArgumentNullException(nameof(inputStr));
            using SHA1 sha1 = SHA1.Create();
            byte[] output = sha1.ComputeHash(Encoding.Default.GetBytes(inputStr));
            string outputStr = BitConverter.ToString(output).Replace("-", "");
            outputStr = isLower ? outputStr.ToLower() : outputStr.ToUpper();
            return outputStr;
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
            using MD5 md5 = MD5.Create();
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
                outPutStr1 = inputStr[..(count / 2)];
                outPutStr2 = inputStr[(count / 2)..];
            }
            else
            {
                outPutStr1 = inputStr[..((count / 2) + 1)];
                outPutStr2 = inputStr[((count / 2) + 1)..];
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
        private const string InputIv = "MateralC";
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="inputString">需要解密的字符串</param>
        /// <param name="inputKey">密钥,必须为8位字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>解密后的字符串</returns>
        public static string ToDesEncode(this string inputString, string inputKey, Encoding? encoding = null)
        {
            return ToDesEncode(inputString, inputKey, InputIv, encoding);
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="inputString">需要解密的字符串</param>
        /// <param name="inputKey">密钥,必须为8位字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecode(this string inputString, string inputKey, Encoding? encoding = null)
        {
            return DesDecode(inputString, inputKey, InputIv, encoding);
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="inputString">需要加密的字符串</param>
        /// <param name="inputKey">密钥,必须为8位字符串</param>
        /// <param name="inputIv">向量,必须为8位字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>加密后的字符串</returns>
        public static string ToDesEncode(this string inputString, string inputKey, string inputIv, Encoding? encoding = null)
        {
            if (inputKey.Length != 8) throw new MateralConvertException("密钥必须为8位");
            if (inputIv.Length != 8) throw new MateralConvertException("向量必须为8位");
            encoding ??= Encoding.UTF8;
            DES dsp = DES.Create();
            using var memoryStream = new MemoryStream();
            byte[] key = encoding.GetBytes(inputKey);
            byte[] iv = encoding.GetBytes(inputIv);
            using var cryptoStream = new CryptoStream(memoryStream, dsp.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(inputString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            memoryStream.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="inputString">需要解密的字符串</param>
        /// <param name="inputKey">密钥,必须为8位字符串</param>
        /// <param name="inputIv">向量,必须为8位字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecode(this string inputString, string inputKey, string inputIv, Encoding? encoding = null)
        {
            if (inputKey.Length != 8) throw new MateralConvertException("密钥必须为8位");
            if (inputIv.Length != 8) throw new MateralConvertException("向量必须为8位");
            encoding ??= Encoding.UTF8;
            DES dsp = DES.Create();
            byte[] buffer = Convert.FromBase64String(inputString);
            using var memoryStream = new MemoryStream();
            byte[] key = encoding.GetBytes(inputKey);
            byte[] iv = encoding.GetBytes(inputIv);
            using var cryptoStream = new CryptoStream(memoryStream, dsp.CreateDecryptor(key, iv), CryptoStreamMode.Write);
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();
            return encoding.GetString(memoryStream.ToArray());
        }
        /// <summary>
        /// 获取RSA
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<string, string> GetRSAKey()
        {
            var RSA = new RSACryptoServiceProvider();
            string publicKey = RSA.ToXmlString(false);
            string privateKey = RSA.ToXmlString(true);
            return new KeyValuePair<string, string>(publicKey, privateKey);
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encryptKey">加密key</param>
        /// <returns></returns>
        public static string ToRSAEncode(this string content, string encryptKey)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(encryptKey);
            var ByteConverter = new UnicodeEncoding();
            byte[] DataToEncrypt = ByteConverter.GetBytes(content);
            byte[] resultBytes = rsa.Encrypt(DataToEncrypt, false);
            return Convert.ToBase64String(resultBytes);
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="decryptKey">解密key</param>
        /// <returns></returns>
        public static string RSADecode(this string content, string decryptKey)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(content);
            var RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(decryptKey);
            byte[] resultBytes = RSA.Decrypt(dataToDecrypt, false);
            var ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetString(resultBytes);
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="publicKey">公开密钥</param>
        /// <param name="privateKey">私有密钥</param>
        /// <returns>加密后结果</returns>
        public static string ToRSAEncode(this string content, out string publicKey, out string privateKey)
        {
            var rsaProvider = new RSACryptoServiceProvider();
            publicKey = rsaProvider.ToXmlString(false);
            privateKey = rsaProvider.ToXmlString(true);
            var ByteConverter = new UnicodeEncoding();
            byte[] DataToEncrypt = ByteConverter.GetBytes(content);
            byte[] resultBytes = rsaProvider.Encrypt(DataToEncrypt, false);
            return Convert.ToBase64String(resultBytes);
        }

        ///// <summary>
        ///// 获得二维码
        ///// </summary>
        ///// <param name="inputStr">需要加密的字符串</param>
        ///// <param name="pixelsPerModule">每个模块的像素</param>
        ///// <param name="darkColor">暗色</param>
        ///// <param name="lightColor">亮色</param>
        ///// <param name="icon">图标</param>
        ///// <returns>二维码图片</returns>
        //public static Bitmap ToQRCode(this string inputStr, int pixelsPerModule = 20, Color? darkColor = null, Color? lightColor = null, Bitmap? icon = null)
        //{
        //    darkColor ??= Color.Black;
        //    lightColor ??= Color.White;
        //    var qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(inputStr, QRCodeGenerator.ECCLevel.H);
        //    var qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(pixelsPerModule, darkColor.Value, lightColor.Value, icon);
        //    return qrCodeImage;
        //}
    }
}
