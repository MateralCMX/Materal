using System.ComponentModel;

namespace Materal.Extensions
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 描述字段名称
        /// </summary>
        private const string DescriptionMemberName = "Description";
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj)
        {
            if (inputObj is Enum @enum)
            {
                return inputObj.GetDescription(@enum.ToString());
            }
            else
            {
                Type objType = inputObj.GetType();
                DescriptionAttribute? attribute = objType.GetCustomAttribute<DescriptionAttribute>();
                if (attribute is not null) return attribute.Description;
                const string descriptionName = DescriptionMemberName;
                return inputObj.GetDescription(descriptionName);
            }
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <returns>描述</returns>
        public static string? GetDescriptionOrNull(this object inputObj)
        {
            try
            {
                return inputObj.GetDescription();
            }
            catch (ExtensionException)
            {
                return null;
            }
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="memberInfo">属性名称</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj, MemberInfo memberInfo)
        {
            DescriptionAttribute? attribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if (attribute is not null) return attribute.Description;
            if (inputObj is Enum @enum) return @enum.ToString();
            if (memberInfo.Name == DescriptionMemberName)
            {
                object? value = memberInfo.GetValue(inputObj);
                if (value is not null && value is string descriptionValue)
                {
                    return descriptionValue;
                }
            }
            throw new ExtensionException($"未找到特性{nameof(DescriptionAttribute)}");
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="memberInfo">属性名称</param>
        /// <returns>描述</returns>
        public static string? GetDescriptionOrNull(this object inputObj, MemberInfo memberInfo)
        {
            try
            {
                return inputObj.GetDescription(memberInfo);
            }
            catch (ExtensionException)
            {
                return null;
            }
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="memberName">属性名称</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj, string memberName)
        {
            Type objType = inputObj.GetType();
            MemberInfo? memberInfo = objType.GetRuntimeField(memberName);
            memberInfo ??= objType.GetRuntimeProperty(memberName);
            if (memberInfo is null) throw new ExtensionException($"未找到字段或属性{memberName}");
            return inputObj.GetDescription(memberInfo);
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="memberName">属性名称</param>
        /// <returns>描述</returns>
        public static string? GetDescriptionOrNull(this object inputObj, string memberName)
        {
            try
            {
                return inputObj.GetDescription(memberName);
            }
            catch (ExtensionException)
            {
                return null;
            }
        }
    }
}
