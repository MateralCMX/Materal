namespace Materal.BaseCore.Common.Utils
{
    public static class ExcelHelperExtensions
    {
        /// <summary>
        /// Excel文本转换为时间
        /// </summary>
        /// <param name="excelValue"></param>
        /// <returns></returns>
        public static DateTime ExcelValueToDate(this string excelValue)
        {
            string[] dateValues = excelValue.Split("-");
            if (dateValues.Length != 3) throw new InvalidOperationException("格式错误");
            dateValues[1] = dateValues[1].Substring(0, dateValues[1].Length - 1);
            int year = Convert.ToInt32(dateValues[2]);
            int month = Convert.ToInt32(dateValues[1]);
            int day = Convert.ToInt32(dateValues[0]);
            DateTime result = new DateTime(year, month, day);
            return result;
        }
    }
}
