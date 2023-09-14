using System.Reflection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 拦截器帮助类
    /// </summary>
    public static class InterceptorHelper
    {
        /// <summary>
        /// 处理拦截器
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static object? Handler<TInterface>(string methodName, object obj, params object[] args)
        {
            Type interfaceType = typeof(TInterface);
            Type objType = obj.GetType();
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            MethodInfo objMethodInfo = objType.GetMethod(methodName, argTypes) ?? throw new Exception("获取执行方法错误");
            MethodInfo interfaceMethodInfo = interfaceType.GetMethod(methodName, argTypes) ?? throw new Exception("获取接口方法错误");
            IEnumerable<InterceptorAttribute> interceptors = interfaceMethodInfo.GetCustomAttributes<InterceptorAttribute>();
            foreach (InterceptorAttribute interceptor in interceptors)
            {
                if (!interceptor.Befor()) return null;
            }
            object? result = objMethodInfo.Invoke(obj, args);
            foreach (InterceptorAttribute interceptor in interceptors)
            {
                interceptor.After();
            }
            return result;
        }
        //public static TResult? Handler<T, TResult>(string methodName, Func<TResult> next, params object[] args)
        //{
        //    TResult? result = default;
        //    Type serviceType = typeof(T);
        //    Type[] argTypes = args.Select(m => m.GetType()).ToArray();
        //    MethodInfo? methodInfo = serviceType.GetMethod(methodName, argTypes);
        //    if (methodInfo is not null)
        //    {
        //        IEnumerable<InterceptorAttribute> interceptors = methodInfo.GetCustomAttributes<InterceptorAttribute>();
        //        foreach (InterceptorAttribute interceptor in interceptors)
        //        {
        //            if (!interceptor.Befor()) return result;
        //        }
        //        result = next();
        //        foreach (InterceptorAttribute interceptor in interceptors)
        //        {
        //            interceptor.After();
        //        }
        //    }
        //    else
        //    {
        //        result = next();
        //    }
        //    return result;
        //}
    }
}