﻿using Materal.Extensions;

namespace System.Reflection
{
    /// <summary>
    /// MemberInfo扩展
    /// </summary>
    public static class MemberInfoExtension
    {
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static object? GetValue(this MemberInfo memberInfo, object obj)
        {
            object? memberValue = memberInfo switch
            {
                PropertyInfo propertyInfo => propertyInfo.GetValue(obj, null),
                FieldInfo fieldInfo => fieldInfo.GetValue(obj),
                _ => throw new ExtensionException($"{memberInfo.Name}不能获取值"),
            };
            return memberValue;
        }
    }
}
