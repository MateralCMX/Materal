using System;
using System.Collections.Generic;

namespace Materal.EnumHelper
{
    public static class TypeExtension
    {
        /// <summary>
        /// 获取所有枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Enum> GetAllEnum(this Type type)
        {
            if (!type.IsEnum) throw new MateralEnumHelperException("该类型不是枚举类型");
            var result = new List<Enum>();
            Array allEnums = Enum.GetValues(type);
            foreach (Enum item in allEnums)
            {
                result.Add(item);
            }
            return result;
        }
        /// <summary>
        /// 获取枚举总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetEnumCount(this Type type)
        {
            if (!type.IsEnum) throw new MateralEnumHelperException("该类型不是枚举类型");
            return Enum.GetValues(type).Length;
        }
    }
}
