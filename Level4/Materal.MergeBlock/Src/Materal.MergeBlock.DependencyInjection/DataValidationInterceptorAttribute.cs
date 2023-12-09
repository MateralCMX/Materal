using Materal.Abstractions;
using Materal.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Materal.MergeBlock.DependencyInjection
{
    /// <summary>
    /// 数据验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DataValidationInterceptorAttribute : InterceptorAttribute
    {
        public override void Befor(InterceptorContext context)
        {
            ParameterInfo[] parameterInfos = context.MethodInfo.GetParameters();
            ParameterInfo[] interfaceParameterInfos = context.InterfaceMethodInfo.GetParameters();
            if (parameterInfos.Length != interfaceParameterInfos.Length) throw new MateralException("参数数量不一致");
            for (var i = 0; i < parameterInfos.Length; i++)
            {
                object? value = context.Parameters[i];
                value?.Validation();
                List<ValidationAttribute> validations = parameterInfos[i].GetCustomAttributes<ValidationAttribute>().ToList();
                validations.AddRange(interfaceParameterInfos[i].GetCustomAttributes<ValidationAttribute>());
                if (!validations.Any()) continue;
                value.Validation(validations, validationAttribute => GetValidationException(validationAttribute, context.MethodInfo.GetType().Name, parameterInfos[i], value));
            }
        }
        /// <summary>
        /// 获得验证异常
        /// </summary>
        /// <param name="validationAttribute"></param>
        /// <param name="typeName"></param>
        /// <param name="parameterInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static ValidationException GetValidationException(ValidationAttribute validationAttribute, string typeName, ParameterInfo parameterInfo, object? value)
        {
            string errorMessage = $"{typeName}.{parameterInfo.Member.Name}.{parameterInfo.Name}验证失败";
            if (validationAttribute.ErrorMessage is not null && !string.IsNullOrWhiteSpace(validationAttribute.ErrorMessage))
            {
                errorMessage += $":{validationAttribute.ErrorMessage}";
            }
            return new ValidationException(errorMessage, validationAttribute, value);
        }
    }
}
