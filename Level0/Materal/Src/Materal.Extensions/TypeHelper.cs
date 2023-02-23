namespace System
{
    /// <summary>
    /// 类型帮助类
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Instantiation<T>(params object[] args) => typeof(T).Instantiation<T>(typeof(T), args);
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? InstantiationOrDefault<T>(params object[] args) => typeof(T).InstantiationOrDefault<T>(typeof(T), args);
    }
}
