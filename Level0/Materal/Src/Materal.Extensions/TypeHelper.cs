namespace Materal.Extensions
{
    /// <summary>
    /// 类型帮助类
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByFilter(Func<Type, bool> filter, Assembly[]? assemblies = null)
        {
            assemblies ??= AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type? tempType = assembly.GetTypeByFilter(filter);
                if (tempType == null) continue;
                return tempType;
            }
            return null;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="argTypes"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByFilter(Func<Type, bool> filter, Type[] argTypes, Assembly[]? assemblies = null)
        {
            bool TrueFilter(Type m)
            {
                if (!filter(m)) return false;
                ConstructorInfo? constructorInfo = m.GetConstructor(argTypes);
                return constructorInfo != null;
            }
            return GetTypeByFilter(TrueFilter, assemblies);
        }
    }
}
