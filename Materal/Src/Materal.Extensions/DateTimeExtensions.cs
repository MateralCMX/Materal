using System.Globalization;

namespace Materal.Extensions
{
    /// <summary>
    /// 日期时间扩展
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 添加秒
        /// </summary>
        /// <param name="time"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static TimeOnly AddSeconds(this TimeOnly time, int seconds) => time.Add(new TimeSpan(0, 0, seconds));
        /// <summary>
        /// 转换为时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this DateTimeOffset dateTime, DateTimeKind dateTimeKind = DateTimeKind.Local)
            => DateTime.SpecifyKind(dateTime.DateTime, dateTimeKind);
        /// <summary>
        /// 转换为时间偏移
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime, DateTimeKind? dateTimeKind = null) => new(DateTime.SpecifyKind(dateTime, dateTimeKind ?? dateTime.Kind));
        /// <summary>
        /// 转换为时间
        /// </summary>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this TimeOnly time, DateOnly date, DateTimeKind dateTimeKind = DateTimeKind.Local)
            => new(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, dateTimeKind);
        /// <summary>
        /// 转换为时间偏移
        /// </summary>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this TimeOnly time, DateOnly date, DateTimeKind dateTimeKind = DateTimeKind.Local) => time.ToDateTime(date, dateTimeKind).ToDateTimeOffset();
        /// <summary>
        /// 转换为时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this DateOnly date, DateTimeKind dateTimeKind = DateTimeKind.Local) => date.ToDateTime(new TimeOnly(0, 0, 0), dateTimeKind);
        /// <summary>
        /// 转换为时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this DateOnly date, TimeOnly time, DateTimeKind dateTimeKind = DateTimeKind.Local) => time.ToDateTime(date, dateTimeKind);
        /// <summary>
        /// 转换为时间偏移
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this DateOnly date, DateTimeKind dateTimeKind = DateTimeKind.Local) => date.ToDateTimeOffset(new TimeOnly(0, 0, 0), dateTimeKind);
        /// <summary>
        /// 转换为时间偏移
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this DateOnly date, TimeOnly time, DateTimeKind dateTimeKind = DateTimeKind.Local) => date.ToDateTime(time, dateTimeKind).ToDateTimeOffset();
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateOnly ToDateOnly(this DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day);
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static DateOnly ToDateOnly(this DateTimeOffset dateTimeOffset) => new(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day);
        /// <summary>
        /// 转换为时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static TimeOnly ToTimeOnly(this DateTime dateTime) => new(dateTime.Hour, dateTime.Minute, dateTime.Second);
        /// <summary>
        /// 转换为时间
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static TimeOnly ToTimeOnly(this DateTimeOffset dateTimeOffset) => new(dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second);
        /// <summary>
        /// 获得该日期是该年的第几季度
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetQuarterOfYear(this DateOnly date) => date.ToDateTime().GetQuarterOfYear();
        /// <summary>
        /// 获得该日期是该季度的第几月
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetMonthOfQuarter(this DateOnly date) => date.ToDateTime().GetMonthOfQuarter();
        /// <summary>
        /// 获得该日期是该季度的第几周
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetWeekOfQuarter(this DateOnly date) => date.ToDateTime().GetWeekOfQuarter();
        /// <summary>
        /// 获得该日期是该季度的第几天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetDayOfQuarter(this DateOnly date) => date.ToDateTime().GetDayOfQuarter();
        /// <summary>
        /// 获得该日期是该年的第几周
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateOnly date) => date.ToDateTime().GetWeekOfYear();
        /// <summary>
        /// 获得该日期是该月的第几周
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetWeekOfMonth(this DateOnly date) => date.ToDateTime().GetWeekOfMonth();
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
        /// <param name="firstDay"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateTime dateTime, DayOfWeek firstDay = DayOfWeek.Monday)
        {
            GregorianCalendar gregoranCalendar = new();
            int result = gregoranCalendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, firstDay);
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
            DayOfWeek monthFirstDayOfWeek = monthFirstDay.DayOfWeek;
            int result;
            if (monthFirstDayOfWeek == DayOfWeek.Sunday)
            {
                result = (dateTime.Day + 5) / 7 + 1;
            }
            else
            {
                result = (dateTime.Day + (int)monthFirstDayOfWeek - 2) / 7 + 1;
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
            TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, dateTime.Kind);
            return timeSpan.Ticks;
        }
        /// <summary>
        /// 获得时间戳
        /// 1970年1月1日 0点0分0秒以来的秒数
        /// </summary>
        /// <returns>时间戳</returns>
        public static long GetTimeStamp(this DateTimeOffset dateTime) => dateTime.ToDateTime().GetTimeStamp();
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
