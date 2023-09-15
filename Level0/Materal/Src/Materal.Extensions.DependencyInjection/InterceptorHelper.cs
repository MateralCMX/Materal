using Materal.Abstractions;
using System.Reflection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 拦截器帮助类
    /// </summary>
    public static class InterceptorHelper
    {
        /// <summary>
        /// 全局拦截器
        /// </summary>
        private static List<GolablInterceptorModel> _golablInterceptors = new();
        /// <summary>
        /// 添加全局拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        /// <param name="filter"></param>
        public static void AddGolablInterceptors(InterceptorAttribute interceptor, Func<MethodInfo, MethodInfo, bool>? filter)
        {
            GolablInterceptorModel golablInterceptor = new(interceptor, filter);
            _golablInterceptors.Add(golablInterceptor);
        }
        /// <summary>
        /// 排序全局拦截器
        /// </summary>
        public static void SortGolablInterceptors() => _golablInterceptors = _golablInterceptors.OrderByDescending(m => m.Interceptor.Order).ToList();
        /// <summary>
        /// 处理拦截器
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        /// <param name="argTypes"></param>
        /// <param name="genericTypes"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static object? Handler<TInterface>(string methodName, object obj, object?[] args, Type[] argTypes, Type[] genericTypes) => Handler<TInterface, object>(methodName, obj, args, argTypes, genericTypes);
        /// <summary>
        /// 处理拦截器
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        /// <param name="argTypes"></param>
        /// <param name="genericTypes"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static TResult? Handler<TInterface, TResult>(string methodName, object obj, object?[] args, Type[] argTypes, Type[] genericTypes)
        {
            Type interfaceType = typeof(TInterface);
            Type objType = obj.GetType();
            MethodInfo objMethodInfo = GetMethodInfo(objType, methodName, argTypes, genericTypes) ?? throw new MateralException("获取对象方法错误");
            MethodInfo interfaceMethodInfo = GetMethodInfo(interfaceType, methodName, argTypes, genericTypes) ?? throw new MateralException("获取接口方法错误");
            List<InterceptorAttribute> interceptors = GetInterceptors(objMethodInfo, interfaceMethodInfo);
            InterceptorContext context = new(interfaceMethodInfo, objMethodInfo, args);
            object? objResult = null;
            foreach (InterceptorAttribute interceptor in interceptors)
            {
                interceptor.Befor(context);
                if (context.IsReturn)
                {
                    objResult = context.ReturnValue;
                    break;
                }
            }
            if (!context.IsReturn)
            {
                objResult = objMethodInfo.Invoke(obj, args);
                for (int i = interceptors.Count - 1; i >= 0; i--)
                {
                    InterceptorAttribute interceptor = interceptors[i];
                    interceptor.After(context);
                    if (context.IsReturn)
                    {
                        objResult = context.ReturnValue;
                        break;
                    }
                }
            }
            if (objResult is not TResult result) return default;
            return result;
        }
        /// <summary>
        /// 获得拦截器
        /// </summary>
        /// <param name="objMethodInfo"></param>
        /// <param name="interfaceMethodInfo"></param>
        /// <returns></returns>
        private static List<InterceptorAttribute> GetInterceptors(MethodInfo objMethodInfo, MethodInfo interfaceMethodInfo)
        {
            List<InterceptorAttribute> interceptors = interfaceMethodInfo.GetCustomAttributes<InterceptorAttribute>().ToList();
            interceptors.AddRange(objMethodInfo.GetCustomAttributes<InterceptorAttribute>());
            if(interfaceMethodInfo.DeclaringType is not null)
            {
                interceptors.AddRange(interfaceMethodInfo.DeclaringType.GetCustomAttributes<InterceptorAttribute>());
            }
            if(objMethodInfo.DeclaringType is not null)
            {
                interceptors.AddRange(objMethodInfo.DeclaringType.GetCustomAttributes<InterceptorAttribute>());
            }
            if(_golablInterceptors.Count > 0)
            {
                interceptors.AddRange(_golablInterceptors.Where(m => m.Filter(interfaceMethodInfo, objMethodInfo)).Select(m => m.Interceptor));
            }
            interceptors = interceptors.OrderByDescending(m => m.Order).ToList();
            return interceptors;
        }
        /// <summary>
        /// 获得方法信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="argTypes"></param>
        /// <param name="genericTypes"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        private static MethodInfo? GetMethodInfo(Type type, string methodName, Type[] argTypes, Type[] genericTypes)
        {
            MethodInfo? result = type.GetMethod(methodName, argTypes);
            if (result is null)
            {
                MethodInfo[] methodInfos = type.GetMethods().Where(m => m.Name == methodName).ToArray();
                if (methodInfos.Length <= 0) return null;
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    Type[] parameterTypes = methodInfo.GetParameters().Select(m => m.ParameterType).ToArray();
                    if (parameterTypes.Length != argTypes.Length || !methodInfo.IsGenericMethod) continue;
                    result = methodInfo;
                }
            }
            if (result is not null && result.IsGenericMethod)
            {
                result = result.MakeGenericMethod(genericTypes);
            }
            return result;
        }
    }
}