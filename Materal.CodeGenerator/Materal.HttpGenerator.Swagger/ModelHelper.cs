using Materal.ConvertHelper;
using Materal.StringHelper;
using System.Reflection;

namespace Materal.HttpGenerator.Swagger
{
    public static class ModelHelper
    {
        public static void SetDefaultProperty(this object model, string name, string value)
        {
            name = name.FirstUpper();
            PropertyInfo? propertyInfo = model.GetType().GetProperty(name);
            if (propertyInfo == null) return;
            if (value.CanConvertTo(propertyInfo.PropertyType))
            {
                propertyInfo.SetValue(model, value.ConvertTo(propertyInfo.PropertyType));
            }
        }
        public static string HandlerRef(this string @ref) => @ref.Split('/').Last();
    }
}
