using Materal.Common;
using System;
using System.Reflection;

namespace WebAPP.MateralUI.Helper
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="inputObj">对象</param>
        /// <returns>描述</returns>
        public static string GetTargetClass(this object inputObj)
        {
            Type objType = inputObj.GetType();
            FieldInfo fieldInfo = objType.GetField(inputObj.ToString());
            Attribute attribute = fieldInfo != null ?
                fieldInfo.GetCustomAttribute(typeof(TargetClassAttribute)) :
                objType.GetCustomAttribute(typeof(TargetClassAttribute));
            return attribute != null && attribute is TargetClassAttribute targetClassAttribute ?
                targetClassAttribute.Class :
                throw new MateralException("需要特性TargetClassAttribute");
        }
    }
}
