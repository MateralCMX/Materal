//namespace Materal.Extensions.DependencyInjection
//{
//    /// <summary>
//    /// 全局拦截器模型
//    /// </summary>
//    public class GolablInterceptorModel
//    {
//        /// <summary>
//        /// 拦截器
//        /// </summary>
//        public InterceptorAttribute Interceptor { get; }
//        /// <summary>
//        /// 过滤器
//        /// </summary>
//        public Func<MethodInfo, MethodInfo, bool> Filter { get; }
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        public GolablInterceptorModel(InterceptorAttribute interceptor, Func<MethodInfo, MethodInfo, bool>? filter)
//        {
//            filter ??= (im, m) => true;
//            Interceptor = interceptor;
//            Filter = filter;
//        }
//    }
//}