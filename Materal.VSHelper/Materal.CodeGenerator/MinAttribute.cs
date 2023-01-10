using System.ComponentModel.DataAnnotations;

namespace Materal.CodeGenerator
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
        public decimal MinNumber { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="minNumber"></param>
        public MinAttribute(int minNumber)
        {
            MinNumber = minNumber;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            return value switch
            {
                int intValue => intValue >= MinNumber,
                long longValue => longValue >= MinNumber,
                short shortValue => shortValue >= MinNumber,
                uint uintValue => uintValue >= MinNumber,
                ulong ulongValue => ulongValue >= MinNumber,
                ushort ushortValue => ushortValue >= MinNumber,
                float floatValue => floatValue >= Convert.ToSingle(MinNumber),
                double doubleValue => doubleValue >= Convert.ToDouble(MinNumber),
                decimal decimalValue => decimalValue >= MinNumber,
                _ => true,
            };
        }
    }
}
