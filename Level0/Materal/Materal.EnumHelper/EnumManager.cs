using System;

namespace Materal.EnumHelper
{
    public static class EnumManager
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T Parse<T>(string value, bool ignoreCase = true) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
    }
}
