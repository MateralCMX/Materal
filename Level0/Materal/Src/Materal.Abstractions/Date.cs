namespace Materal.Abstractions
{
    /// <summary>
    /// 日期
    /// </summary>
    [Serializable]
    public struct Date
    {
        /// <summary>
        /// 有31号的月份
        /// </summary>
        public static readonly int[] Has31DayMonths = [1, 3, 5, 7, 8, 10, 12];
        private int _year = 1;
        private int _month = 1;
        private int _day = 1;
        /// <summary>
        /// 年
        /// </summary>
        public int Year
        {
            readonly get => _year;
            set
            {
                _year = value;
                ChangeDateByCorrect();
            }
        }
        /// <summary>
        /// 月
        /// </summary>
        public int Month
        {
            readonly get => _month;
            set
            {
                _month = value;
                ChangeDateByCorrect();
            }
        }
        /// <summary>
        /// 日
        /// </summary>
        public int Day
        {
            readonly get => _day;
            set
            {
                _day = value;
                ChangeDateByCorrect();
            }
        }
        /// <summary>
        /// 是闰年
        /// </summary>
        public readonly bool IsLeapYear => CanLeapYear(Year);
        /// <summary>
        /// 最大天数
        /// </summary>
        public readonly int MaxDay => GetMaxDay(Year, Month);
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public Date(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
            ChangeDateByCorrect();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public Date() : this(DateTime.Now)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dateTime"></param>
        public Date(DateTime dateTime) : this(dateTime.Year, dateTime.Month, dateTime.Day)
        {
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override readonly string ToString() => $"{Year:0000}-{Month:00}-{Day:00}";
        /// <summary>
        /// 添加年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public Date AddYear(int year)
        {
            int trueYear = Year + year;
            int trueMonth = Month;
            int trueDay = Day;
            ChangeDateByCumulativeCorrect(ref trueYear, ref trueMonth, ref trueDay);
            Year = trueYear;
            Month = trueMonth;
            Day = trueDay;
            return this;
        }
        /// <summary>
        /// 添加月
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Date AddMonth(int month)
        {
            int trueYear = Year;
            int trueMonth = Month + month;
            int trueDay = Day;
            ChangeDateByCumulativeCorrect(ref trueYear, ref trueMonth, ref trueDay);
            Year = trueYear;
            Month = trueMonth;
            Day = trueDay;
            return this;
        }
        /// <summary>
        /// 添加天
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public Date AddDay(int day)
        {
            int trueYear = Year;
            int trueMonth = Month;
            int trueDay = Day + day;
            ChangeDateByCumulativeCorrect(ref trueYear, ref trueMonth, ref trueDay);
            Year = trueYear;
            Month = trueMonth;
            Day = trueDay;
            return this;
        }
        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Date a, Date b) => a.Equals(b);
        /// <summary>
        /// 不等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Date a, Date b) => !a.Equals(b);
        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Date a, Date b)
        {
            if (a.Year > b.Year) return true;
            else if (a.Year < b.Year) return false;
            if (a.Month > b.Month) return true;
            else if (a.Month < b.Month) return false;
            if (a.Day > b.Day) return true;
            else if (a.Day < b.Day) return false;
            return false;
        }
        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >=(Date a, Date b)
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
        public static bool operator <(Date a, Date b)
        {
            if (a.Year < b.Year) return true;
            else if (a.Year > b.Year) return false;
            if (a.Month < b.Month) return true;
            else if (a.Month > b.Month) return false;
            if (a.Day < b.Day) return true;
            else if (a.Day > b.Day) return false;
            return false;
        }
        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <=(Date a, Date b)
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
            if (obj is null || obj is not Date date) return false;
            return Year == date.Year && Month == date.Month && Day == date.Day;
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
        private void ChangeDateByCorrect()
        {
            while (!ChangeDate()) { }
        }
        /// <summary>
        /// 更改日期
        /// </summary>
        private bool ChangeDate()
        {
            bool result = true;
            #region 年
            if (_year < 0)
            {
                _year = DateTime.MinValue.Year;
                result = false;
            }
            #endregion
            #region 月
            if (_month <= 0)
            {
                _month = 1;
                result = false;
            }
            if (_month > 12)
            {
                _month = 12;
                result = false;
            }
            #endregion
            #region 日
            if (_day <= 0)
            {
                _day = 1;
                result = false;
            }
            if (_day > MaxDay)
            {
                _day = MaxDay;
                result = false;
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 修改日期到累加正确
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        private static void ChangeDateByCumulativeCorrect(ref int year, ref int month, ref int day)
        {
            while (!ChangeDateByCumulative(ref year, ref month, ref day)) { }
        }
        /// <summary>
        /// 更改日期
        /// </summary>
        private static bool ChangeDateByCumulative(ref int year, ref int month, ref int day)
        {
            bool result = true;
            #region 年
            if (year < 0)
            {
                year = 1;
                result = false;
            }
            #endregion
            #region 月
            if (month < 0)
            {
                year -= 1;
                month += 12;
                result = false;
            }
            while (month > 12)
            {
                month -= 12;
                year += 1;
                result = false;
            }
            #endregion
            #region 日
            int maxDay = GetMaxDay(year, month);
            if (day < 0)
            {
                month -= 1;
                day += maxDay;
                result = false;
            }
            while (day > maxDay)
            {
                day -= maxDay;
                month += 1;
                result = false;
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 是否是闰年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static bool CanLeapYear(int year) => year % 4 == 0;
        /// <summary>
        /// 获取最大天数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private static int GetMaxDay(int year, int month)
        {
            if (month == 2)
            {
                return CanLeapYear(year) ? 29 : 28;
            }
            else if (Has31DayMonths.Contains(month))
            {
                return 31;
            }
            return 30;
        }
        #endregion
    }
}
