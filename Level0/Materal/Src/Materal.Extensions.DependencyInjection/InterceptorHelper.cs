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
        /// 获得拦截器
        /// </summary>
        /// <param name="service"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static (List<InterceptorAttribute> interceptors, InterceptorContext context) GetInterceptorsAndInterceptorContext(object service, string methodName, object?[] args, Type[] argTypes)
        {
            Type objType = service.GetType();
            MethodInfo objMethodInfo = objType.GetMethod(methodName, argTypes) ?? throw new MateralException("获取对象方法失败");
            List<InterceptorAttribute> interceptors = GetInterceptors(objMethodInfo, argTypes);
            InterceptorContext context = new(objMethodInfo, args);
            return (interceptors, context);
        }
        /// <summary>
        /// 获得拦截器
        /// </summary>
        /// <param name="objMethodInfo"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static List<InterceptorAttribute> GetInterceptors(MethodInfo objMethodInfo, Type[] argTypes)
        {
            Type[] interfaceTypes = objMethodInfo.DeclaringType.GetInterfaces();
            List<InterceptorAttribute> interceptors = new();
            foreach (Type interfaceType in interfaceTypes)
            {
                MethodInfo? interceptorMethodInfo = interfaceType.GetMethod(objMethodInfo.Name, argTypes);
                if (interceptorMethodInfo is null) continue;
                interceptors.AddRange(interceptorMethodInfo.GetCustomAttributes<InterceptorAttribute>());
            }
            interceptors.AddRange(objMethodInfo.GetCustomAttributes<InterceptorAttribute>());
            return interceptors;
        }
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
            IEnumerable<InterceptorAttribute> interceptors = interfaceMethodInfo.GetCustomAttributes<InterceptorAttribute>();
            InterceptorContext context = new(objMethodInfo, args);
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
                foreach (InterceptorAttribute interceptor in interceptors)
                {
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