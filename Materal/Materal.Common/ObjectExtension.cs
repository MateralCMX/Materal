using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Materal.Common
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj)
        {
            Type objType = inputObj.GetType();
            FieldInfo fieldInfo = objType.GetField(inputObj.ToString());
            Attribute attribute = fieldInfo != null ? 
                fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) : 
                objType.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute != null && attribute is DescriptionAttribute descriptionAttribute ?
                descriptionAttribute.Description :
                throw new MateralException("需要特性DescriptionAttribute");
        }
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>描述</returns>
        public static string GetDescription(this object inputObj, string propertyName)
        {
            Type objType = inputObj.GetType();
            PropertyInfo propertyInfo = objType.GetProperty(propertyName);
            if(propertyInfo == null) throw new MateralException($"未找到名称是{propertyName}的属性");
            var attribute = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
            return attribute != null ?
                attribute.Description :
                throw new MateralException("需要特性DescriptionAttribute");
        }
        /// <summary>
        /// 对象是否为空或者空字符串
        /// </summary>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyString(this object inputObj)
        {
            switch (inputObj)
            {
                case null:
                    return true;
                case string inputStr:
                    return string.IsNullOrEmpty(inputStr);
                default:
                    return false;
            }
        }
        /// <summary>
        /// 对象是否为空或者空或者空格字符串
        /// </summary>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpaceString(this object inputObj)
        {
            switch (inputObj)
            {
                case null:
                    return true;
                case string inputStr:
                    return string.IsNullOrWhiteSpace(inputStr);
                default:
                    return false;
            }
        }
        /// <summary>
        /// 属性是否包含
        /// </summary>
        /// <param name="leftModel"></param>
        /// <param name="rightModel"></param>
        /// <param name="maps"></param>
        /// <returns></returns>
        public static bool PropertyContain(this object leftModel, object rightModel, Dictionary<string, Func<bool>> maps = null)
        {
            Type aType = leftModel.GetType();
            Type bType = rightModel.GetType();
            foreach (PropertyInfo aProperty in aType.GetProperties())
            {
                if (maps != null && maps.ContainsKey(aProperty.Name))
                {
                    bool mapResult = maps[aProperty.Name].Invoke();
                    if (!mapResult) return false;
                }
                else
                {
                    PropertyInfo bProperty = bType.GetProperty(aProperty.Name);
                    if (bProperty == null || aProperty.PropertyType != bProperty.PropertyType) return false;
                    object aValue = aProperty.GetValue(leftModel);
                    object bValue = bProperty.GetValue(rightModel);
                    if (aValue != bValue) return false;
                }
            }
            return true;
        }
    }
}
