namespace Materal.Extensions
{
    /// <summary>
    /// 程序集扩展
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static Type? GetTypeByFilter(this Assembly assembly, Func<Type, bool> filter)
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch
            {
                return null;
            }
            return types.FirstOrDefault(m => filter(m));
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypesByFilter(this Assembly assembly, Func<Type, bool> filter)
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch
            {
                return [];
            }
            return types.Where(m => filter(m));
        }
        /// <summary>
        /// 获取类型 T 及其子类的实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes<T>(this Assembly assembly) => assembly.GetTypesByFilter(type => type.IsAssignableTo(typeof(T)) && type.IsPublic && type.IsClass && !type.IsAbstract);
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
