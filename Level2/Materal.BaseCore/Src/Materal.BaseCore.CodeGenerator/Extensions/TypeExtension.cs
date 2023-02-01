using System.Reflection;

namespace Materal.BaseCore.CodeGenerator.Extensions
{
    /// <summary>
    /// <see cref="Type"/>常用扩展
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 是否可作为类型使用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static bool IsAssignableTo(this Type type, Type targetType)
        {
            if (targetType.IsInterface)
            {
                List<Type> allInterfaces = type.GetAllInterfaces();
                return allInterfaces.Any(m => m.FullName == targetType.FullName);
            }
            else if (targetType.IsClass)
            {
                if (type == targetType) return true;
                List<Type> allBaseTypes = type.GetAllBaseType();
                return allBaseTypes.Any(m => m.FullName == targetType.FullName);
            }
            return false;
        }
        /// <summary>
        /// 获得所有的基类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<Type> GetAllBaseType(this Type type)
        {
            List<Type> allBaseTypes = new();
            Type temp = type;
            Type objType = typeof(object);
            while (temp != objType)
            {
                allBaseTypes.Add(temp);
                temp = temp.BaseType;
            }
            allBaseTypes.Add(objType);
            return allBaseTypes;
        }
        /// <summary>
        /// 获得所有接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Type> GetAllInterfaces(this Type type) => new(GetAllInterfaces(type.GetInterfaces()));
        /// <summary>
        /// 获得所有接口
        /// </summary>
        /// <param name="interfaces"></param>
        /// <returns></returns>
        private static List<Type> GetAllInterfaces(Type[] interfaces)
        {
            List<Type> allInterfaces = new();
            foreach (var item in interfaces)
            {
                allInterfaces.Add(item);
                Type[] itemInterfaces = item.GetInterfaces();
                if (itemInterfaces.Length <= 0) continue;
                allInterfaces.AddRange(GetAllInterfaces(itemInterfaces));
            }
            return allInterfaces.Distinct().ToList();
        }
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object? Instantiation(this Type type, params object[] args)
        {
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            ConstructorInfo? constructorInfo = type.GetConstructor(argTypes);
            return constructorInfo?.Invoke(args);
        }
    }
}
