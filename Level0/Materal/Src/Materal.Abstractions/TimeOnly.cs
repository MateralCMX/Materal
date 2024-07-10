#if NETSTANDARD
namespace Materal.Abstractions
{
    /// <summary>
    /// 时间
    /// </summary>
    [Serializable]
    public struct TimeOnly
    {
        private int _hour;
        private int _minute;
        private int _second;
        /// <summary>
        /// 小时
        /// </summary>
        public int Hour
        {
            readonly get => _hour;
            set
            {
                _hour = value;
                ChangeTimeByCorrect();
            }
        }
        /// <summary>
        /// 分钟
        /// </summary>
        public int Minute
        {
            readonly get => _minute;
            set
            {
                _minute = value;
                ChangeTimeByCorrect();
            }
        }
        /// <summary>
        /// 秒
        /// </summary>
        public int Second
        {
            readonly get => _second; set
            {
                _second = value;
                ChangeTimeByCorrect();
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public TimeOnly(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dateTime"></param>
        public TimeOnly(DateTime dateTime) : this(dateTime.Hour, dateTime.Minute, dateTime.Second)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public TimeOnly() : this(DateTime.Now)
        {
        }
        /// <summary>
        /// 添加小时
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        public TimeOnly AddHours(int hour)
        {
            int trueHour = Hour + hour;
            int trueMinute = Minute;
            int trueSecond = Second;
            ChangeTimeByCumulativeCorrect(ref trueHour, ref trueMinute, ref trueSecond);
            Hour = trueHour;
            Minute = trueMinute;
            Second = trueSecond;
            return this;
        }
        /// <summary>
        /// 添加分钟
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public TimeOnly AddMinutes(int minute)
        {
            int trueHour = Hour;
            int trueMinute = Minute + minute;
            int trueSecond = Second;
            ChangeTimeByCumulativeCorrect(ref trueHour, ref trueMinute, ref trueSecond);
            Hour = trueHour;
            Minute = trueMinute;
            Second = trueSecond;
            return this;
        }
        /// <summary>
        /// 添加秒
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public TimeOnly AddSeconds(int second)
        {
            int trueHour = Hour;
            int trueMinute = Minute;
            int trueSecond = Second + second;
            ChangeTimeByCumulativeCorrect(ref trueHour, ref trueMinute, ref trueSecond);
            Hour = trueHour;
            Minute = trueMinute;
            Second = trueSecond;
            return this;
        }
        /// <summary>
        /// 转换为DateTime
        /// </summary>
        /// <returns></returns>
        public readonly DateTime ToDateTime()
        {
            DateTime nowDate = DateTime.Now;
            return ToDateTime(nowDate.Year, nowDate.Month, nowDate.Day);
        }
        /// <summary>
        /// 转换为DateTime
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public readonly DateTime ToDateTime(DateOnly day) => ToDateTime(day.Year, day.Month, day.Day);
        /// <summary>
        /// 转换为DateTime
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public readonly DateTime ToDateTime(int year, int month, int day) => new(year, month, day, Hour, Minute, Second);
        /// <summary>
        /// 转换为DateTimeOffset
        /// </summary>
        /// <returns></returns>
        public readonly DateTimeOffset ToDateTimeOffset() => new(ToDateTime());
        /// <summary>
        /// 转换为DateTimeOffset
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public readonly DateTimeOffset ToDateTimeOffset(DateOnly day) => ToDateTimeOffset(day.Year, day.Month, day.Day);
        /// <summary>
        /// 转换为DateTimeOffset
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public readonly DateTimeOffset ToDateTimeOffset(int year, int month, int day) => new(ToDateTime(year, month, day));
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override readonly string ToString() => $"{Hour:00}:{Minute:00}:{Second:00}";
        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(TimeOnly a, TimeOnly b) => a.Equals(b);
        /// <summary>
        /// 不等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(TimeOnly a, TimeOnly b) => !a.Equals(b);
        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(TimeOnly a, TimeOnly b)
        {
            if (a.Hour > b.Hour) return true;
            else if (a.Hour < b.Hour) return false;
            if (a.Minute > b.Minute) return true;
            else if (a.Minute < b.Minute) return false;
            if (a.Second > b.Second) return true;
            else if (a.Second < b.Second) return false;
            return false;
        }
        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >=(TimeOnly a, TimeOnly b)
        {
            if (a == b) return true;
            return a > b;
        }
        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(TimeOnly a, TimeOnly b)
        {
            if (a.Hour < b.Hour) return true;
            else if (a.Hour > b.Hour) return false;
            if (a.Minute < b.Minute) return true;
            else if (a.Minute > b.Minute) return false;
            if (a.Second < b.Second) return true;
            else if (a.Second > b.Second) return false;
            return false;
        }
        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <=(TimeOnly a, TimeOnly b)
        {
            if (a == b) return true;
            return a < b;
        }
        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override readonly bool Equals(object? obj)
        {
            if (obj is null || obj is not TimeOnly time) return false;
            return Hour == time.Hour && Minute == time.Minute && Second == time.Second;
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns></returns>
        public override readonly int GetHashCode() => base.GetHashCode();
        #region 私有方法
        /// <summary>
        /// 修改日期到正确
        /// </summary>
        private void ChangeTimeByCorrect()
        {
            while (!ChangeTime()) { }
        }
        /// <summary>
        /// 更改日期
        /// </summary>
        private bool ChangeTime()
        {
            bool result = true;
            #region 时
            if (_hour < 0)
            {
                _hour = 0;
                result = false;
            }
            if (_hour > 23)
            {
                _hour = 23;
                result = false;
            }
            #endregion
            #region 分
            if (_minute < 0)
            {
                _minute = 0;
                result = false;
            }
            if (_minute > 59)
            {
                _minute = 59;
                result = false;
            }
            #endregion
            #region 秒
            if (_second < 0)
            {
                _second = 0;
                result = false;
            }
            if (_second > 59)
            {
                _second = 59;
                result = false;
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 修改日期到累加正确
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        private static void ChangeTimeByCumulativeCorrect(ref int hour, ref int minute, ref int second)
        {
            while (!ChangeTimeByCumulative(ref hour, ref minute, ref second)) { }
        }
        /// <summary>
        /// 更改日期
        /// </summary>
        private static bool ChangeTimeByCumulative(ref int hour, ref int minute, ref int second)
        {
            bool result = true;
            #region 时
            if (hour < 0)
            {
                hour = 0;
                result = false;
            }
            while (hour > 23)
            {
                hour -= 24;
                result = false;
            }
            #endregion
            #region 分
            if (minute < 0)
            {
                hour -= 1;
                minute += 60;
                result = false;
            }
            while (minute > 59)
            {
                minute -= 60;
                hour += 1;
                result = false;
            }
            #endregion
            #region 秒
            if (second < 0)
            {
                minute -= 1;
                second += 60;
                result = false;
            }
            while (second > 59)
            {
                second -= 60;
                minute += 1;
                result = false;
            }
            #endregion
            return result;
        }
        #endregion
    }
}
#endif