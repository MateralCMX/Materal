using System.Globalization;

namespace Materal.DateTimeHelper
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 转换为DateTimeOffset
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime) => dateTime.ToDateTimeOffset(DateTimeKind.Local);
        /// <summary>
        /// 转换为DateTimeOffset
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime, DateTimeKind dateTimeKind) => DateTime.SpecifyKind(dateTime, dateTimeKind);
        /// <summary>
        /// 合并时间
        /// </summary>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTimeOffset MergeDateTimeOffset(this Time time, Date date) => time.MergeDateTime(date).ToDateTimeOffset();
        /// <summary>
        /// 合并时间
        /// </summary>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime MergeDateTime(this Time time, Date date) => new(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        /// <summary>
        /// 合并时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTimeOffset MergeDateTimeOffset(this Date date, Time time) => date.MergeDateTime(time).ToDateTimeOffset();
        /// <summary>
        /// 合并时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime MergeDateTime(this Date date, Time time) => time.MergeDateTime(date);
        /// <summary>
        /// 获得日期
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static Date ToDate(this DateTimeOffset dateTimeOffset) => new(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
        /// <summary>
        /// 获得日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Date ToDate(this DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day);
        /// <summary>
        /// 获得时间
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static Time ToTime(this DateTimeOffset dateTimeOffset) => new(dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second);
        /// <summary>
        /// 获得时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Time ToTime(this DateTime dateTime) => new(dateTime.Hour, dateTime.Minute, dateTime.Second);
        /// <summary>
        /// 获得该日期是该年的第几季度
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetQuarterOfYear(this DateTime dateTime)
        {
            int result = dateTime.Month / 3;
            if (dateTime.Month % 3 != 0)
            {
                result++;
            }
            return result;
        }
        /// <summary>
        /// 获得该日期是该季度的第几月
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetMonthOfQuarter(this DateTime dateTime)
        {
            int quarter = dateTime.GetQuarterOfYear();
            int result = dateTime.Month - (quarter - 1) * 3;
            return result;
        }
        /// <summary>
        /// 获得该日期是该季度的第几周
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetWeekOfQuarter(this DateTime dateTime)
        {
            int quarter = dateTime.GetQuarterOfYear();
            DateTime startDate = new(dateTime.Year, (quarter - 1) * 3 + 1, 1);
            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }
            int result = 0;
            while (dateTime.Year > startDate.Year)
            {
                startDate = startDate.AddDays(7);
                result += 1;
            }
            int differenceDay = dateTime.DayOfYear - startDate.DayOfYear + 1;
            if (differenceDay < 0)
            {
                differenceDay = 0;
            }
            result += differenceDay / 7;
            if (differenceDay % 7 != 0)
            {
                result += 1;
            }
            return result;
        }
        /// <summary>
        /// 获得该日期是该季度的第几天
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetDayOfQuarter(this DateTime dateTime)
        {
            int quarter = dateTime.GetQuarterOfYear();
            DateTime startDate = new(dateTime.Year, (quarter - 1) * 3 + 1, 1);
            int result = dateTime.DayOfYear - startDate.DayOfYear + 1;
            return result;
        }
        /// <summary>
        /// 获得该日期是该年的第几周
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateTime dateTime)
        {
            GregorianCalendar gregoranCalendar = new();
            int result = gregoranCalendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return result;
        }
        /// <summary>
        /// 获得该日期是该月的第几周
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int GetWeekOfMonth(this DateTime dateTime)
        {
            DateTime monthFirstDay = new(dateTime.Year, dateTime.Month, 1);
            DayOfWeek monthFirstDayOfWeek = monthFirstDay.Date.DayOfWeek;
            int result;
            if (monthFirstDayOfWeek == DayOfWeek.Sunday)
            {
                result = (dateTime.Date.Day + 5) / 7 + 1;
            }
            else
            {
                result = (dateTime.Date.Day + (int)monthFirstDayOfWeek - 2) / 7 + 1;
            }
            return result;
        }
    }
}
