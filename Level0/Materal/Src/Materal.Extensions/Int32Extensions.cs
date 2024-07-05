namespace Materal.Extensions
{
    /// <summary>
    /// 整数扩展
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// 简体中文
        /// </summary>
        private readonly static IntConvertModel SimplifiedChineseModel = new()
        {
            Numbers = new()
            {
                [0] = "零",
                [1] = "一",
                [2] = "二",
                [3] = "三",
                [4] = "四",
                [5] = "五",
                [6] = "六",
                [7] = "七",
                [8] = "八",
                [9] = "九",
            },
            Units =
            [
                "",
                "万",
                "亿"
            ],
            Extend = new()
            {
                [0] = "千",
                [1] = "百",
                [2] = "十"
            }
        };
        /// <summary>
        /// 大写中文
        /// </summary>
        private readonly static IntConvertModel CapitalChineseModel = new()
        {
            Numbers = new()
            {
                [0] = "零",
                [1] = "壹",
                [2] = "贰",
                [3] = "叁",
                [4] = "肆",
                [5] = "伍",
                [6] = "陆",
                [7] = "柒",
                [8] = "捌",
                [9] = "玖",
            },
            Units =
            [
                "",
                "万",
                "亿"
            ],
            Extend = new()
            {
                [0] = "仟",
                [1] = "佰",
                [2] = "拾"
            }
        };
        /// <summary>
        /// 转换为简体中文
        /// </summary>
        /// <param name="inputNumber"></param>
        /// <returns></returns>
        public static string ConvertToSimplifiedChinese(this int inputNumber) => inputNumber.ConvertToChinese(SimplifiedChineseModel);
        /// <summary>
        /// 转换为大写中文
        /// </summary>
        /// <param name="inputNumber"></param>
        /// <returns></returns>
        public static string ConvertToCapitalChinese(this int inputNumber) => inputNumber.ConvertToChinese(CapitalChineseModel);
        /// <summary>
        /// 转换为中文
        /// </summary>
        /// <param name="inputNumber"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static string ConvertToChinese(this int inputNumber, IntConvertModel model)
        {
            int number = inputNumber;
            StringBuilder result = new();
            if (number < 0)
            {
                result.Append('负');
                number *= -1;
            }
            else if (number == 0) return "零";
            Dictionary<int, string> chineseNumberDictionary = model.Numbers;
            List<int> numbers = [];
            while (number > 0)
            {
                numbers.Add(number % 10);
                number /= 10;
            }
            while (numbers.Count % 4 != 0)
            {
                numbers.Add(0);
            }
            List<string> chineseNumbers = [];
            string nowChineseNumber = string.Empty;
            int temp = 0;
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                nowChineseNumber += chineseNumberDictionary[numbers[i]];
                temp++;
                if (temp % 4 == 0)
                {
                    chineseNumbers.Add(nowChineseNumber);
                    nowChineseNumber = string.Empty;
                }
            }
            temp = 0;
            for (int i = chineseNumbers.Count - 1; i >= 0; i--)
            {
                string chineseNumber = chineseNumbers[i];
                ConvertToChineseHandler(chineseNumber, temp++, model);
            }
            result.Append(string.Join("", chineseNumbers));
            if (result[^1] == '零')
            {
                result.Remove(result.Length - 1, 1);
            }
            return result.ToString();
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="dicIndex"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static string ConvertToChineseHandler(string inputString, int dicIndex, IntConvertModel model)
        {
            Dictionary<int, string> extend = new()
            {
                [0] = model.Extend[0],
                [1] = model.Extend[1],
                [2] = model.Extend[2],
                [3] = model.Units[dicIndex]
            };
            StringBuilder result = new();
            for (int i = 0; i < inputString.Length; i++)
            {
                string tempString = inputString[i] + extend[i];
                if (tempString[0] == '零')
                {
                    if (result.Length > 0 && result[^1] == '零') continue;
                    if (result.Length == 0) continue;
                    tempString = inputString[i].ToString();
                }
                result.Append(tempString);
            }
            if (result.Length > 0 && result[^1] == '零')
            {
                result.Remove(result.Length - 1, 1);
                result.Append(extend[inputString.Length - 1] + "零");
            }
            return result.ToString();
        }
        /// <summary>
        /// 转换为二进制字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetBinaryString(this int buffer) => Convert.ToString(buffer, 2);
    }
}
