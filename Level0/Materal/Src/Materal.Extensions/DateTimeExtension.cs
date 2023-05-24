using Materal.Abstractions;
using System.Globalization;

namespace System
{
    /// <summary>
    /// 日期时间扩展
    /// </summary>
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
        /// <summary>
        /// 获得时间戳
        /// 1970年1月1日 0点0分0秒以来的秒数
        /// </summary>
        /// <returns>时间戳</returns>
        public static long GetTimeStamp(this DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return timeSpan.Ticks;
        }
        /// <summary>
        /// 获得当天第一秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDayFirstSecond(this DateTime dateTime) => dateTime.Date;
        /// <summary>
        /// 获得当天第一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDayFirstMillisecond(this DateTime dateTime) => dateTime.Date;
        /// <summary>
        /// 获得当天最后一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDayLastMillisecond(this DateTime dateTime) => dateTime.Date.AddDays(1).AddMilliseconds(-1);
        /// <summary>
        /// 获得当天最后一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDayLastSecond(this DateTime dateTime) => dateTime.Date.AddDays(1).AddSeconds(-1);
        /// <summary>
        /// 获得当月第一秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstSecond(this DateTime dateTime) => new(dateTime.Year, dateTime.Month, 1);
        /// <summary>
        /// 获得当月第一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstMillisecond(this DateTime dateTime) => new(dateTime.Year, dateTime.Month, 1);
        /// <summary>
        /// 获得当月最后一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetMonthLastMillisecond(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddMilliseconds(-1);
        /// <summary>
        /// 获得当月最后一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetMonthLastSecond(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, 1).AddDays(1).AddSeconds(-1);
        /// <summary>
        /// 获得当年第一秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetYearFirstSecond(this DateTime dateTime) => new(dateTime.Year, 1, 1);
        /// <summary>
        /// 获得当年第一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetYearFirstMillisecond(this DateTime dateTime) => new(dateTime.Year, 1, 1);
        /// <summary>
        /// 获得当年最后一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetYearLastMillisecond(this DateTime dateTime) => new DateTime(dateTime.Year, 12, 31).AddDays(1).AddMilliseconds(-1);
        /// <summary>
        /// 获得当年最后一毫秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetYearLastSecond(this DateTime dateTime) => new DateTime(dateTime.Year, 12, 31).AddDays(1).AddSeconds(-1);
    }
}
