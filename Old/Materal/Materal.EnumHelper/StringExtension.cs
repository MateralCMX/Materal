using Materal.Common;
using System;
using System.Collections.Generic;

namespace Materal.EnumHelper
{
    public static class StringExtension
    {
        /// <summary>
        /// 根据描述获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumByDescription<T>(string description)
        {
            Type type = typeof(T);
            if (!type.IsEnum) throw new MateralEnumHelperException("该类型不是枚举类型");
            List<Enum> allEnums = type.GetAllEnum();
            foreach (Enum @enum in allEnums)
            {
                if (@enum.GetDescription().Equals(description))
                {
                    return (T)Enum.Parse(type, @enum.ToString());
                }
            }
            throw new MateralEnumHelperException("未找到该描述的枚举");
        }
    }
}
