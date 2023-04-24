using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 节点处理器总线帮助类
    /// </summary>
    internal static class StepHandlerBusHelper
    {
        private static readonly Type _iStepHandlerType = typeof(IStepHandler<>);
        private static readonly Dictionary<Type, Type> _stepHandlerTypes = new();
        private static readonly Dictionary<Type, IStepHandler> _stepHandlers = new();
        /// <summary>
        /// 获得节点处理器类型
        /// </summary>
        /// <param name="stepDataType"></param>
        /// <returns></returns>
        public static Type GetStepHandlerType(Type stepDataType)
        {
            if (!stepDataType.IsAssignableTo(typeof(IStepData))) throw new WorkflowException("节点数据类型错误");
            return _iStepHandlerType.MakeGenericType(stepDataType);
        }
        /// <summary>
        /// 获得节点处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="stepDataType"></param>
        /// <returns></returns>
        public static IStepHandler? GetStepHandler(IServiceProvider services, Type stepDataType)
        {
            Type? trueStepDataType = stepDataType;
            while (!_stepHandlerTypes.ContainsKey(trueStepDataType))
            {
                trueStepDataType = trueStepDataType.BaseType;
                if (trueStepDataType == null || trueStepDataType == typeof(StepData) || trueStepDataType == typeof(object)) return null;
            }
            Type iStepHandlerType = GetStepHandlerType(trueStepDataType);
            if (_stepHandlers.ContainsKey(iStepHandlerType)) return _stepHandlers[iStepHandlerType];
            Type stepHandlerType = _stepHandlerTypes[trueStepDataType];
            object tempObj = ActivatorUtilities.CreateInstance(services, stepHandlerType);
            if (tempObj is IStepHandler stepHandler)
            {
                _stepHandlers.Add(iStepHandlerType, stepHandler);
                return stepHandler;
            }
            return null;
        }
        /// <summary>
        /// 添加节点处理器
        /// </summary>
        /// <param name="stepDataType"></param>
        /// <param name="stepHandlerType"></param>
        public static void AddStepHandler(Type stepDataType, Type stepHandlerType)
        {
            if (_stepHandlerTypes.ContainsKey(stepDataType))
            {
                _stepHandlerTypes[stepDataType] = stepHandlerType;
            }
            else
            {
                _stepHandlerTypes.Add(stepDataType, stepHandlerType);
            }
        }
        /// <summary>
        /// 添加节点处理器
        /// </summary>
        /// <param name="stepHandlerAssemblys"></param>
        public static void AddStepHandlers(ICollection<Assembly> stepHandlerAssemblys)
        {
            foreach (Assembly stepHandlerAssembly in stepHandlerAssemblys)
            {
                Type[] stepHandlerTypes = stepHandlerAssembly.GetTypes().Where(m => !m.IsInterface && !m.IsAbstract && m.IsAssignableTo(typeof(IStepHandler))).ToArray();
                foreach (Type stepHandlerType in stepHandlerTypes)
                {
                    foreach (Type tempInterfaceType in stepHandlerType.GetInterfaces().Where(m => m.IsGenericType && m.GenericTypeArguments.Length == 1))
                    {
                        Type stepDataType = tempInterfaceType.GenericTypeArguments[0];
                        AddStepHandler(stepDataType, stepHandlerType);
                        break;
                    }
                }
            }
        }
    }
}
