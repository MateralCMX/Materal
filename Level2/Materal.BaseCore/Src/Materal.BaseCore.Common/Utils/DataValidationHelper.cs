using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Materal.BaseCore.Common.Utils
{
    public static class DataValidationHelper
    {
        /// <summary>
        /// 执行之前
        /// </summary>
        public static void ValidModel(object model)
        {
            Type modelType = model.GetType();
            PropertyInfo[] properties = modelType.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                object? propertyValue = propertyInfo.GetValue(model);
                if (propertyValue == null)
                {
                    ValidNull(propertyInfo);
                }
                Type parameterType = propertyInfo.PropertyType;
                if (propertyValue == null) return;
                if (parameterType.IsClass)
                {
                    if (parameterType == typeof(string))
                    {
                        ValidValue(propertyInfo, propertyValue);
                    }
                    else
                    {
                        switch (propertyValue)
                        {
                            case ICollection contextCollection:
                                ValidValue(propertyInfo, contextCollection);
                                break;
                            default:
                                ValidClass(propertyValue, parameterType);
                                break;
                        }
                    }
                }
                else
                {
                    ValidValue(propertyInfo, propertyValue);
                }
            }
        }
        /// <summary>
        /// 验证空
        /// </summary>
        /// <param name="parameterInfo"></param>
        private static void ValidNull(PropertyInfo parameterInfo)
        {
            List<ValidationAttribute> customAttributes = parameterInfo.GetCustomAttributes<ValidationAttribute>().ToList();
            if (customAttributes.Count <= 0) return;
            ValidationAttribute? requiredAttribute = customAttributes.FirstOrDefault(m => m is RequiredAttribute);
            if (requiredAttribute != null)
            {
                throw new InvalidOperationException(requiredAttribute.ErrorMessage);
            }
        }
        /// <summary>
        /// 验证值
        /// </summary>
        /// <param name="parameterInfo"></param>
        /// <param name="contextParameter"></param>
        private static void ValidValue(PropertyInfo parameterInfo, object contextParameter)
        {
            List<ValidationAttribute> customAttributes = parameterInfo.GetCustomAttributes<ValidationAttribute>().ToList();
            if (customAttributes.Count <= 0) return;
            ValidationAttribute? requiredAttribute = customAttributes.FirstOrDefault(m => m is RequiredAttribute);
            if (requiredAttribute != null || !contextParameter.IsNullOrEmptyString())
            {
                Valid(customAttributes, contextParameter);
            }
        }
        /// <summary>
        /// 验证Class 
        /// </summary>
        /// <param name="contextParameter"></param>
        /// <param name="parameterType"></param>
        private static void ValidClass(object contextParameter, Type parameterType)
        {
            PropertyInfo[] propertyInfos = parameterType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object? value = propertyInfo.GetValue(contextParameter);
                var customAttributes = propertyInfo.GetCustomAttributes<ValidationAttribute>().ToList();
                if (customAttributes.Count <= 0) continue;
                ValidationAttribute? requiredAttribute = customAttributes.FirstOrDefault(m => m is RequiredAttribute);
                if (requiredAttribute != null || value != null && !value.IsNullOrWhiteSpaceString())
                {
                    if (value == null) return;
                    Valid(customAttributes, value);
                }
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="customAttributes"></param>
        /// <param name="value"></param>
        private static void Valid(IEnumerable<ValidationAttribute> customAttributes, object value)
        {
            foreach (ValidationAttribute validationAttribute in customAttributes)
            {
                switch (value)
                {
                    case Guid guid when validationAttribute is RequiredAttribute && (!validationAttribute.IsValid(value) || guid == Guid.Empty):
                        throw new InvalidOperationException(validationAttribute.ErrorMessage);
                    case DateTime dateTime when validationAttribute is RequiredAttribute && (!validationAttribute.IsValid(value) || dateTime == DateTime.MinValue):
                        throw new InvalidOperationException(validationAttribute.ErrorMessage);
                    default:
                        {
                            if (!validationAttribute.IsValid(value)) throw new InvalidOperationException(validationAttribute.ErrorMessage);
                            break;
                        }
                }
            }
        }
    }
}
