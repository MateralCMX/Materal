using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Materal.Model
{
    public static class ObjectExtensions
    {
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
        public static void Validation(this object model)
        {
            Type thisType = model.GetType();
            foreach (PropertyInfo propertyInfo in thisType.GetProperties())
            {
                foreach (ValidationAttribute validationAttribute in propertyInfo.GetCustomAttributes<ValidationAttribute>())
                {
                    object? propertyValue = propertyInfo.GetValue(model, null);
                    if (!validationAttribute.IsValid(propertyValue))
                    {
                        throw new ValidationException(validationAttribute.ErrorMessage, validationAttribute, propertyValue);
                    }
                }
            }
        }
    }
}
