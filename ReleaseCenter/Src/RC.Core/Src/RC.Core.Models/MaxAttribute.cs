using System.ComponentModel.DataAnnotations;

namespace RC.Core.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MaxAttribute : ValidationAttribute
    {
        public decimal MaxNumber { get; }

        public MaxAttribute(int maxNumber)
        {
            MaxNumber = maxNumber;
        }

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
