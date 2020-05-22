using Materal.StringHelper.Models;
using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Materal.StringHelper
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 获得中文拼音
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetChinesePinYin(this string inputString, PinYinMode mode = PinYinMode.NoTone)
        {
            var chineseChars = new object[inputString.Length];
            for (var i = 0; i < inputString.Length; i++)
            {
                char inputChar = inputString[i];
                if (!inputChar.ToString().IsChinese())
                {
                    chineseChars[i] = inputChar;
                    continue;
                }
                chineseChars[i] = new ChineseChar(inputChar);
            }
            IEnumerable<string> result = GetPinYin("", 0, chineseChars.ToList(), mode);
            return result.Distinct();
        }

        /// <summary>
        /// 获得拼音
        /// </summary>
        /// <param name="nowPinyin"></param>
        /// <param name="index"></param>
        /// <param name="chineseChars"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetPinYin(string nowPinyin, int index, IReadOnlyList<object> chineseChars, PinYinMode mode)
        {
            var inputPinYin = new List<string>();
            if (chineseChars.Count <= index)
            {
                inputPinYin.Add(nowPinyin); 
            }
            else
            {
                switch (chineseChars[index])
                {
                    case char charString:
                        inputPinYin.AddRange(GetPinYin(nowPinyin + charString, index + 1, chineseChars, mode));
                        break;
                    case ChineseChar chinese:
                        for (var i = 0; i < chinese.PinyinCount; i++)
                        {
                            string pinyin = chinese.Pinyins[i];
                            switch (mode)
                            {
                                case PinYinMode.Full:
                                    pinyin = pinyin.ToLower().FirstUpper();
                                    break;
                                case PinYinMode.NoTone:
                                    pinyin = pinyin.ToLower().FirstUpper().Substring(0, pinyin.Length - 1);
                                    break;
                                case PinYinMode.Abbreviation:
                                    pinyin = pinyin[0].ToString();
                                    break;
                            }
                            string str = pinyin;
                            if (!string.IsNullOrEmpty(nowPinyin))
                            {
                                str = nowPinyin + str;
                            }
                            inputPinYin.AddRange(GetPinYin(str, index + 1, chineseChars, mode));
                        }
                        break;
                }
            }
            return inputPinYin;
        }
        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string FirstLower(this string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString)) return inputString;
            return inputString[0].ToString().ToLower() + inputString.Substring(1);
        }
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string FirstUpper(this string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString)) return inputString;
            return inputString[0].ToString().ToUpper() + inputString.Substring(1);
        }
        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="obj">要验证的字符串</param>
        /// <param name="regStr">验证正则表达式</param>
        /// <returns>验证结果</returns>
        public static bool VerifyRegex(this string obj, string regStr)
        {
            return !string.IsNullOrEmpty(regStr) && !string.IsNullOrEmpty(obj) && Regex.IsMatch(obj, regStr);
        }
        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="obj">要验证的字符串</param>
        /// <param name="regStr">验证正则表达式</param>
        /// <param name="isPerfect">完全匹配</param>
        /// <returns>验证结果</returns>
        public static bool VerifyRegex(this string obj, string regStr, bool isPerfect)
        {
            regStr = isPerfect ? GetPerfectRegStr(regStr) : regStr;
            return obj.VerifyRegex(regStr);
        }
        /// <summary>
        /// 获得所有匹配的字符串
        /// </summary>
        /// <param name="obj">要验证的字符串</param>
        /// <param name="regStr">验证正则表达式</param>
        /// <returns></returns>
        public static MatchCollection GetVerifyRegex(this string obj, string regStr)
        {
            MatchCollection resM = null;
            if (!string.IsNullOrEmpty(regStr) && !string.IsNullOrEmpty(obj))
            {
                resM = Regex.Matches(obj, regStr);
            }
            return resM;
        }
        /// <summary>
        /// 获得所有匹配的字符串
        /// </summary>
        /// <param name="obj">要验证的字符串</param>
        /// <param name="regStr">验证正则表达式</param>
        /// <param name="isPerfect">完全匹配</param>
        /// <returns></returns>
        public static MatchCollection GetVerifyRegex(this string obj, string regStr, bool isPerfect)
        {
            regStr = isPerfect ? GetPerfectRegStr(regStr) : regStr;
            return obj.GetVerifyRegex(regStr);
        }
        /// <summary>
        /// 获得完全匹配的正则表达式
        /// </summary>
        /// <param name="resStr">部分匹配的正则表达式</param>
        /// <returns>完全匹配的正则表达式</returns>
        public static string GetPerfectRegStr(string resStr)
        {
            int length = resStr.Length;
            if (length <= 0) return resStr;
            const char first = '^';
            const char last = '$';
            if (resStr[0] != first)
            {
                resStr = first + resStr;
            }
            if (resStr[resStr.Length - 1] != last)
            {
                resStr += last;
            }
            return resStr;
        }
        /// <summary>
        /// 获得不完全匹配的正则表达式
        /// </summary>
        /// <param name="resStr">部分匹配的正则表达式</param>
        /// <returns>完全匹配的正则表达式</returns>
        public static string GetNoPerfectRegStr(string resStr)
        {
            int length = resStr.Length;
            if (length <= 0) return resStr;
            const char first = '^';
            const char last = '$';
            if (resStr[0] == first)
            {
                resStr = resStr.Substring(1);
            }
            if (resStr[resStr.Length - 1] == last)
            {
                resStr = resStr.Substring(0, resStr.Length - 1);
            }
            return resStr;
        }
        #region 简单验证
        /// <summary>
        /// 验证输入字符串是否为16进制颜色
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是16进制颜色
        /// false不是16进制颜色
        /// </returns>
        public static bool IsHexColor(this string obj)
        {
            return obj.VerifyRegex(RegexData.HexColor, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的16进制颜色
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的16进制颜色
        /// </returns>
        public static MatchCollection GetHexColor(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.HexColor, false);
        }
        /// <summary>
        /// 验证输入字符串是否为IPv4地址(无端口号)
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是IPv4地址(无端口号)
        /// false不是IPv4地址(无端口号)
        /// </returns>
        public static bool IsIPv4(this string obj)
        {
            return obj.VerifyRegex(RegexData.InternetProtocolV4, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的IPv4地址(无端口号)
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的IPv4地址(无端口号)
        /// </returns>
        public static MatchCollection GetIPv4(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.InternetProtocolV4, false);
        }
        /// <summary>
        /// 验证输入字符串是否为IPv4地址(带端口号)
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是IPv4地址(带端口号)
        /// false不是IPv4地址(带端口号)
        /// </returns>
        public static bool IsIPv4AndPort(this string obj)
        {
            return obj.VerifyRegex(RegexData.InternetProtocolV4AndPort, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的IPv4地址(带端口号)
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的IPv4地址(带端口号)
        /// </returns>
        public static MatchCollection GetIPv4AndPort(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.InternetProtocolV4AndPort, false);
        }
        /// <summary>
        /// 验证输入字符串是否为邮箱
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是邮箱
        /// false不是邮箱
        /// </returns>
        public static bool IsEMail(this string obj)
        {
            return obj.VerifyRegex(RegexData.EMail, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的邮箱
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的邮箱
        /// </returns>
        public static MatchCollection GetEMail(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.EMail, false);
        }
        /// <summary>
        /// 验证输入字符串是否为实数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是实数
        /// false不是实数
        /// </returns>
        public static bool IsNumber(this string obj)
        {
            return obj.VerifyRegex(RegexData.Number, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的实数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的实数
        /// </returns>
        public static MatchCollection GetNumber(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.Number, false);
        }
        /// <summary>
        /// 验证输入字符串是否为正实数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是正实数
        /// false不是正实数
        /// </returns>
        public static bool IsNumberPositive(this string obj)
        {
            return obj.VerifyRegex(RegexData.NumberPositive, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的正实数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的正实数
        /// </returns>
        public static MatchCollection GetNumberPositive(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.NumberPositive, false);
        }
        /// <summary>
        /// 验证输入字符串是否为负实数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是负实数
        /// false不是负实数
        /// </returns>
        public static bool IsNumberNegative(this string obj)
        {
            return obj.VerifyRegex(RegexData.NumberNegative, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的负实数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的负实数
        /// </returns>
        public static MatchCollection GetNumberNegative(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.NumberNegative, false);
        }
        /// <summary>
        /// 验证输入字符串是否为整数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是整数
        /// false不是整数
        /// </returns>
        public static bool IsInteger(this string obj)
        {
            return obj.VerifyRegex(RegexData.Integer, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的整数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的整数
        /// </returns>
        public static MatchCollection GetInteger(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.Integer, false);
        }
        /// <summary>
        /// 验证输入字符串是否为正整数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是正整数
        /// false不是正整数
        /// </returns>
        public static bool IsIntegerPositive(this string obj)
        {
            return obj.VerifyRegex(RegexData.IntegerPositive, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的正整数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的正整数
        /// </returns>
        public static MatchCollection GetIntegerPositive(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.IntegerPositive, false);
        }
        /// <summary>
        /// 验证输入字符串是否为负整数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是负整数
        /// false不是负整数
        /// </returns>
        public static bool IsIntegerNegative(this string obj)
        {
            return obj.VerifyRegex(RegexData.IntegerNegative, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的负整数
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的负整数
        /// </returns>
        public static MatchCollection GetIntegerNegative(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.IntegerNegative, false);
        }
        /// <summary>
        /// 验证输入字符串是否为URL地址
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是URL地址
        /// false不是URL地址
        /// </returns>
        public static bool IsUrl(this string obj)
        {
            return obj.VerifyRegex(RegexData.Url, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的URL地址
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的URL地址
        /// </returns>
        public static MatchCollection GetUrl(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.Url, false);
        }
        /// <summary>
        /// 验证输入字符串是否为相对路径
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是相对路径
        /// false不是相对路径
        /// </returns>
        public static bool IsRelativePath(this string obj)
        {
            return obj.VerifyRegex(RegexData.RelativePath, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的相对路径
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的相对路径
        /// </returns>
        public static MatchCollection GetRelativePath(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.RelativePath, false);
        }
        /// <summary>
        /// 验证输入字符串是否为文件名
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是文件名
        /// false不是文件名
        /// </returns>
        public static bool IsFileName(this string obj)
        {
            return obj.VerifyRegex(RegexData.FileName, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的文件名
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的文件名
        /// </returns>
        public static MatchCollection GetFileName(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.FileName, false);
        }
        /// <summary>
        /// 验证输入字符串是否为文件夹绝对路径
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是文件名
        /// false不是文件名
        /// </returns>
        public static bool IsAbsoluteDirectoryPath(this string obj)
        {
            return obj.VerifyRegex(RegexData.AbsoluteDirectoryPath, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的文件夹绝对路径
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的文件名
        /// </returns>
        public static MatchCollection GetAbsoluteDirectoryPath(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.AbsoluteDirectoryPath, false);
        }
        /// <summary>
        /// 验证输入字符串是否为手机号码
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是手机号码
        /// false不是手机号码
        /// </returns>
        public static bool IsPhoneNumber(this string obj)
        {
            return obj.VerifyRegex(RegexData.PhoneNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的手机号码
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的手机号码
        /// </returns>
        public static MatchCollection GetPhoneNumber(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.PhoneNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日期
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>
        /// true是日期
        /// false不是日期
        /// </returns>
        public static bool IsDate(this string obj, string delimiter = "-/.")
        {
            if (!obj.VerifyRegex(RegexData.Date(delimiter), true)) return false;
            var resM = false;
            char[] delimiters = delimiter.ToCharArray();
            foreach (char item in delimiters)
            {
                string[] dateStr = obj.Split(item);
                if (dateStr.Length != 3) continue;
                int month = int.Parse(dateStr[1]);
                if (month == 2)
                {
                    int year = int.Parse(dateStr[0]);
                    int day = int.Parse(dateStr[2]);
                    if (year % 4 == 0)
                    {
                        resM = true;
                    }
                    else
                    {
                        if (day <= 28)
                        {
                            resM = true;
                        }
                    }
                }
                else
                {
                    resM = true;
                }
                break;
            }
            return resM;
        }
        /// <summary>
        /// 获取输入字符串中所有的日期
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>
        /// 字符串中所有的日期
        /// </returns>
        public static MatchCollection GetDate(this string obj, string delimiter = "-/.")
        {
            return obj.GetVerifyRegex(RegexData.Date(delimiter), false);
        }
        /// <summary>
        /// 验证输入字符串是否为时间
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是时间
        /// false不是时间
        /// </returns>
        public static bool IsTime(this string obj)
        {
            return obj.VerifyRegex(RegexData.Time, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的时间
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的时间
        /// </returns>
        public static MatchCollection GetTime(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.Time, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日期和时间
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>
        /// true是日期和时间
        /// false不是日期和时间
        /// </returns>
        public static bool IsDateTime(this string obj, string delimiter = "-/.")
        {
            if (!obj.VerifyRegex(RegexData.DateTime(delimiter), true)) return false;
            int index = obj.IndexOf(' ');
            if (index < 0) index = obj.IndexOf('T');
            if (index < 0) return false;
            string dataStr = obj.Substring(0, index);
            return dataStr.IsDate(delimiter);
        }
        /// <summary>
        /// 获取输入字符串中所有的日期和时间
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>
        /// 字符串中所有的日期和时间
        /// </returns>
        public static MatchCollection GetDateTime(this string obj, string delimiter = "-/.")
        {
            return obj.GetVerifyRegex(RegexData.DateTime(delimiter), false);
        }
        /// <summary>
        /// 验证输入字符串是否为字母
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLetter(this string obj)
        {
            return obj.VerifyRegex(RegexData.Letter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的字母
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLetter(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.Letter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是字母或数字
        /// false不是字母或数字
        /// </returns>
        public static bool IsLetterOrNumber(this string obj)
        {
            return obj.VerifyRegex(RegexData.LetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母或数字
        /// </returns>
        public static MatchCollection GetLetterOrNumber(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.LetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为小写字母
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLowerLetterr(this string obj)
        {
            return obj.VerifyRegex(RegexData.LowerLetter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的小写字母
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLowerLetter(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.LowerLetter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为小写字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLowerLetterrOrNumber(this string obj)
        {
            return obj.VerifyRegex(RegexData.LowerLetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的小写字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLowerLetterOrNumber(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.LowerLetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为大写字母
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsUpperLetterr(this string obj)
        {
            return obj.VerifyRegex(RegexData.UpperLetter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的大写字母
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetUpperLetter(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.UpperLetter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为大写字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsUpperLetterrOrNumber(this string obj)
        {
            return obj.VerifyRegex(RegexData.UpperLetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的大写字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetUpperLetterOrNumber(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.UpperLetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为中文
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是中文
        /// false不是中文
        /// </returns>
        public static bool IsChinese(this string obj)
        {
            return obj.VerifyRegex(RegexData.Chinese, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的中文
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的中文或数字
        /// </returns>
        public static MatchCollection GetChinese(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.Chinese, false);
        }
        /// <summary>
        /// 验证输入字符串是否为中文或字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是中文或字母或数字
        /// false不是中文或字母或数字
        /// </returns>
        public static bool IsChineseOrLetterOrNumber(this string obj)
        {
            return obj.VerifyRegex(RegexData.ChineseLetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的中文或字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的中文或字母或数字
        /// </returns>
        public static MatchCollection GetChineseOrLetterOrNumber(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.ChineseLetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日文
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是日文
        /// false不是日文
        /// </returns>
        public static bool IsJapanese(this string obj)
        {
            return obj.VerifyRegex(RegexData.Japanese, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的日文
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的日文或数字
        /// </returns>
        public static MatchCollection GetJapanese(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.Japanese, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日文或字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是日文或字母或数字
        /// false不是日文或字母或数字
        /// </returns>
        public static bool IsJapaneseOrLetterOrNumber(this string obj)
        {
            return obj.VerifyRegex(RegexData.JapaneseLetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的日文或字母或数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的日文或字母或数字
        /// </returns>
        public static MatchCollection GetJapaneseOrLetterOrNumber(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.JapaneseLetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为十六进制数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是十六进制数字
        /// false不是十六进制数字
        /// </returns>
        public static bool IsHexNumber(this string obj)
        {
            return obj.VerifyRegex(RegexData.HexNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的十六进制数字
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的十六进制数字
        /// </returns>
        public static MatchCollection GetHexNumber(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.HexNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为Guid
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是Guid
        /// false不是Guid
        /// </returns>
        public static bool IsGuid(this string obj)
        {
            return obj.VerifyRegex(RegexData.Guid, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的Guid
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的Guid
        /// </returns>
        public static MatchCollection GetGuid(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.Guid, false);
        }
        #endregion
        #region 复杂验证
        /// <summary>  
        /// 验证输入字符串是否为(中国)身份证 
        /// </summary>  
        /// <param name="obj">输入的字符串</param>
        /// <param name="accurate">详细验证</param>  
        /// <returns>
        /// true是(中国)身份证
        /// false不是(中国)身份证
        /// </returns>
        public static bool IsIDCardForChina(this string obj, bool accurate = false)
        {
            if (string.IsNullOrEmpty(obj)) return false;
            switch (obj.Length)
            {
                case 18 when accurate:
                    return MCheckIDCard18(obj);
                case 18:
                    return IsIDCard18ForChina(obj);
                case 15 when accurate:
                    return MCheckIDCard15(obj);
                case 15:
                    return IsIDCard15ForChina(obj);
                default:
                    return false;
            }
        }
        /// <summary>
        /// 验证输入字符串是否为(中国)身份证18位
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是(中国)身份证18位
        /// false不是(中国)身份证18位
        /// </returns>
        public static bool IsIDCard18ForChina(this string obj)
        {
            return obj.VerifyRegex(RegexData.IDCard18China, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的(中国)身份证18位
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的(中国)身份证18位
        /// </returns>
        public static MatchCollection GetIDCard18ForChina(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.IDCard18China, false);
        }
        /// <summary>
        /// 验证输入字符串是否为(中国)身份证15位
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// true是(中国)身份证15位
        /// false不是(中国)身份证15位
        /// </returns>
        public static bool IsIDCard15ForChina(this string obj)
        {
            return obj.VerifyRegex(RegexData.IDCard15China, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的(中国)身份证15位
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的(中国)身份证15位
        /// </returns>
        public static MatchCollection GetIDCard15ForChina(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.IDCard15China, false);
        }
        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        /// <param name="obj">身份证号码</param>
        /// <returns>
        ///     true验证成功
        ///     false验证失败
        /// </returns>
        private static bool MCheckIDCard18(this string obj)
        {
            if (long.TryParse(obj.Remove(17), out long n) == false
                || n < Math.Pow(10, 16) || long.TryParse(obj.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(obj.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//省份验证  
            }
            string birth = obj.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            if (DateTime.TryParse(birth, out DateTime _) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = obj.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }

            Math.DivRem(sum, 11, out int y);
            return arrVarifyCode[y] == obj.Substring(17, 1).ToLower();
        }
        /// <summary>  
        /// 15位身份证号码验证  
        /// </summary>  
        /// <param name="obj">身份证号码</param>
        /// <returns>
        ///     true验证成功
        ///     false验证失败
        /// </returns> 
        private static bool MCheckIDCard15(this string obj)
        {
            if (long.TryParse(obj, out long n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(obj.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//省份验证  
            }
            string birth = obj.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            return DateTime.TryParse(birth, out DateTime _);
        }
        /// <summary>
        /// 验证输入字符串是否为磁盘根目录
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <param name="isReal">验证真实的磁盘路径</param>
        /// <returns>
        /// true是磁盘根目录
        /// false不是磁盘根目录
        /// </returns>
        public static bool IsDiskPath(this string obj, bool isReal = false)
        {
            if (!obj.VerifyRegex(RegexData.DiskPath, true)) return false;
            var isOk = false;
            if (isReal)
            {
                if (obj.Length == 2)
                {
                    obj += "\\";
                }
                else if (obj.Last() != '\\')
                {
                    obj = obj.Substring(0, 2) + "\\";
                }
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                if (allDrives.Any(disk => disk.Name == obj))
                {
                    isOk = true;
                }
            }
            else
            {
                isOk = true;
            }
            return isOk;
        }
        /// <summary>
        /// 获取输入字符串中所有的磁盘根目录
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的磁盘根目录
        /// </returns>
        public static MatchCollection GetDiskPath(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.DiskPath, false);
        }
        /// <summary>
        /// 验证输入字符串是否为绝对路径
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <param name="isReal">验证真实的磁盘路径</param>
        /// <returns>
        /// true是绝对路径
        /// false不是绝对路径
        /// </returns>
        public static bool IsAbsolutePath(this string obj, bool isReal = false)
        {
            if (!obj.VerifyRegex(RegexData.AbsolutePath, true)) return false;
            string diskPath = obj.Length >= 3 ? obj.Substring(0, 3) : obj;
            return IsDiskPath(diskPath, isReal);
        }
        /// <summary>
        /// 获取输入字符串中所有的绝对路径
        /// </summary>
        /// <param name="obj">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的绝对路径
        /// </returns>
        public static MatchCollection GetAbsolutePath(this string obj)
        {
            return obj.GetVerifyRegex(RegexData.AbsolutePath, false);
        }
        #endregion
    }
}
