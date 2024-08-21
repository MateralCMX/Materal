using Materal.Extensions.ValidationAttributes;

namespace System
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 默认验证错误处理
        /// </summary>
        public static Dictionary<Type, Func<ValidationAttribute, string, object?, string>> DefaultValidationFailHandler { get; } = new()
        {
            [typeof(RequiredAttribute)] = (a, m, o) => GetValidationFailMessage<RequiredAttribute>(a, m, o, (ta, tm, to) => $"{tm}必填"),
            [typeof(MinAttribute)] = (a, m, o) => GetValidationFailMessage<MinAttribute>(a, m, o, (ta, tm, to) => $"{tm}必须大于{ta.MinValue}"),
            [typeof(MaxAttribute)] = (a, m, o) => GetValidationFailMessage<MaxAttribute>(a, m, o, (ta, tm, to) => $"{tm}必须小于{ta.MaxValue}"),
            [typeof(RangeAttribute)] = (a, m, o) => GetValidationFailMessage<RangeAttribute>(a, m, o, (ta, tm, to) => $"{tm}必须在{ta.Minimum}-{ta.Maximum}之间"),
            [typeof(MinLengthAttribute)] = (a, m, o) => GetValidationFailMessage<MinLengthAttribute>(a, m, o, (ta, tm, to) => $"{tm}长度必须大于{ta.Length}"),
            [typeof(MaxLengthAttribute)] = (a, m, o) => GetValidationFailMessage<MaxLengthAttribute>(a, m, o, (ta, tm, to) => $"{tm}长度必须小于{ta.Length}"),
            [typeof(StringLengthAttribute)] = (a, m, o) => GetValidationFailMessage<StringLengthAttribute>(a, m, o, (ta, tm, to) => $"{tm}长度必须在{ta.MinimumLength}-{ta.MaximumLength}之间"),
        };
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool Validation(this object model, out string errorMessage)
        {
            try
            {
                model.Validation();
                errorMessage = string.Empty;
                return true;
            }
            catch (ValidationException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        public static void Validation(this object model) => model.Validation("");
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="prefix"></param>
        public static void Validation(this object model, string prefix)
        {
            Type modelType = model.GetType();
            List<MemberInfo> memberInfos = [.. modelType.GetProperties(), .. modelType.GetFields()];
            foreach (MemberInfo memberInfo in memberInfos)
            {
                Validation(model, memberInfo, prefix);
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="memberInfo"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool Validation(this object model, MemberInfo memberInfo, out string errorMessage)
        {
            try
            {
                model.Validation(memberInfo, string.Empty);
                errorMessage = string.Empty;
                return true;
            }
            catch (ValidationException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="memberInfo"></param>
        /// <param name="prefix"></param>
        public static void Validation(this object model, MemberInfo memberInfo, string prefix = "")
        {
            if (IsDefaultType(model)) return;
            object? memberValue;
            try
            {
                memberValue = memberInfo.GetValue(model);
                Validation(memberValue, memberInfo.GetCustomAttributes<ValidationAttribute>(), validationAttribute => NewException(validationAttribute, prefix, memberInfo, memberValue));
                if (memberValue is null || IsDefaultType(memberValue)) return;
                string nextPrefix = memberInfo.Name;
                if (!string.IsNullOrWhiteSpace(prefix))
                {
                    nextPrefix = $"{prefix}.{nextPrefix}";
                }
                if (memberValue is ICollection collection)
                {
                    int index = 0;
                    foreach (object? item in collection)
                    {
                        if (memberValue is null || IsDefaultType(item)) continue;
                        item.Validation($"{nextPrefix}[{index}]");
                    }
                }
                else
                {
                    memberValue.Validation($"{nextPrefix}");
                }
            }
            catch (ValidationException)
            {
                throw;
            }
            catch
            {
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validations"></param>
        /// <param name="newException"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool Validation(this object? value, IEnumerable<ValidationAttribute> validations, Func<ValidationAttribute, ValidationException>? newException, out string errorMessage)
        {
            try
            {
                value.Validation(validations, newException);
                errorMessage = string.Empty;
                return true;
            }
            catch (ValidationException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validations"></param>
        /// <param name="newException"></param>
        public static void Validation(this object? value, IEnumerable<ValidationAttribute> validations, Func<ValidationAttribute, ValidationException>? newException = null)
        {
            newException ??= validationAttribute => new ValidationException($"[{validationAttribute.GetType().Name}]验证失败", validationAttribute, value);
            foreach (ValidationAttribute validationAttribute in validations)
            {
                if (!validationAttribute.IsValid(value))
                {
                    throw newException(validationAttribute);
                }
                else if (validationAttribute is RequiredAttribute requiredAttribute)
                {
                    if (value is null) throw newException(validationAttribute);
                    switch (value)
                    {
                        case Guid guid when guid == Guid.Empty:
                            throw newException(validationAttribute);
                        case DateTime dateTime when dateTime == DateTime.MinValue:
                            throw newException(validationAttribute);
                    }
                }
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validations"></param>
        public static void Validation(this object? value, params ValidationAttribute[] validations) => Validation(value, validations, null);
        /// <summary>
        /// 新异常
        /// </summary>
        /// <param name="validationAttribute"></param>
        /// <param name="prefix"></param>
        /// <param name="memberInfo"></param>
        /// <param name="memberValue"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        private static ValidationException NewException(ValidationAttribute validationAttribute, string prefix, MemberInfo memberInfo, object? memberValue)
        {
            string errorMessage = GetValidationFailMesage(validationAttribute, prefix, memberInfo, memberValue);
            return new ValidationException(errorMessage, validationAttribute, memberValue);
        }
        /// <summary>
        /// 是默认类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsDefaultType(object value) => value is int || value is uint || value is short || value is ushort || value is long || value is ulong ||
                value is float || value is double || value is decimal || value is string || value is DateTime || value is TimeSpan || value is Guid ||
                value is Enum;
        /// <summary>
        /// 获得验证失败消息
        /// </summary>
        /// <param name="validationAttribute"></param>
        /// <param name="prefix"></param>
        /// <param name="memberInfo"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private static string GetValidationFailMesage(ValidationAttribute validationAttribute, string prefix, MemberInfo memberInfo, object? propertyValue)
        {
            string? message = validationAttribute.ErrorMessage;
            if (!string.IsNullOrWhiteSpace(message)) return message;
            Type type = validationAttribute.GetType();
            string memberName = memberInfo.Name;
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                memberName = $"{prefix}.{memberName}";
            }
            if (DefaultValidationFailHandler.TryGetValue(type, out Func<ValidationAttribute, string, object?, string>? value))
            {
                message = value(validationAttribute, memberName, propertyValue);
            }
            else
            {
                message = GetDefaultValidationFailMessage(memberName);
            }
            return message;
        }
        /// <summary>
        /// 获得默认验证失败消息
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        private static string GetDefaultValidationFailMessage(string memberName) => $"{memberName}验证失败";
        /// <summary>
        /// 获得必填验证失败消息
        /// </summary>
        /// <param name="validationAttribute"></param>
        /// <param name="memberName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="getErrorMessage"></param>
        /// <returns></returns>
        private static string GetValidationFailMessage<T>(ValidationAttribute validationAttribute, string memberName, object? propertyValue, Func<T, string, object?, string> getErrorMessage)
            where T : ValidationAttribute
        {
            if (validationAttribute is not T tAttribute) return GetDefaultValidationFailMessage(memberName);
            return getErrorMessage(tAttribute, memberName, propertyValue);
        }
    }
}
