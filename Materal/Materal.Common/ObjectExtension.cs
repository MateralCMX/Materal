using System;
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
    }
}
