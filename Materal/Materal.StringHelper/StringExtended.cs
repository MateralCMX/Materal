using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Materal.StringHelper
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtended
    {
        /// <summary>
        /// 是否为空或空字符串
        /// </summary>
        /// <param name="inputStr">要验证的字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsNullOrEmpty(this string inputStr)
        {
            return string.IsNullOrEmpty(inputStr);
        }
        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="inputStr">要验证的字符串</param>
        /// <param name="regStr">验证正则表达式</param>
        /// <returns>验证结果</returns>
        public static bool VerifyRegex(this string inputStr, string regStr)
        {
            bool resM = regStr.IsNullOrEmpty();
            if (!resM && !inputStr.IsNullOrEmpty())
            {
                resM = Regex.IsMatch(inputStr, regStr);
            }
            return resM;
        }
        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="inputStr">要验证的字符串</param>
        /// <param name="regStr">验证正则表达式</param>
        /// <param name="isPerfect">完全匹配</param>
        /// <returns>验证结果</returns>
        public static bool VerifyRegex(this string inputStr, string regStr, bool isPerfect)
        {
            regStr = isPerfect ? GetPerfectRegStr(regStr) : regStr;
            return inputStr.VerifyRegex(regStr);
        }
        /// <summary>
        /// 获得所有匹配的字符串
        /// </summary>
        /// <param name="inputStr">要验证的字符串</param>
        /// <param name="regStr">验证正则表达式</param>
        /// <returns></returns>
        public static MatchCollection GetVerifyRegex(this string inputStr, string regStr)
        {
            MatchCollection resM = null;
            if (!inputStr.IsNullOrEmpty() && !regStr.IsNullOrEmpty())
            {
                resM = Regex.Matches(inputStr, regStr);
            }
            return resM;
        }
        /// <summary>
        /// 获得所有匹配的字符串
        /// </summary>
        /// <param name="inputStr">要验证的字符串</param>
        /// <param name="regStr">验证正则表达式</param>
        /// <param name="isPerfect">完全匹配</param>
        /// <returns></returns>
        public static MatchCollection GetVerifyRegex(this string inputStr, string regStr, bool isPerfect)
        {
            regStr = isPerfect ? GetPerfectRegStr(regStr) : regStr;
            return inputStr.GetVerifyRegex(regStr);
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
        /// 验证输入字符串是否为IPv4地址(无端口号)
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是IPv4地址(无端口号)
        /// false不是IPv4地址(无端口号)
        /// </returns>
        public static bool IsIPv4(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.InternetProtocolV4, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的IPv4地址(无端口号)
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的IPv4地址(无端口号)
        /// </returns>
        public static MatchCollection GetIPv4InStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.InternetProtocolV4, false);
        }
        /// <summary>
        /// 验证输入字符串是否为IPv4地址(带端口号)
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是IPv4地址(带端口号)
        /// false不是IPv4地址(带端口号)
        /// </returns>
        public static bool IsIPv4AndPort(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.InternetProtocolV4AndPort, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的IPv4地址(带端口号)
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的IPv4地址(带端口号)
        /// </returns>
        public static MatchCollection GetIPv4AndPortInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.InternetProtocolV4AndPort, false);
        }
        /// <summary>
        /// 验证输入字符串是否为邮箱
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是邮箱
        /// false不是邮箱
        /// </returns>
        public static bool IsEMail(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.EMail, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的邮箱
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的邮箱
        /// </returns>
        public static MatchCollection GetEMailInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.EMail, false);
        }
        /// <summary>
        /// 验证输入字符串是否为实数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是实数
        /// false不是实数
        /// </returns>
        public static bool IsNumber(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.Number, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的实数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的实数
        /// </returns>
        public static MatchCollection GetNumberInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.Number, false);
        }
        /// <summary>
        /// 验证输入字符串是否为正实数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是正实数
        /// false不是正实数
        /// </returns>
        public static bool IsNumberPositive(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.NumberPositive, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的正实数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的正实数
        /// </returns>
        public static MatchCollection GetNumberPositiveInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.NumberPositive, false);
        }
        /// <summary>
        /// 验证输入字符串是否为负实数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是负实数
        /// false不是负实数
        /// </returns>
        public static bool IsNumberNegative(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.NumberNegative, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的负实数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的负实数
        /// </returns>
        public static MatchCollection GetNumberNegativeInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.NumberNegative, false);
        }
        /// <summary>
        /// 验证输入字符串是否为整数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是整数
        /// false不是整数
        /// </returns>
        public static bool IsInteger(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.Integer, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的整数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的整数
        /// </returns>
        public static MatchCollection GetIntegerInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.Integer, false);
        }
        /// <summary>
        /// 验证输入字符串是否为正整数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是正整数
        /// false不是正整数
        /// </returns>
        public static bool IsIntegerPositive(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.IntegerPositive, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的正整数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的正整数
        /// </returns>
        public static MatchCollection GetIntegerPositiveInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.IntegerPositive, false);
        }
        /// <summary>
        /// 验证输入字符串是否为负整数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是负整数
        /// false不是负整数
        /// </returns>
        public static bool IsIntegerNegative(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.IntegerNegative, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的负整数
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的负整数
        /// </returns>
        public static MatchCollection GetIntegerNegativeInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.IntegerNegative, false);
        }
        /// <summary>
        /// 验证输入字符串是否为URL地址
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是URL地址
        /// false不是URL地址
        /// </returns>
        public static bool IsUrl(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.Url, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的URL地址
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的URL地址
        /// </returns>
        public static MatchCollection GetUrlInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.Url, false);
        }
        /// <summary>
        /// 验证输入字符串是否为相对路径
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是相对路径
        /// false不是相对路径
        /// </returns>
        public static bool IsRelativePath(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.RelativePath, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的相对路径
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的相对路径
        /// </returns>
        public static MatchCollection GetRelativePathInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.RelativePath, false);
        }
        /// <summary>
        /// 验证输入字符串是否为文件名
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是文件名
        /// false不是文件名
        /// </returns>
        public static bool IsFileName(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.FileName, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的文件名
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的文件名
        /// </returns>
        public static MatchCollection GetFileNameInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.FileName, false);
        }
        /// <summary>
        /// 验证输入字符串是否为文件夹绝对路径
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是文件名
        /// false不是文件名
        /// </returns>
        public static bool IsAbsoluteDirectoryPath(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.AbsoluteDirectoryPath, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的文件夹绝对路径
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的文件名
        /// </returns>
        public static MatchCollection GetAbsoluteDirectoryPathInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.AbsoluteDirectoryPath, false);
        }
        /// <summary>
        /// 验证输入字符串是否为手机号码
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是手机号码
        /// false不是手机号码
        /// </returns>
        public static bool IsPhoneNumber(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.PhoneNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的手机号码
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的手机号码
        /// </returns>
        public static MatchCollection GetPhoneNumberInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.PhoneNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日期
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>
        /// true是日期
        /// false不是日期
        /// </returns>
        public static bool IsDate(this string inputStr, string delimiter = "-/.")
        {
            if (!inputStr.VerifyRegex(RegexData.Date(delimiter), true)) return false;
            var resM = false;
            char[] delimiters = delimiter.ToCharArray();
            foreach (char item in delimiters)
            {
                string[] dateStr = inputStr.Split(item);
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
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>
        /// 字符串中所有的日期
        /// </returns>
        public static MatchCollection GetDateInStr(this string inputStr, string delimiter = "-/.")
        {
            return inputStr.GetVerifyRegex(RegexData.Date(delimiter), false);
        }
        /// <summary>
        /// 验证输入字符串是否为时间
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是时间
        /// false不是时间
        /// </returns>
        public static bool IsTime(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.Time, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的时间
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的时间
        /// </returns>
        public static MatchCollection GetTimeInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.Time, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日期和时间
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>
        /// true是日期和时间
        /// false不是日期和时间
        /// </returns>
        public static bool IsDateTime(this string inputStr, string delimiter = "-/.")
        {
            return inputStr.VerifyRegex(RegexData.DateTime(delimiter), true);
        }
        /// <summary>
        /// 获取输入字符串中所有的日期和时间
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>
        /// 字符串中所有的日期和时间
        /// </returns>
        public static MatchCollection GetDateTimeInStr(this string inputStr, string delimiter = "-/.")
        {
            return inputStr.GetVerifyRegex(RegexData.DateTime(delimiter), false);
        }
        /// <summary>
        /// 验证输入字符串是否为字母
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLetter(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.Letter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的字母
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLetterInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.Letter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是字母或数字
        /// false不是字母或数字
        /// </returns>
        public static bool IsLetterOrNumber(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.LetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母或数字
        /// </returns>
        public static MatchCollection GetLetterOrNumberInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.LetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为小写字母
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLowerLetterr(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.LowerLetter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的小写字母
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLowerLetterInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.LowerLetter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为小写字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsLowerLetterrOrNumber(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.LowerLetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的小写字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetLowerLetterOrNumberInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.LowerLetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为大写字母
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsUpperLetterr(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.UpperLetter, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的大写字母
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetUpperLetterInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.UpperLetter, false);
        }
        /// <summary>
        /// 验证输入字符串是否为大写字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是字母
        /// false不是字母
        /// </returns>
        public static bool IsUpperLetterrOrNumber(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.UpperLetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的大写字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的字母
        /// </returns>
        public static MatchCollection GetUpperLetterOrNumberInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.UpperLetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为中文
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是中文
        /// false不是中文
        /// </returns>
        public static bool IsChinese(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.Chinese, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的中文
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的中文或数字
        /// </returns>
        public static MatchCollection GetChineseInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.Chinese, false);
        }
        /// <summary>
        /// 验证输入字符串是否为中文或字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是中文或字母或数字
        /// false不是中文或字母或数字
        /// </returns>
        public static bool IsChineseOrLetterOrNumber(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.ChineseLetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的中文或字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的中文或字母或数字
        /// </returns>
        public static MatchCollection GetChineseOrLetterOrNumberInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.ChineseLetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日文
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是日文
        /// false不是日文
        /// </returns>
        public static bool IsJapanese(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.Japanese, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的日文
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的日文或数字
        /// </returns>
        public static MatchCollection GetJapaneseInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.Japanese, false);
        }
        /// <summary>
        /// 验证输入字符串是否为日文或字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是日文或字母或数字
        /// false不是日文或字母或数字
        /// </returns>
        public static bool IsJapaneseOrLetterOrNumber(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.JapaneseLetterNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的日文或字母或数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的日文或字母或数字
        /// </returns>
        public static MatchCollection GetJapaneseOrLetterOrNumberInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.JapaneseLetterNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为十六进制数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是十六进制数字
        /// false不是十六进制数字
        /// </returns>
        public static bool IsHexNumber(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.HexNumber, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的十六进制数字
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的十六进制数字
        /// </returns>
        public static MatchCollection GetHexNumberInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.HexNumber, false);
        }
        /// <summary>
        /// 验证输入字符串是否为Guid
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是Guid
        /// false不是Guid
        /// </returns>
        public static bool IsGuid(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.Guid, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的Guid
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的Guid
        /// </returns>
        public static MatchCollection GetGuidInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.Guid, false);
        }
        #endregion
        #region 复杂验证
        /// <summary>  
        /// 验证输入字符串是否为(中国)身份证 
        /// </summary>  
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="accurate">详细验证</param>  
        /// <returns>
        /// true是(中国)身份证
        /// false不是(中国)身份证
        /// </returns>
        public static bool IsIDCardForChina(this string inputStr, bool accurate = false)
        {
            if (inputStr.IsNullOrEmpty()) return false;
            switch (inputStr.Length)
            {
                case 18 when accurate:
                    return MCheckIDCard18(inputStr);
                case 18:
                    return IsIDCard18ForChina(inputStr);
                case 15 when accurate:
                    return MCheckIDCard15(inputStr);
                case 15:
                    return IsIDCard15ForChina(inputStr);
                default:
                    return false;
            }
        }
        /// <summary>
        /// 验证输入字符串是否为(中国)身份证18位
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是(中国)身份证18位
        /// false不是(中国)身份证18位
        /// </returns>
        public static bool IsIDCard18ForChina(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.IDCard18China, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的(中国)身份证18位
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的(中国)身份证18位
        /// </returns>
        public static MatchCollection GetIDCard18ForChinaInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.IDCard18China, false);
        }
        /// <summary>
        /// 验证输入字符串是否为(中国)身份证15位
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// true是(中国)身份证15位
        /// false不是(中国)身份证15位
        /// </returns>
        public static bool IsIDCard15ForChina(this string inputStr)
        {
            return inputStr.VerifyRegex(RegexData.IDCard15China, true);
        }
        /// <summary>
        /// 获取输入字符串中所有的(中国)身份证15位
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的(中国)身份证15位
        /// </returns>
        public static MatchCollection GetIDCard15ForChinaInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.IDCard15China, false);
        }
        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        /// <param name="inputStr">身份证号码</param>
        /// <returns>
        ///     true验证成功
        ///     false验证失败
        /// </returns>
        private static bool MCheckIDCard18(this string inputStr)
        {
            if (long.TryParse(inputStr.Remove(17), out long n) == false
                || n < Math.Pow(10, 16) || long.TryParse(inputStr.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(inputStr.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//省份验证  
            }
            string birth = inputStr.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            if (DateTime.TryParse(birth, out DateTime _) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] ai = inputStr.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }

            Math.DivRem(sum, 11, out int y);
            return arrVarifyCode[y] == inputStr.Substring(17, 1).ToLower();
        }
        /// <summary>  
        /// 15位身份证号码验证  
        /// </summary>  
        /// <param name="inputStr">身份证号码</param>
        /// <returns>
        ///     true验证成功
        ///     false验证失败
        /// </returns> 
        private static bool MCheckIDCard15(this string inputStr)
        {
            if (long.TryParse(inputStr, out long n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            const string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(inputStr.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//省份验证  
            }
            string birth = inputStr.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            return DateTime.TryParse(birth, out DateTime _);
        }
        /// <summary>
        /// 验证输入字符串是否为磁盘根目录
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="isReal">验证真实的磁盘路径</param>
        /// <returns>
        /// true是磁盘根目录
        /// false不是磁盘根目录
        /// </returns>
        public static bool IsDiskPath(this string inputStr, bool isReal = false)
        {
            if (!inputStr.VerifyRegex(RegexData.DiskPath, true)) return false;
            var isOk = false;
            if (isReal)
            {
                if (inputStr.Length == 2)
                {
                    inputStr += "\\";
                }
                else if (inputStr.Last() != '\\')
                {
                    inputStr = inputStr.Substring(0, 2) + "\\";
                }
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                if (allDrives.Any(disk => disk.Name == inputStr))
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
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的磁盘根目录
        /// </returns>
        public static MatchCollection GetDiskPathInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.DiskPath, false);
        }
        /// <summary>
        /// 验证输入字符串是否为绝对路径
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="isReal">验证真实的磁盘路径</param>
        /// <returns>
        /// true是绝对路径
        /// false不是绝对路径
        /// </returns>
        public static bool IsAbsolutePath(this string inputStr, bool isReal = false)
        {
            if (!inputStr.VerifyRegex(RegexData.AbsolutePath, true)) return false;
            string diskPath = inputStr.Length >= 3 ? inputStr.Substring(0, 3) : inputStr;
            return IsDiskPath(diskPath, isReal);
        }
        /// <summary>
        /// 获取输入字符串中所有的绝对路径
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <returns>
        /// 字符串中所有的绝对路径
        /// </returns>
        public static MatchCollection GetAbsolutePathInStr(this string inputStr)
        {
            return inputStr.GetVerifyRegex(RegexData.AbsolutePath, false);
        }
        #endregion
    }
}
