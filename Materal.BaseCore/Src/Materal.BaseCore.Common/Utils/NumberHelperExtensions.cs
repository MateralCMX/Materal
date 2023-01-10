namespace Materal.BaseCore.Common.Utils
{
    /// <summary>
    /// 数字帮助扩展
    /// </summary>
    public static class NumberHelperExtensions
    {
        /// <summary>
        /// 简体中文
        /// </summary>
        public static NumberHelperModel SimplifiedChineseModel = new()
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
            Units = new()
            {
                "",
                "万",
                "亿"
            },
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
        public static NumberHelperModel CapitalChineseModel = new()
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
            Units = new()
            {
                "",
                "万",
                "亿"
            },
            Extend = new()
            {
                [0] = "仟",
                [1] = "佰",
                [2] = "拾"
            }
        };
        public static string ConvertToChinese(this uint inputNumber, NumberHelperModel? model = null)
        {
            model ??= SimplifiedChineseModel;
            Dictionary<uint, string> chineseNumberDictionary = model.Numbers;
            uint number = inputNumber;
            List<uint> numbers = new();
            while (number > 0)
            {
                numbers.Add(number % 10);
                number /= 10;
            }
            while (numbers.Count % 4 != 0)
            {
                numbers.Add(0);
            }
            List<string> chineseNumbers = new();
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
                var chineseNumber = chineseNumbers[i];
                Handler(chineseNumber, temp++, model);
            }
            var result = string.Join("", chineseNumbers);
            if (result.EndsWith("零"))
            {
                result = result[..^1];
            }
            return result;
        }
        private static string Handler(string inputString, int dicIndex, NumberHelperModel model)
        {
            Dictionary<int, string> extend = new()
            {
                [0] = model.Extend[0],
                [1] = model.Extend[1],
                [2] = model.Extend[2],
                [3] = model.Units[dicIndex]
            };
            string result = string.Empty;
            for (int i = 0; i < inputString.Length; i++)
            {
                string tempString = inputString[i] + extend[i];
                if (tempString.StartsWith("零"))
                {
                    if (result.EndsWith("零")) continue;
                    if (string.IsNullOrWhiteSpace(result)) continue;
                    tempString = inputString[i].ToString();
                }
                result += tempString;
            }
            if (result.EndsWith("零"))
            {
                result = result[..^1] + extend[inputString.Length - 1] + "零";
            }
            return result;
        }
    }
}
