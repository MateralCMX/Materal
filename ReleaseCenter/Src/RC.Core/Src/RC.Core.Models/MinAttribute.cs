using System.ComponentModel.DataAnnotations;

namespace RC.Core.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MinAttribute : ValidationAttribute
    {
        public decimal MinNumber { get; }

        public MinAttribute(int minNumber)
        {
            MinNumber = minNumber;
        }

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
