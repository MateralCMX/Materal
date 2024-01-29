//namespace Materal.Extensions.DependencyInjection
//{
//    /// <summary>
//    /// 拦截器上下文
//    /// </summary>
//    public class InterceptorContext(MethodInfo interfaceMethodInfo, MethodInfo methodInfo, object?[] parameters)
//    {
//        /// <summary>
//        /// 接口方法信息
//        /// </summary>
//        public MethodInfo InterfaceMethodInfo { get; } = interfaceMethodInfo;
//        /// <summary>
//        /// 方法信息
//        /// </summary>
//        public MethodInfo MethodInfo { get; } = methodInfo;
//        /// <summary>
//        /// 参数
//        /// </summary>
//        public object?[] Parameters { get; } = parameters;
//        /// <summary>
//        /// 是否返回
//        /// </summary>
//        public bool IsReturn { get; set; } = false;
//        private object? _returnValue;
//        /// <summary>
//        /// 返回值
//        /// </summary>
//        public object? ReturnValue
//        {
//            get => _returnValue;
//            set
//            {
//                IsReturn = true;
//                _returnValue = value;
//            }
//        }
//    }
//}
