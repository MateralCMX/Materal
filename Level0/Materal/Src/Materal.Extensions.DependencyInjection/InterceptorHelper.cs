//using System.Data;

//namespace Materal.Extensions.DependencyInjection
//{
//    /// <summary>
//    /// 拦截器帮助类
//    /// </summary>
//    public class InterceptorHelper(IEnumerable<GolablInterceptorModel>? golablInterceptors = null)
//    {
//        /// <summary>
//        /// 处理拦截器
//        /// </summary>
//        /// <typeparam name="TInterface"></typeparam>
//        /// <param name="methodName"></param>
//        /// <param name="obj"></param>
//        /// <param name="args"></param>
//        /// <param name="argTypes"></param>
//        /// <param name="genericTypes"></param>
//        /// <returns></returns>
//        /// <exception cref="MateralException"></exception>
//        public object? Handler<TInterface>(string methodName, object obj, object?[] args, Type[] argTypes, Type[] genericTypes) => Handler<TInterface, object>(methodName, obj, args, argTypes, genericTypes);
//        /// <summary>
//        /// 处理拦截器
//        /// </summary>
//        /// <typeparam name="TInterface"></typeparam>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="methodName"></param>
//        /// <param name="obj"></param>
//        /// <param name="args"></param>
//        /// <param name="argTypes"></param>
//        /// <param name="genericTypes"></param>
//        /// <returns></returns>
//        /// <exception cref="MateralException"></exception>
//        public TResult Handler<TInterface, TResult>(string methodName, object obj, object?[] args, Type[] argTypes, Type[] genericTypes)
//        {
//            Type interfaceType = typeof(TInterface);
//            Type objType = obj.GetType();
//            MethodInfo objMethodInfo = GetMethodInfo(objType, methodName, argTypes, genericTypes) ?? throw new MateralException("获取对象方法错误");
//            MethodInfo interfaceMethodInfo = GetMethodInfo(interfaceType, methodName, argTypes, genericTypes) ?? throw new MateralException("获取接口方法错误");
//            List<InterceptorAttribute> interceptors = GetInterceptors(objMethodInfo, interfaceMethodInfo);
//            InterceptorContext context = new(interfaceMethodInfo, objMethodInfo, args);
//            object? objResult = null;
//            foreach (InterceptorAttribute interceptor in interceptors)
//            {
//                interceptor.Befor(context);
//                if (context.IsReturn)
//                {
//                    objResult = context.ReturnValue;
//                    break;
//                }
//            }
//            if (!context.IsReturn)
//            {
//                objResult = objMethodInfo.Invoke(obj, args);
//                if(objResult is Task task)
//                {
//                    task.Wait();
//                }
//                for (int i = interceptors.Count - 1; i >= 0; i--)
//                {
//                    InterceptorAttribute interceptor = interceptors[i];
//                    interceptor.After(context);
//                    if (context.IsReturn)
//                    {
//                        objResult = context.ReturnValue;
//                        break;
//                    }
//                }
//            }
//            if (interfaceMethodInfo.ReturnType == typeof(void)) return default!;
//            if (objResult is not TResult result) throw new MateralException($"装饰器获取的结果不是类型[{typeof(TResult).FullName}]");
//            return result;
//        }
//        /// <summary>
//        /// 获得拦截器
//        /// </summary>
//        /// <param name="objMethodInfo"></param>
//        /// <param name="interfaceMethodInfo"></param>
//        /// <returns></returns>
//        private List<InterceptorAttribute> GetInterceptors(MethodInfo objMethodInfo, MethodInfo interfaceMethodInfo)
//        {
//            List<InterceptorAttribute> interceptors = interfaceMethodInfo.GetCustomAttributes<InterceptorAttribute>().ToList();
//            interceptors.AddRange(objMethodInfo.GetCustomAttributes<InterceptorAttribute>());
//            if (interfaceMethodInfo.DeclaringType is not null)
//            {
//                interceptors.AddRange(interfaceMethodInfo.DeclaringType.GetCustomAttributes<InterceptorAttribute>());
//            }
//            if (objMethodInfo.DeclaringType is not null)
//            {
//                interceptors.AddRange(objMethodInfo.DeclaringType.GetCustomAttributes<InterceptorAttribute>());
//            }
//            if (golablInterceptors is not null && golablInterceptors.Any())
//            {
//                interceptors.AddRange(golablInterceptors.Where(m => m.Filter(interfaceMethodInfo, objMethodInfo)).Select(m => m.Interceptor));
//            }
//            interceptors = [.. interceptors.OrderByDescending(m => m.Order)];
//            return interceptors;
//        }
//        /// <summary>
//        /// 获得方法信息
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="methodName"></param>
//        /// <param name="argTypes"></param>
//        /// <param name="genericTypes"></param>
//        /// <returns></returns>
//        /// <exception cref="MateralException"></exception>
//        public static MethodInfo? GetMethodInfo(Type type, string methodName, Type[] argTypes, Type[] genericTypes)
//        {
//            bool isGenericMethod = genericTypes is not null && genericTypes.Length > 0;
//            MethodInfo[] methodInfos = type.GetMethods().Where(m => m.Name == methodName).ToArray();
//            if (methodInfos.Length <= 0) return null;
//            foreach (MethodInfo methodInfo in methodInfos)
//            {
//                if (isGenericMethod != methodInfo.IsGenericMethod) continue;
//                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
//                if (parameterInfos.Length != argTypes.Length) continue;
//                bool parameterOK = true;
//                for (int i = 0; i < parameterInfos.Length; i++)
//                {
//                    if (parameterInfos[i].ParameterType != argTypes[i])
//                    {
//                        parameterOK = false;
//                        break;
//                    }
//                }
//                if (!parameterOK) continue;
//                if (methodInfo.IsGenericMethod)
//                {
//                    Type[] methodGenericTypes = methodInfo.GetGenericArguments();
//                    if (genericTypes is null || methodGenericTypes.Length != genericTypes.Length) continue;
//                    for (int i = 0; i < genericTypes.Length; i++)
//                    {
//                        if (methodGenericTypes[i].FullName is not null || genericTypes[i].FullName is not null || methodGenericTypes[i].Name != genericTypes[i].Name || methodGenericTypes[i].FullName != genericTypes[i].FullName)
//                        {
//                            parameterOK = false;
//                            break;
//                        }
//                    }
//                    if (!parameterOK) continue;
//                    return methodInfo.MakeGenericMethod(genericTypes);
//                }
//                else
//                {
//                    return methodInfo;
//                }
//            }
//            return null;
//        }
//    }
//}