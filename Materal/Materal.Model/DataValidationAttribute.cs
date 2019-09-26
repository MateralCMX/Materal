﻿using AspectCore.DynamicProxy;
using Materal.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Materal.Model
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DataValidationAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            InvokeBefore(context);
            await context.Invoke(next);
        }
        /// <summary>
        /// 执行之前
        /// </summary>
        private void InvokeBefore(AspectContext context)
        {
            ParameterInfo[] parameterInfos = context.ImplementationMethod.GetParameters();
            for (var i = 0; i < parameterInfos.Length; i++)
            {
                object contextParameter = context.Parameters[i];
                Type parameterType = contextParameter.GetType();
                if (parameterType.IsClass)
                {
                    if (contextParameter is string contextString)
                    {
                        ValidValue(parameterInfos[i], contextString);
                    }
                    ValidClass(contextParameter, parameterType);
                }
                else
                {
                    ValidValue(parameterInfos[i], contextParameter);
                }
            }
        }
        /// <summary>
        /// 验证值
        /// </summary>
        /// <param name="parameterInfo"></param>
        /// <param name="contextParameter"></param>
        private void ValidValue(ParameterInfo parameterInfo, object contextParameter)
        {
            List<ValidationAttribute> customAttributes = parameterInfo.GetCustomAttributes<ValidationAttribute>().ToList();
            if (customAttributes.Count <= 0) return;
            ValidationAttribute requiredAttribute = customAttributes.FirstOrDefault(m => m is RequiredAttribute);
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
        private void ValidClass(object contextParameter, Type parameterType)
        {
            PropertyInfo[] propertyInfos = parameterType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object value = propertyInfo.GetValue(contextParameter);
                List<ValidationAttribute> customAttributes = propertyInfo.GetCustomAttributes<ValidationAttribute>().ToList();
                if (customAttributes.Count <= 0) continue;
                ValidationAttribute requiredAttribute = customAttributes.FirstOrDefault(m => m is RequiredAttribute);
                if (requiredAttribute != null || !value.IsNullOrEmptyString())
                {
                    Valid(customAttributes, value);
                }
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="customAttributes"></param>
        /// <param name="value"></param>
        private void Valid(IEnumerable<ValidationAttribute> customAttributes, object value)
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
