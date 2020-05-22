namespace Materal.StringHelper
{
    /// <summary>
    /// 正则表达式数据类
    /// </summary>
    public class RegexData
    {
        /// <summary>
        /// IPv4正则表达式(不带端口号)
        /// </summary>
        public const string InternetProtocolV4 = @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)";
        /// <summary>
        /// IPv4正则表达式(带端口号)
        /// </summary>
        public const string InternetProtocolV4AndPort = @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?):(6553[0-5]|655[0-2]\d|65[0,4]\d{2}|6[0-4]\d{3}|[1-5]\d{0,4}|[1-9]\d{0,3})";
        /// <summary>
        /// 邮箱地址正则表达式
        /// </summary>
        public const string EMail = @"\w+@\w+\.\w+";
        /// <summary>
        /// 实数正则表达式
        /// </summary>
        public const string Number = @"-?\d+\.?\d{0,}";
        /// <summary>
        /// 正实数正则表达式
        /// </summary>
        public const string NumberPositive = @"\d+\.?\d{0,}";
        /// <summary>
        /// 负实数正则表达式
        /// </summary>
        public const string NumberNegative = @"-\d+\.?\d{0,}";
        /// <summary>
        /// 整数正则表达式
        /// </summary>
        public const string Integer = @"-?\d+";
        /// <summary>
        /// 正整数正则表达式
        /// </summary>
        public const string IntegerPositive = @"\d+";
        /// <summary>
        /// 负整数正则表达式
        /// </summary>
        public const string IntegerNegative = @"-\d+";
        /// <summary>
        /// URL地址(http|https|ftp|rtsp|mms)正则表达式
        /// </summary>
        public const string Url = @"(https|http|ftp|rtsp|mms|ws|wss)://[\S]+";
        /// <summary>
        /// 磁盘根目录正则表达式
        /// </summary>
        private const string DiskPathLeft = @"\w:[\\/]?";
        /// <summary>
        /// 相对路径正则表达式
        /// </summary>
        public const string RelativePath = @"(((~|..:)?(\\|/))?[^\\\?\/\*\|<>:" + "\"" + @"]+)*[\\/]?";
        /// <summary>
        /// 文件名正则表达式
        /// </summary>
        public const string FileName = @"[^\\\?\/\*\|<>:" + "\"" + @"]+([^\.]|\.[^\\\?\/\*\|<>:" + "\"" + @"]+)";
        /// <summary>
        /// 文件夹绝对路径正则表达式
        /// </summary>
        public const string AbsoluteDirectoryPath = @"[\w]:(\\[^\\\?\/\*\|<>:\" + "\"].+)+";
        /// <summary>
        /// 磁盘根目录正则表达式
        /// </summary>
        public static string DiskPath => DiskPathLeft + @"[\\/]?";
        /// <summary>
        /// 绝对路径正则表达式
        /// </summary>
        public static string AbsolutePath => DiskPathLeft + RelativePath;
        /// <summary>
        /// 身份证号码18位中国正则表达式
        /// </summary>
        public const string IDCard18China = @"\d{17}[\d|x|X]{1}";
        /// <summary>
        /// 身份证号码15位中国正则表达式
        /// </summary>
        public const string IDCard15China = @"\d{15}";
        /// <summary>
        /// 手机号码正则表达式
        /// </summary>
        public const string PhoneNumber = @"(13|14|15|18|17)\d{9}";
        /// <summary>
        /// 日期正则表达式
        /// </summary>
        /// <param name="delimiter">分隔符</param>
        /// <returns></returns>
        public static string Date(string delimiter = "-/.")
        {
            return @"\d{4}[" + delimiter + @"](0?2[" + delimiter + @"]([1-2]\d|0?[1-9])|(1[02]|0?[13578])[" + delimiter + @"]([1-2]\d|3[0-1]|0?[1-9])|(11|0?[469])[" + delimiter + @"]([1-2]\d|30|0?[1-9]))";
        }
        /// <summary>
        /// 时间正则表达式
        /// 00:00:00.000-23:59:59.999
        /// </summary>
        public const string Time = @"(2[0-3]|1\d|0?\d)(:([1-5]\d|0?\d)){1,2}(.\d{1,3})?";
        /// <summary>
        /// 日期加时间正则表达式
        /// 1993/04/20 23:59:59.999
        /// 1993-04-20T00:00:00.000
        /// </summary>
        /// <param name="delimiter">分隔符</param>
        /// <returns></returns>
        public static string DateTime(string delimiter = "-/.")
        {
            return Date(delimiter) + @"[\sT]" + Time;
        }
        /// <summary>
        /// 字母正则表达式
        /// </summary>
        public const string Letter = @"[a-zA-Z]+";
        /// <summary>
        /// 小写字母正则表达式
        /// </summary>
        public const string LowerLetter = @"[a-z]+";
        /// <summary>
        /// 大写字母正则表达式
        /// </summary>
        public const string UpperLetter = @"[A-Z]+";
        /// <summary>
        /// 字母或数字正则表达式
        /// </summary>
        public const string LetterNumber = @"[a-zA-Z\d]+";
        /// <summary>
        /// 小写字母或数字正则表达式
        /// </summary>
        public const string LowerLetterNumber = @"[a-z\d]+";
        /// <summary>
        /// 大写字母或数字正则表达式
        /// </summary>
        public const string UpperLetterNumber = @"[A-Z\d]+";
        /// <summary>
        /// 中文正则表达式
        /// </summary>
        public const string Chinese = @"[\u4E00-\u9FA5]+";
        /// <summary>
        /// 中文、字母、数字正则表达式
        /// </summary>
        public const string ChineseLetterNumber = @"[\u4E00-\u9FA5a-zA-Z0-9]+";
        /// <summary>
        /// 日文正则表达式
        /// </summary>
        public const string Japanese = @"[\u0800-\u9fa5]+";
        /// <summary>
        /// 日文正则表达式
        /// </summary>
        public const string JapaneseLetterNumber = @"[\u0800-\u9fa5a-zA-Z0-9]+";
        /// <summary>
        /// 16进制字符串
        /// </summary>
        public const string HexNumber = @"[\dA-Fa-f]+";
        /// <summary>
        /// Guid
        /// </summary>
        public const string Guid = @"[a-zA-Z\d]{8}-([a-zA-Z\d]{4}-){3}[a-zA-Z\d]{12}";
        /// <summary>
        /// 16进制颜色
        /// </summary>
        public const string HexColor = @"#([\da-f]{6}|[\da-f]{3})";
    }
}
