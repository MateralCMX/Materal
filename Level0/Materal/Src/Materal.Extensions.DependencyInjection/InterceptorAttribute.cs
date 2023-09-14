﻿namespace Materal.Extensions.DependencyInjection
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
        public virtual void Befor(InterceptorContext context) { }
        /// <summary>
        /// 之后
        /// </summary>
        public virtual void After(InterceptorContext context) { }
    }
}