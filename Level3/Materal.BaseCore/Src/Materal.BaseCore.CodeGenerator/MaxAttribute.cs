using System.ComponentModel.DataAnnotations;

namespace Materal.BaseCore.CodeGenerator
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
        public decimal MaxNumber { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="maxNumber"></param>
        public MaxAttribute(int maxNumber)
        {
            MaxNumber = maxNumber;
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
                int intValue => intValue <= MaxNumber,
                long longValue => longValue <= MaxNumber,
                short shortValue => shortValue <= MaxNumber,
                uint uintValue => uintValue <= MaxNumber,
                ulong ulongValue => ulongValue <= MaxNumber,
                ushort ushortValue => ushortValue <= MaxNumber,
                float floatValue => floatValue <= Convert.ToSingle(MaxNumber),
                double doubleValue => doubleValue <= Convert.ToDouble(MaxNumber),
                decimal decimalValue => decimalValue <= MaxNumber,
                _ => true,
            };
        }
    }
}
