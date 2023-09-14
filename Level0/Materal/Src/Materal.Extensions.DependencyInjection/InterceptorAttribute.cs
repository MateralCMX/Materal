namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 拦截器特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class InterceptorAttribute : Attribute
    {
        /// <summary>
        /// 之前
        /// </summary>
        /// <returns></returns>
        public abstract bool Befor();
        /// <summary>
        /// 之后
        /// </summary>
        public abstract void After();
    }
}