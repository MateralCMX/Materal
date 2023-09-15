﻿using System.Reflection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 拦截器上下文
    /// </summary>
    public class InterceptorContext
    {
        /// <summary>
        /// 接口方法信息
        /// </summary>
        public MethodInfo InterfaceMethodInfo { get; }
        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo MethodInfo { get; }
        /// <summary>
        /// 参数
        /// </summary>
        public object?[] Parameters { get; }
        /// <summary>
        /// 是否返回
        /// </summary>
        public bool IsReturn { get; set; } = false;
        private object? _returnValue;
        /// <summary>
        /// 返回值
        /// </summary>
        public object? ReturnValue
        {
            get => _returnValue;
            set
            {
                IsReturn = true;
                _returnValue = value;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="interfaceMethodInfo"></param>
        /// <param name="methodInfo"></param>
        /// <param name="parameters"></param>
        public InterceptorContext(MethodInfo interfaceMethodInfo, MethodInfo methodInfo, object?[] parameters)
        {
            InterfaceMethodInfo = interfaceMethodInfo;
            MethodInfo = methodInfo;
            Parameters = parameters;
        }
    }
}