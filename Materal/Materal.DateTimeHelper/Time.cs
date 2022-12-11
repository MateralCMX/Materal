namespace Materal.DateTimeHelper
{
    [Serializable]
    public class Time
    {
        private int _hour;
        private int _minute;
        private int _second;

        /// <summary>
        /// 小时
        /// </summary>
        public int Hour
        {
            get => _hour;
            set
            {
                if (value > 23) _hour = 23;
                else if (value < 0) _hour = 0;
                else _hour = value;
            }
        }
        /// <summary>
        /// 分钟
        /// </summary>
        public int Minute
        {
            get => _minute;
            set
            {
                if (value > 59) _minute = 59;
                else if (value < 0) _minute = 0;
                else _minute = value;
            }
        }
        /// <summary>
        /// 秒
        /// </summary>
        public int Second
        {
            get => _second; set
            {
                if (value > 59) _second = 59;
                else if (value < 0) _second = 0;
                else _second = value;
            }
        }
        public Time(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }
        public Time()
        {

        }
        public override string ToString()
        {
            return $"{Hour:00}:{Minute:00}:{Second:00}";
        }
    }
}
