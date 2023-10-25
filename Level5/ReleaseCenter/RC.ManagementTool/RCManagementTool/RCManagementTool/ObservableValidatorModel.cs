using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RCManagementTool
{
    public class ObservableValidatorModel : ObservableValidator
    {
        private readonly Dictionary<string, PropertyInfo> _errorMessagePropertyInfos = new();
        public virtual bool ValidateErrors()
        {
            foreach (KeyValuePair<string, PropertyInfo> item in _errorMessagePropertyInfos)
            {
                item.Value.SetValue(this, string.Empty);
            }
            ValidateAllProperties();
            if (!HasErrors) return true;
            IEnumerable<ValidationResult> errors = GetErrors();
            Type type = GetType();
            foreach (ValidationResult error in errors)
            {
                foreach (string memberName in error.MemberNames)
                {
                    string propertyName = $"{memberName}ErrorMessage";
                    PropertyInfo? errorMessagePropertyInfo;
                    if (_errorMessagePropertyInfos.ContainsKey(propertyName))
                    {
                        errorMessagePropertyInfo = _errorMessagePropertyInfos[propertyName];
                    }
                    else
                    {
                        errorMessagePropertyInfo = type.GetProperty(propertyName);
                        if (errorMessagePropertyInfo is null || errorMessagePropertyInfo.PropertyType != typeof(string)) continue;
                        _errorMessagePropertyInfos.Add(propertyName, errorMessagePropertyInfo);
                    }
                    object? errorMessageObj = errorMessagePropertyInfo.GetValue(this);
                    string errorMessage = string.Empty;
                    if (errorMessageObj is not null && errorMessageObj is string oldMessage)
                    {
                        errorMessage = oldMessage;
                    }
                    errorMessage += error.ErrorMessage;
                    errorMessagePropertyInfo.SetValue(this, errorMessage);
                }
            }
            return false;
        }
    }
}
