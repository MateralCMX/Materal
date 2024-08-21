namespace Materal.Extensions.ValidationAttributes
{
    /// <summary>
    /// 最大
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MaxAttribute : ValidationAttribute
    {
        /// <summary>
        /// 最大值
        /// </summary>
        public object MaxValue { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="minNumber"></param>
        public MaxAttribute(int minNumber)
        {
            MaxValue = Convert.ToDecimal(minNumber);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="minNumber"></param>
        public MaxAttribute(double minNumber)
        {
            MaxValue = Convert.ToDecimal(minNumber);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="minNumber"></param>
        public MaxAttribute(decimal minNumber)
        {
            MaxValue = minNumber;
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
        public MaxAttribute(int year, int month, int day, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
        {
            MaxValue = new DateTime(year, month, day, hour, minute, second, millisecond);
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            bool result = false;
            if (MaxValue is decimal minNumberValue)
            {
                result = value switch
                {
                    int intValue => intValue <= minNumberValue,
                    long longValue => longValue <= minNumberValue,
                    short shortValue => shortValue <= minNumberValue,
                    uint uintValue => uintValue <= minNumberValue,
                    ulong ulongValue => ulongValue <= minNumberValue,
                    ushort ushortValue => ushortValue <= minNumberValue,
                    float floatValue => floatValue <= Convert.ToSingle(minNumberValue),
                    double doubleValue => doubleValue <= Convert.ToDouble(minNumberValue),
                    decimal decimalValue => decimalValue <= minNumberValue,
                    _ => false,
                };
            }
            else if (MaxValue is DateTime minDateTimeValue && value is not null && value is DateTime dateTimeValue)
            {
                result = dateTimeValue <= minDateTimeValue;
            }
            return result;
        }
    }
}
