using System;
using System.Collections.Generic;

namespace Materal.EnumHelper
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取所有枚举
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static List<Enum> GetAllEnum(this Enum @enum)
        {
            Type type = @enum.GetType();
            return type.GetAllEnum();
        }

        /// <summary>
        /// 获取枚举总数
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static int GetEnumCount(this Enum @enum)
        {
            Type type = @enum.GetType();
            return type.GetEnumCount();
        }
    }
}
