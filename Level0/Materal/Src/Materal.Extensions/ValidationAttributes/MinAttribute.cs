﻿namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// 最小
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MinAttribute : ValidationAttribute
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public object MinValue { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="minNumber"></param>
        public MinAttribute(int minNumber)
        {
            MinValue = Convert.ToDecimal(minNumber);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="minNumber"></param>
        public MinAttribute(double minNumber)
        {
            MinValue = Convert.ToDecimal(minNumber);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="minNumber"></param>
        public MinAttribute(decimal minNumber)
        {
            MinValue = minNumber;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="millisecond"></param>
        public MinAttribute(int year, int month, int day, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
        {
            MinValue = new DateTime(year, month, day, hour, minute, second, millisecond);
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            bool result = false;
            if(MinValue is decimal minNumberValue)
            {
                result = value switch
                {
                    int intValue => intValue >= minNumberValue,
                    long longValue => longValue >= minNumberValue,
                    short shortValue => shortValue >= minNumberValue,
                    uint uintValue => uintValue >= minNumberValue,
                    ulong ulongValue => ulongValue >= minNumberValue,
                    ushort ushortValue => ushortValue >= minNumberValue,
                    float floatValue => floatValue >= Convert.ToSingle(minNumberValue),
                    double doubleValue => doubleValue >= Convert.ToDouble(minNumberValue),
                    decimal decimalValue => decimalValue >= minNumberValue,
                    _ => false,
                };
            }
            else if(MinValue is DateTime minDateTimeValue && value != null && value is DateTime dateTimeValue)
            {
                result = dateTimeValue >= minDateTimeValue;
            }
            return result;
        }
    }
}
