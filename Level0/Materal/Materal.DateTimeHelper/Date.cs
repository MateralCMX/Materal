namespace Materal.DateTimeHelper
{
    [Serializable]
    public class Date
    {
        /// <summary>
        /// 有31号的月份
        /// </summary>
        public static readonly int[] Has31DayMonths = new[] { 1, 3, 5, 7, 8, 10, 12 };
        private int _year = 1;
        private int _month = 1;
        private int _day = 1;

        /// <summary>
        /// 年
        /// </summary>
        public int Year
        {
            get => _year;
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
            get => _month;
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
            get => _day; set
            {
                _day = value;
                ChangeDateByCorrect();
            }
        }
        /// <summary>
        /// 是闰年
        /// </summary>
        public bool IsLeapYear => Year % 4 == 0;
        /// <summary>
        /// 最大天数
        /// </summary>
        public int MaxDay
        {
            get
            {
                if (Month == 2)
                {
                    return IsLeapYear ? 29 : 28;
                }
                else if (Has31DayMonths.Contains(Month))
                {
                    return 31;
                }
                return 30;
            }
        }
        public Date(int year, int month, int day)
        {
            _year = year;
            _month = month;
            _day = day;
            ChangeDateByCorrect();
        }
        public Date()
        {
        }
        public Date(DateTime dateTime) : this(dateTime.Year, dateTime.Month, dateTime.Day)
        {
        }
        public override string ToString()
        {
            return $"{Year:0000}-{Month:00}-{Day:00}";
        }
        /// <summary>
        /// 添加年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public Date AddYear(int year)
        {
            Year += year;
            return this;
        }
        /// <summary>
        /// 添加月
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Date AddMonth(int month)
        {
            Month += month;
            return this;
        }
        /// <summary>
        /// 添加天
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public Date AddDay(int day)
        {
            Day += day;
            return this;
        }
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
        #region 私有方法
        /// <summary>
        /// 修改日期到正确
        /// </summary>
        private void ChangeDateByCorrect()
        {
            while (!ChangeDate()) { }
        }
        /// <summary>
        /// 校验日期
        /// </summary>
        private bool ChangeDate()
        {
            bool result = true;
            #region 年
            if (_year < 0)
            {
                _year = 1;
                result = false;
            }
            #endregion
            #region 月
            if (_month < 0)
            {
                _year -= 1;
                _month += 12;
                result = false;
            }
            while (_month > 12)
            {
                _month -= 12;
                _year += 1;
                result = false;
            }
            #endregion
            #region 日
            if (_day < 0)
            {
                _month -= 1;
                _day += MaxDay;
                result = false;
            }
            while (_day > MaxDay)
            {
                _day -= MaxDay;
                _month += 1;
                result = false;
            }
            #endregion
            return result;
        }
        #endregion
    }
}
