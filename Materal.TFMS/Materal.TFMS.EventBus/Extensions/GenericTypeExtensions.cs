using System;
using System.Linq;

namespace Materal.TFMS.EventBus.Extensions
{
    public static class GenericTypeExtensions
    {
        /// <summary>
        /// 获得一般类型名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGenericTypeName(this Type type)
        {
            string typeName;

            if (type.IsGenericType)
            {
                string genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }
        /// <summary>
        /// 获得一般类型名称
        /// </summary>
        /// <param name="@object"></param>
        /// <returns></returns>
        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
    }
}
