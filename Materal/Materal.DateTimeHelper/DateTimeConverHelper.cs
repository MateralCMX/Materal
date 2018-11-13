namespace Materal.DateTimeHelper
{
    public static class DateTimeConverHelper
    {
        /// <summary>
        /// 转换为毫秒
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="dateTimeType"></param>
        /// <returns></returns>
        public static double ToMilliseconds(double timeValue, DateTimeTypeEnum dateTimeType)
        {
            var result = 0d;
            switch (dateTimeType)
            {
                case DateTimeTypeEnum.Year:
                    result = timeValue * 365 * 24 * 60 * 60 * 1000;
                    break;
                case DateTimeTypeEnum.Month:
                    result = timeValue * 30 * 24 * 60 * 60 * 1000;
                    break;
                case DateTimeTypeEnum.Day:
                    result = timeValue * 24 * 60 * 60 * 1000;
                    break;
                case DateTimeTypeEnum.Hour:
                    result = timeValue * 60 * 60 * 1000;
                    break;
                case DateTimeTypeEnum.Minute:
                    result = timeValue * 60 * 1000;
                    break;
                case DateTimeTypeEnum.Second:
                    result = timeValue * 1000;
                    break;
                case DateTimeTypeEnum.Millisecond:
                    result = timeValue;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 转换为秒
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="dateTimeType"></param>
        /// <returns></returns>
        public static double ToSeconds(double timeValue, DateTimeTypeEnum dateTimeType)
        {
            var result = 0d;
            switch (dateTimeType)
            {
                case DateTimeTypeEnum.Year:
                    result = timeValue * 365 * 24 * 60 * 60;
                    break;
                case DateTimeTypeEnum.Month:
                    result = timeValue * 30 * 24 * 60 * 60;
                    break;
                case DateTimeTypeEnum.Day:
                    result = timeValue * 24 * 60 * 60;
                    break;
                case DateTimeTypeEnum.Hour:
                    result = timeValue * 60 * 60;
                    break;
                case DateTimeTypeEnum.Minute:
                    result = timeValue * 60;
                    break;
                case DateTimeTypeEnum.Second:
                    result = timeValue;
                    break;
                case DateTimeTypeEnum.Millisecond:
                    result = timeValue / 1000;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 转换为分钟
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="dateTimeType"></param>
        /// <returns></returns>
        public static double ToMinutes(double timeValue, DateTimeTypeEnum dateTimeType)
        {
            var result = 0d;
            switch (dateTimeType)
            {
                case DateTimeTypeEnum.Year:
                    result = timeValue * 365 * 24 * 60;
                    break;
                case DateTimeTypeEnum.Month:
                    result = timeValue * 30 * 24 * 60;
                    break;
                case DateTimeTypeEnum.Day:
                    result = timeValue * 24 * 60;
                    break;
                case DateTimeTypeEnum.Hour:
                    result = timeValue * 60;
                    break;
                case DateTimeTypeEnum.Minute:
                    result = timeValue;
                    break;
                case DateTimeTypeEnum.Second:
                    result = timeValue / 60;
                    break;
                case DateTimeTypeEnum.Millisecond:
                    result = timeValue / 60 / 1000;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 转换为小时
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="dateTimeType"></param>
        /// <returns></returns>
        public static double ToHours(double timeValue, DateTimeTypeEnum dateTimeType)
        {
            var result = 0d;
            switch (dateTimeType)
            {
                case DateTimeTypeEnum.Year:
                    result = timeValue * 365 * 24;
                    break;
                case DateTimeTypeEnum.Month:
                    result = timeValue * 30 * 24;
                    break;
                case DateTimeTypeEnum.Day:
                    result = timeValue * 24;
                    break;
                case DateTimeTypeEnum.Hour:
                    result = timeValue;
                    break;
                case DateTimeTypeEnum.Minute:
                    result = timeValue / 60;
                    break;
                case DateTimeTypeEnum.Second:
                    result = timeValue / 60 / 60;
                    break;
                case DateTimeTypeEnum.Millisecond:
                    result = timeValue / 60 / 60 / 1000;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 转换为天
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="dateTimeType"></param>
        /// <returns></returns>
        public static double ToDay(double timeValue, DateTimeTypeEnum dateTimeType)
        {
            var result = 0d;
            switch (dateTimeType)
            {
                case DateTimeTypeEnum.Year:
                    result = timeValue * 365;
                    break;
                case DateTimeTypeEnum.Month:
                    result = timeValue * 30;
                    break;
                case DateTimeTypeEnum.Day:
                    result = timeValue;
                    break;
                case DateTimeTypeEnum.Hour:
                    result = timeValue / 24;
                    break;
                case DateTimeTypeEnum.Minute:
                    result = timeValue / 24 / 60;
                    break;
                case DateTimeTypeEnum.Second:
                    result = timeValue / 24 / 60 / 60;
                    break;
                case DateTimeTypeEnum.Millisecond:
                    result = timeValue / 24 / 60 / 60 / 1000;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 转换为月
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="dateTimeType"></param>
        /// <returns></returns>
        public static double ToMonth(double timeValue, DateTimeTypeEnum dateTimeType)
        {
            var result = 0d;
            switch (dateTimeType)
            {
                case DateTimeTypeEnum.Year:
                    result = timeValue * 12;
                    break;
                case DateTimeTypeEnum.Month:
                    result = timeValue;
                    break;
                case DateTimeTypeEnum.Day:
                    result = timeValue / 30;
                    break;
                case DateTimeTypeEnum.Hour:
                    result = timeValue / 30 / 24;
                    break;
                case DateTimeTypeEnum.Minute:
                    result = timeValue / 30 / 24 / 60;
                    break;
                case DateTimeTypeEnum.Second:
                    result = timeValue / 30 / 24 / 60 / 60;
                    break;
                case DateTimeTypeEnum.Millisecond:
                    result = timeValue / 30 / 24 / 60 / 60 / 1000;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 转换为年
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="dateTimeType"></param>
        /// <returns></returns>
        public static double ToYear(double timeValue, DateTimeTypeEnum dateTimeType)
        {
            var result = 0d;
            switch (dateTimeType)
            {
                case DateTimeTypeEnum.Year:
                    result = timeValue;
                    break;
                case DateTimeTypeEnum.Month:
                    result = timeValue / 12;
                    break;
                case DateTimeTypeEnum.Day:
                    result = timeValue / 365;
                    break;
                case DateTimeTypeEnum.Hour:
                    result = timeValue /365 / 30 / 24;
                    break;
                case DateTimeTypeEnum.Minute:
                    result = timeValue /365 / 30 / 24 / 60;
                    break;
                case DateTimeTypeEnum.Second:
                    result = timeValue /365 / 30 / 24 / 60 / 60;
                    break;
                case DateTimeTypeEnum.Millisecond:
                    result = timeValue /365 / 30 / 24 / 60 / 60 / 1000;
                    break;
            }
            return result;
        }
    }
}
