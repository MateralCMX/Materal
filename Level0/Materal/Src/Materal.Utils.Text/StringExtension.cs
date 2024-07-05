using Microsoft.International.Converters.PinYinConverter;

namespace Materal.Utils.Text
{
    /// <summary>
    /// 字符串扩展
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
            object[] chineseChars = new object[inputString.Length];
            for (int i = 0; i < inputString.Length; i++)
            {
                char inputChar = inputString[i];
                if (!inputChar.ToString().IsChinese())
                {
                    chineseChars[i] = inputChar;
                    continue;
                }
                chineseChars[i] = new ChineseChar(inputChar);
            }
            IEnumerable<string> result = GetPinYin("", 0, [.. chineseChars], mode);
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
        private static List<string> GetPinYin(string nowPinyin, int index, IReadOnlyList<object> chineseChars, PinYinMode mode)
        {
            List<string> inputPinYin = [];
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
                        for (int i = 0; i < chinese.PinyinCount; i++)
                        {
                            string pinyin = chinese.Pinyins[i];
                            switch (mode)
                            {
                                case PinYinMode.Full:
                                    pinyin = pinyin.ToLower().FirstUpper();
                                    break;
                                case PinYinMode.NoTone:
                                    pinyin = pinyin.ToLower().FirstUpper()[..(pinyin.Length - 1)];
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
    }
}
