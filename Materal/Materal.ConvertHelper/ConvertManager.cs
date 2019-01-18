using System;
using System.Linq;
using System.Reflection;

namespace Materal.ConvertHelper
{
    /// <summary>
    /// 转换控制器
    /// </summary>
    public class ConvertManager
    {
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <typeparam name="T">要设置的类型</typeparam>
        /// <returns>默认对象</returns>
        public static T GetDefaultObject<T>()
        {
            return (T)GetDefaultObject(typeof(T));
        }
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <typeparam name="T">要设置的类型</typeparam>
        /// <param name="parameters">参数</param>
        /// <returns>默认对象</returns>
        public static T GetDefaultObject<T>(params object[] parameters)
        {
            return (T)GetDefaultObject(typeof(T), parameters);
        }
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <param name="type">要设置的类型</param>
        /// <returns>默认对象</returns>
        public static object GetDefaultObject(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            ConstructorInfo constructor = (from m in constructors
                                           where m.GetParameters().Length == 0
                                           select m).FirstOrDefault();
            return constructor != null
                ? constructor.Invoke(new object[0])
                : throw new MateralConvertException("没有可用构造方法，需要一个无参数的构造方法");
        }
        /// <summary>
        /// 获得默认对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static object GetDefaultObject(Type type, params object[] parameters)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            ConstructorInfo constructor = (from m in constructors
                                           where m.GetParameters().Length == parameters.Length
                                           select m).FirstOrDefault();
            try
            {
                return constructor != null
                    ? constructor.Invoke(parameters)
                    : throw new MateralConvertException("没有可用构造方法，参数对应错误");
            }
            catch (Exception ex)
            {
                throw new MateralConvertException("对象实例化失败", ex);
            }
        }
    }
}
