using AspectCore.DynamicProxy;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Materal.BaseCore.Services
{
    /// <summary>
    /// 数据验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DataValidationAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            InvokeBefore(context);
            await context.Invoke(next);
        }
        /// <summary>
        /// 执行之前
        /// </summary>
        private static void InvokeBefore(AspectContext context)
        {
            string methodName = context.ServiceMethod.Name;
            if (methodName.StartsWith("get_") || methodName.StartsWith("set_")) return;
            ParameterInfo[] parameterInfos = context.ImplementationMethod.GetParameters();
            for (var i = 0; i < parameterInfos.Length; i++)
            {
                object value = context.Parameters[i];
                value.Validation();
                IEnumerable<ValidationAttribute> validations = parameterInfos[i].GetCustomAttributes<ValidationAttribute>();
                if (!validations.Any()) continue;
                value.Validation(validations, validationAttribute => GetValidationException(validationAttribute, context.Implementation.GetType().Name, parameterInfos[i], value));
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
        private static ValidationException GetValidationException(ValidationAttribute validationAttribute, string typeName, ParameterInfo parameterInfo, object value)
        {
            string errorMessage = $"{typeName}.{parameterInfo.Member.Name}.{parameterInfo.Name}验证失败";
            if(validationAttribute.ErrorMessage is not null && !string.IsNullOrWhiteSpace(validationAttribute.ErrorMessage))
            {
                errorMessage += $":{validationAttribute.ErrorMessage}";
            }
            return new ValidationException(errorMessage, validationAttribute, value);
        }
    }
}
