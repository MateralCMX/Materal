using Materal.ConvertHelper;
using Materal.StringHelper;
using System.Reflection;

namespace Materal.HttpGenerator.Swagger
{
    public static class SwaggerExtension
    {
        /// <summary>
        /// 获得C#类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        public static string GetCSharpType(this string type, string format, bool isNull = false)
        {
            string result = type;
            if (result == "integer")
            {
                result = format switch
                {
                    "int32" => "int",
                    "int64" => "long",
                    _ => result
                };
            }
            else if (result == "number")
            {
                result = format switch
                {
                    "float" => "int",
                    "double" => "decimal",
                    _ => result
                };
            }
            else if (result == "string")
            {
                result = format switch
                {
                    "uuid" => "Guid",
                    "date-time" => "DateTime",
                    _ => result
                };
            }
            else if (result == "boolean")
            {
                result = "bool";
            }
            if (isNull)
            {
                result += "?";
            }
            return result;
        }
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
        /// <summary>
        /// 处理Ref字符串
        /// </summary>
        /// <param name="ref"></param>
        /// <returns></returns>
        public static string HandlerRef(this string @ref) => @ref.Split('/').Last();
    }
}
