namespace Materal.Extensions
{
    /// <summary>
    /// 程序集扩展
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取类型 T 及其子类的实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes<T>(this Assembly assembly)
        {
            if (assembly == null) yield break;
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch
            {
                yield break;
            }
            foreach (Type type in types)
            {
                if (typeof(T).IsAssignableFrom(type) && type.IsPublic && type.IsClass && !type.IsAbstract) yield return type;
            }
        }
        /// <summary>
        /// 获取所在文件夹路径
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetDirectoryPath(this Assembly assembly) => Path.GetDirectoryName(assembly.Location) ?? throw new MateralException("获取所在文件夹路径失败");
        /// <summary>
        /// 是否包含指定特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static bool HasCustomAttribute<T>(this Assembly assembly)
            where T : Attribute => assembly.GetCustomAttribute<T>() is not null;
    }
}
