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
    }
}
